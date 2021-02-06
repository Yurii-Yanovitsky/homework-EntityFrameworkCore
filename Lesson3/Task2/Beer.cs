using System.Collections.Generic;

namespace Task2
{
    public class Beer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Distributor> Distributors { get; set; } = new List<Distributor>();
    }
}
