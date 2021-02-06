using System.Collections.Generic;

namespace AdditionalTask
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}
