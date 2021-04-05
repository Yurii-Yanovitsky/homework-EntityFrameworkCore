using Microsoft.EntityFrameworkCore;

namespace AdditionalTask
{
    public class ShopDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Customers);
        }
    }
}
