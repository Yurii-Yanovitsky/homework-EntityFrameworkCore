using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new PhoneDbContext();

            Console.WriteLine("ASYNCHRONOUS STORED PROCEDURE CALL: \n");

            //RecreateDB();
            //SeedData();

            var paramId1 = new SqlParameter()
            {
                ParameterName = "@paramId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 1
            };

            var id = new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var name = new SqlParameter()
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 30
            };

            var price = new SqlParameter()
            {
                ParameterName = "@price",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var task1 = context.Database.ExecuteSqlRawAsync("GetPhoneByID_1 @paramId, @id OUT, @name OUT, @price OUT", paramId1, id, name, price);
            task1.Wait();

            Console.WriteLine($"1) {id.Value}. {name.Value} - {price.Value}$");

            //Console.WriteLine("\nPRESS ANY KEY\n");
            //Console.ReadKey();

            var paramId2 = new SqlParameter("@paramId", 3);
            var task2 = new Task<Phone>(() => context.Phones
                                           .FromSqlRaw("GetPhoneByID_2 @paramId", paramId2)
                                           .ToList()
                                           .FirstOrDefault()
                                      );
            task2.Start();

            var car = task2.Result;
            Console.WriteLine($"2) {car.Id}. {car.Name} - {car.Price}$");
        }

        static void SeedData()
        {
            using var context = new PhoneDbContext();
            var company1 = new Company() { Name = "Samsung" };
            var company2 = new Company() { Name = "Apple" };

            var phone1 = new Phone() { Name = "Samsung Galaxy A70", Price = 12000, Company = company1 };
            var phone2 = new Phone() { Name = "Samsung Galaxy S20", Price = 20000, Company = company1 };
            var phone3 = new Phone() { Name = "IPhone 11", Price = 25000, Company = company2 };
            var phone4 = new Phone() { Name = "IPhone XR", Price = 15000, Company = company2 };

            context.Phones.AddRange(phone1, phone2, phone3, phone4);
            context.SaveChanges();
        }

        static void RecreateDB()
        {
            using var context = new PhoneDbContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            //context.Phones.RemoveRange(context.Phones);
            //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT (Phones, RESEED, 0)");
            //context.SaveChanges();
        }
    }

}
//public partial class CreateStoredProcedures : Migration
//{
//    protected override void Up(MigrationBuilder migrationBuilder)
//    {
//        migrationBuilder.Sql(@"
//CREATE PROCEDURE [dbo].[GetPhoneByID_1]
//@paramId int,
//@Id int OUT,
//@Name varchar(25) OUT,
//@Price int OUT
//AS
//SELECT @ID = p.Id, @Name = p.[Name], @Price = p.Price FROM Phone as p
//WHERE p.Id = @paramId
//GO");
//        migrationBuilder.Sql(@"
//CREATE PROCEDURE [dbo].[GetPhoneByID_2]
//@paramId int
//AS
//SELECT* FROM Phones as p
//WHERE p.Id = @paramId
//GO");





//    }

//    protected override void Down(MigrationBuilder migrationBuilder)
//    {
//        migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPhoneByID_1]");
//        migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPhoneByID_2]");
//    }
//}



//1)
//CREATE PROCEDURE[dbo].[GetCarsByID_1]
//@paramId int,
// @Id int out,
// @Name varchar(25) out,
// @Price int out
// AS
// SELECT @ID = c.Id, @Name = c.[Name], @Price = c.Price FROM Cars as c
// WHERE c.Id = @paramId
// GO


//2)
//Create PROCEDURE[dbo].[GetCarsByID_2]
//@paramId int
//AS
// SELECT* FROM Cars as c
// WHERE c.Id = @paramId
// GO

// exec GetCarsByID_2 10
// GO
