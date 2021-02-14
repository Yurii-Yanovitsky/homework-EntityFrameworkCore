using System.Collections.Generic;

public class ParkingSpot
{
    public int Id { get; set; }
    public string Number { get; set; }
    public int Cost { get; set; }
    public bool IsBusy { get; set; }
    public int ParkingLotId { get; set; }
    public ParkingLot ParkingLot { get; set; }
    public List<Visit> Visits { get; set; }

    public ParkingSpot()
    {
        Visits = new List<Visit>();
    }

}
