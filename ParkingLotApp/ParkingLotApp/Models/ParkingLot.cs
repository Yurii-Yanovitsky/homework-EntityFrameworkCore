using System.Collections.Generic;

public class ParkingLot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<ParkingSpot> ParkingSpots { get; set; }

    public ParkingLot()
    {
        ParkingSpots = new List<ParkingSpot>();
    }
}
