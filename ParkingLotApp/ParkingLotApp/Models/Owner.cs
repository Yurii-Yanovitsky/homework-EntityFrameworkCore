using System.Collections.Generic;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public List<Car> Cars { get; set; }

    public Owner()
    {
        Cars = new List<Car>();
    }
}
