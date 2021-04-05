namespace AdditionalTask
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
