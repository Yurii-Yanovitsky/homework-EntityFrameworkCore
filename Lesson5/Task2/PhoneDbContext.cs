using Microsoft.EntityFrameworkCore;

namespace Task2
{
    public class PhoneDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PhoneAppDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Phones)
                .HasForeignKey(p=>p.CompanyID);

            modelBuilder.Entity<Company>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }

}