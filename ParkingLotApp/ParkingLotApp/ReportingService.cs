using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

public class ReportingService
{
    private readonly ParkingLotDbContext _context;

    public ReportingService(ParkingLotDbContext context)
    {
        _context = context;
    }

    public Result CalculateCreditForCustomer(string phoneNumber, DateTime startDate)
    {
        var ownerCars = _context.Visits
            .Where(v => v.Entered >= startDate && v.Car.Owner.Phone == phoneNumber)
            .Select(v => new { v.CarId, v.ParkingSpot, v.Entered, v.Left })
            .AsEnumerable()
            .GroupBy(v => v.CarId)
            .ToList();

        if (ownerCars.Any())
        {
            decimal totallSum = 0;

            foreach (var car in ownerCars)
            {
                foreach (var visit in car)
                {
                    var costPerHour = visit.ParkingSpot.Cost;

                    TimeSpan usingTime = (visit.Left ?? DateTime.Now) - visit.Entered;

                    totallSum += (decimal)(usingTime.TotalMinutes * costPerHour) / 60;
                }
            }

            return Result.Success(totallSum);
        }
        else
        {
            return Result.SearchError();
        }
    }

    public class Result
    {
        public bool IsSuccessful { get; set; }
        public decimal Value { get; set; }
        public string Error { get; set; }

        public Result(decimal value, bool isSuccessful, string error)
        {
            if (isSuccessful)
            {
                Value = value;
            }

            IsSuccessful = isSuccessful;
        }

        static public Result Success(decimal sum)
        {
            return new Result(sum, true, null);
        }
        static public Result SearchError()
        {
            return new Result(0, false, "No owner found with that phone number or date.");
        }
    }
}
