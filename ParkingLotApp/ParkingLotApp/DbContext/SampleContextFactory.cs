using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;

public class SampleContextFactory : IDesignTimeDbContextFactory<ParkingLotDbContext>
{
    public ParkingLotDbContext CreateDbContext(string[] args)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");

        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");

        DbContextOptionsBuilder<ParkingLotDbContext> optionsBuilder = new DbContextOptionsBuilder<ParkingLotDbContext>();
        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        return new ParkingLotDbContext(options);
    }
}
