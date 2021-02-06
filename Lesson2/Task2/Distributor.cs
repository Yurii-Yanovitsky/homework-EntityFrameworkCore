using System.Collections.Generic;

namespace Task2
{
    public class Distributor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Beer> Beers { get; set; } = new List<Beer>();
    }
}
