using Microsoft.EntityFrameworkCore;

public class ParkingLotDbContext : DbContext
{
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ParkingLot> ParkingLots { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<Garage> Garage { get; set; }
    public DbSet<Visit> Visits { get; set; }

    public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Cars)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Car)
            .WithMany(c => c.Visits)
            .HasForeignKey(c => c.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.ParkingSpot)
            .WithMany(c => c.Visits)
            .HasForeignKey(c => c.ParkingSpotId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .Property(v => v.Left)
            .IsRequired(false);

        modelBuilder.Entity<ParkingSpot>()
            .Property(ps => ps.IsBusy)
            .HasDefaultValue(false).IsRequired();
    }
}
