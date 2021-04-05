using System.Collections.Generic;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public Owner Owner { get; set; }
    public int OwnerId { get; set; }
    public List<Visit> Visits { get; set; }

    public Car()
    {
        Visits = new List<Visit>();
    }
}
