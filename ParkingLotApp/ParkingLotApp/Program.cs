using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingLotAppDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddScoped<ReportingService>()
                .AddTransient<ParkingLotDbContext>(factory => new SampleContextFactory().CreateDbContext(args));

            var serviceProvider = services.BuildServiceProvider();

            SeedData(serviceProvider.GetService<ParkingLotDbContext>());
            ShowData(serviceProvider.GetService<ParkingLotDbContext>());

            var reportingService = serviceProvider.GetService<ReportingService>();

            var phoneNumber = "0501448285";
            var startDate = new DateTime(2021, 2, 11);

            var result = reportingService.CalculateCreditForCustomer(phoneNumber, startDate);

            if (result.IsSuccessful)
            {
                Console.WriteLine($"Total cost for 0501448285 - {result.Value}");
            }
        }

        static void SeedData(ParkingLotDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var owner1 = new Owner { Name = "Adam", Phone = "0994750925" };
            var owner2 = new Owner { Name = "Erick", Phone = "0501448285" };
            var owner3 = new Owner { Name = "Rob", Phone = "0678452522" };
            var owner4 = new Owner { Name = "Alice", Phone = "0637870244" };

            var car1 = new Car { Name = "Ferrari", LicensePlate = "XA1232", Owner = owner1 };
            var car2 = new Car { Name = "Audi", LicensePlate = "AA7575", Owner = owner2 };
            var car3 = new Car { Name = "BMW", LicensePlate = "AI8585", Owner = owner3 };
            var car4 = new Car { Name = "Toyota", LicensePlate = "AX7777", Owner = owner4 };

            context.Cars.AddRange(car1, car2, car3, car4);
            context.SaveChanges();

            var parkingLot1 = new ParkingLot { Name = "Greenway Self-Park", Address = "Molochnaya 3" };

            var parkingSpot1 = new ParkingSpot { Number = "1", IsBusy = false, Cost = 60, ParkingLot = parkingLot1 };
            var parkingSpot2 = new ParkingSpot { Number = "2", IsBusy = false, Cost = 50, ParkingLot = parkingLot1 };

            var garage1 = new Garage { Number = "1", IsBusy = false, Cost = 100, ParkingLot = parkingLot1 };
            var garage2 = new Garage { Number = "2", IsBusy = false, Cost = 100, ParkingLot = parkingLot1 };

            context.ParkingSpots.AddRange(parkingSpot1, parkingSpot2, garage1, garage2);
            context.SaveChanges();

            var visit1 = new Visit { Car = car1, ParkingSpot = parkingSpot1, Entered = new DateTime(2021, 2, 10, 12, 20, 45), Left = new DateTime(2021, 2, 11, 15, 25, 30) };
            var visit2 = new Visit { Car = car2, ParkingSpot = garage1, Entered = new DateTime(2021, 2, 11), Left = DateTime.Now, };
            var visit3 = new Visit { Car = car3, ParkingSpot = parkingSpot2, Entered = DateTime.Now, Left = null, };
            var visit4 = new Visit { Car = car4, ParkingSpot = garage2, Entered = DateTime.Now, Left = null, };

            context.Visits.AddRange(visit1, visit2, visit3, visit4);
            context.SaveChanges();
        }

        static void ShowData(ParkingLotDbContext context)
        {
            context.Owners.Load();
            context.Cars.Load();
            context.ParkingLots.Load();
            context.ParkingSpots.Load();
            context.Visits.Load();

            foreach (var owner in context.Owners)
            {
                Console.WriteLine($"{owner.Name} - {owner.Phone}");

                Console.WriteLine("Cars: ");

                foreach (var car in owner.Cars)
                {
                    Console.WriteLine($"{car.Name} - {car.LicensePlate}");
                }
                Console.WriteLine();
            }

            foreach (var parkingLot in context.ParkingLots)
            {
                Console.WriteLine($"Parking Lot: {parkingLot.Name}");

                Console.WriteLine("ParkingSpots: ");

                foreach (var parkingSpot in parkingLot.ParkingSpots)
                {
                    Console.WriteLine($"Number - {parkingSpot.Number}, Cost - {parkingSpot.Cost}, Is Busy - {parkingSpot.IsBusy}");
                }

                Console.WriteLine();
            }

            foreach (var visit in context.Visits)
            {

                if (visit.Left != null)
                {
                    Console.WriteLine($"Car {visit.CarId} entered parking spot {visit.ParkingSpotId} in parking lot {visit.ParkingSpot.ParkingLotId}" + $"at {visit.Entered} and left at {visit.Left}");
                }
                else
                {
                    Console.WriteLine(@$"Car {visit.CarId} entered parking spot {visit.ParkingSpotId} in parking lot {visit.ParkingSpot.ParkingLotId} at {visit.Entered} and still there");
                }
            }

            Console.WriteLine();
        }
    }
}