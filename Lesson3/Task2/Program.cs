using System;
using System.Linq;

namespace Task2
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (BeerDbContext context = new BeerDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var b1 = new Beer() { Name = "Черниговское", Price = 100 };
                var b2 = new Beer() { Name = "Staropramen", Price = 300 };
                var b3 = new Beer() { Name = "Bud", Price = 150 };
                var b4 = new Beer() { Name = "Оболонь", Price = 50 };

                context.Beers.AddRange(b1, b2, b3, b4);
                context.SaveChanges();

                var d1 = new Distributor() { Name = "Churrasco Bar", PhoneNumber = "380661448285" };
                var d2 = new Distributor() { Name = "Lumber", PhoneNumber = "380952596080" };
                var d3 = new Distributor() { Name = "Свинья Бар", PhoneNumber = "380635867245" };
                var d4 = new Distributor() { Name = "Mundstuck", PhoneNumber = "380967882457" };
                var d5 = new Distributor() { Name = "Manhattan bar", PhoneNumber = "380992295008" };

                context.Distributors.AddRange(d1, d2, d3, d4, d5);
                context.SaveChanges();

                b1.Distributors.AddRange(new[] { d2, d4 });
                b2.Distributors.AddRange(new[] { d5, d1 });
                b3.Distributors.AddRange(new[] { d3, d1, d4 });
                b4.Distributors.AddRange(new[] { d2, d5, d3 });

                context.SaveChanges();

                var beers = context.Beers.ToList();

                foreach (var b in beers)
                {
                    Console.WriteLine($"\nBeer: {b.Name}");

                    var distr = b.Distributors;

                    foreach (var d in distr)
                    {
                        Console.WriteLine(d.Name);
                    }
                }
            }
        }
    }
}
