using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AdditionalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new PhoneAppDBContext();

            Console.WriteLine("Information about all phones: ");

            var phonesResult = context.Phones
                .Include(p => p.Company)
                .AsEnumerable()
                .GroupBy(p => p.Company.Name)
                .Select(g => new { Company = g.Key, Phones = g })
                .ToList();

            foreach (var group in phonesResult)
            {
                Console.WriteLine($"Company: {group.Company}");

                foreach (var p in group.Phones)
                {
                    Console.WriteLine($"{p.Name} - {p.Price}");
                }

                Console.WriteLine();
            }

            Console.Write("Search: ");

            string input = Console.ReadLine();

            Console.WriteLine();

            var phones = context.Phones.Where(p => EF.Functions.Like(p.Name, $"%{input}%")).ToList();

            foreach (var p in phones)
            {
                Console.WriteLine($"{p.Name} - {p.Price}");
            }

        }
    }
}
