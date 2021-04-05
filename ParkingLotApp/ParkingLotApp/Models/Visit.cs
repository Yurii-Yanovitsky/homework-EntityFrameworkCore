using System;

public class Visit
{
    public int Id { get; set; }
    public DateTime Entered { get; set; }
    public DateTime? Left { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public int ParkingSpotId { get; set; }
    public ParkingSpot ParkingSpot { get; set; }

}
