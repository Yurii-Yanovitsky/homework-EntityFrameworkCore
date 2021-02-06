using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AdditionalTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                var c1 = new Customer() { Name = "Ann" };
                var c2 = new Customer() { Name = "Julia" };
                var c3 = new Customer() { Name = "James" };
                var c4 = new Customer() { Name = "Rob" };

                context.Customers.AddRange(c1, c2);

                var e1 = new Employee() { Name = "Tom", Post = "Manager", Customers = new[] { c2, c4 } };
                var e2 = new Employee() { Name = "Bob", Post = "Worker", Customers = new[] { c1, c3 } };

                context.AddRange(e1, e2);

                context.SaveChanges();
            }

            using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                //ЯВНАЯ ЗАГРУЗКА
                Console.WriteLine("EXPLICIT LOADING");

                //1) Загружаем employee, который содержит (связан) с опеределенным customer-ом 
                Console.WriteLine("\n1)");
                var customer = context.Customers.FirstOrDefault();
                var employees = context.Employees.Where(e => e.Customers.Any(c => c.ID == customer.ID)).ToList();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.ID}.{e.Name} - {e.Post}");

                    foreach (var c in e.Customers)
                    {
                        Console.WriteLine($"{c.Name}");
                    }
                }

                //2) Загружаем customer-ов, которые связанные с опеределенным employee
                Console.WriteLine("\n2)");
                var employee = context.Employees.FirstOrDefault();
                var customers = context.Customers.Where(e => e.Employee.ID == employee.ID).ToList();

                Console.WriteLine($"{employee.ID}.{employee.Name} - {employee.Post}");
                foreach (var c in customers)
                {
                    Console.WriteLine($"{c.Name}");
                }

                //3) Загружаем первого customer-a и связанного с ним employee
                Console.WriteLine("\n3)");
                customer = context.Customers.FirstOrDefault();
                context.Entry(customer).Reference(c => c.Employee).Load();

                Console.WriteLine($"{customer.Employee.ID}.{customer.Employee.Name} - {customer.Employee.Post}");
                Console.WriteLine(customer.Name);

                //4) Загружаем первого employee и связанных с ним customer-ов
                Console.WriteLine("\n4)");
                employee = context.Employees.FirstOrDefault();
                context.Entry(employee).Collection(e => e.Customers).Load();

                Console.WriteLine($"{employee.ID}.{employee.Name} - {employee.Post}");
                foreach (var c in employee.Customers)
                {
                    Console.WriteLine(c.Name);
                }

                //5) Загружаем employees и связанных с ними customer-ов используя проекцию
                Console.WriteLine("\n5)");
                var empProjection = context.Employees.Select(e => new { e.ID, e.Name, e.Post, e.Customers }).ToList();

                foreach (var ep in empProjection)
                {
                    Console.WriteLine($"{ep.ID}.{ep.Name} - {ep.Post}");

                    foreach (var c in ep.Customers)
                    {
                        Console.WriteLine($"{c.Name}");
                    }
                }

                //6)Загружаем всех employees и customers
                Console.WriteLine("\n6)");
                context.Employees.Load();
                context.Customers.Load();

                foreach (var e in context.Employees.ToList())
                {
                    Console.WriteLine($"{e.ID}.{e.Name} - {e.Post}");

                    foreach (var c in e.Customers)
                    {
                        Console.WriteLine($"{c.Name}");
                    }
                }


                // Жадная загрузка
                Console.WriteLine("\nEAGER LOADING:\n");
                employees = context.Employees.Include(e => e.Customers).ToList();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.ID}.{e.Name} - {e.Post}");

                    foreach (var c in e.Customers)
                    {
                        Console.WriteLine($"{c.Name}");
                    }
                }
            }
        }
    }
}
