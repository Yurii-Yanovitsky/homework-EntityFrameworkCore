using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AdditionalTask
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<ShopDBContext>
    {
        public ShopDBContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(File.OpenRead("appsettings.json"));

            var config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopDBContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new ShopDBContext(options);
        }
    }
}
