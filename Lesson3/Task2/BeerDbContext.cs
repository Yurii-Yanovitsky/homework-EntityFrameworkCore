using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Reflection;

namespace Task2
{
    class BeerDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Distributor> Distributors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8KV4L2A\SQLEXPRESS;Initial Catalog=BeerAppDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Beer).Assembly);
            //modelBuilder.Entity<Distributor>()
            //    .Property(b => b.Name)
            //    .IsRequired()
            //    .HasMaxLength(25);

            //modelBuilder.Entity<Distributor>()
            //    .Property(b => b.PhoneNumber)
            //    .IsRequired()
            //    .HasMaxLength(12);

            //modelBuilder.Entity<Beer>()
            //    .Property(b => b.Name)
            //    .IsRequired()
            //    .HasMaxLength(25);

            //modelBuilder.Entity<Beer>()
            //    .HasMany(b => b.Distributors)
            //    .WithMany(d => d.Beers)
            //    .UsingEntity(t => t.ToTable("BeerDistributorTable"));
        }

        public void ApplyConfigurationsFromAssembly(Assembly assambly)
        {
            var types = assambly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => typeof(IEntityTypeConfiguration<>).IsAssignableFrom(t));

            foreach (var t in types)
            {
                var entityBuilder = new ModelBuilder().Entity(t);

                t.GetMethod("Configure").Invoke(null, new[] { entityBuilder });
            }
        }

        class BeerIEntityTypeConfiguration : IEntityTypeConfiguration<Beer>
        {
            public void Configure(EntityTypeBuilder<Beer> builder)
            {
                builder.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            }
        }
        class DistributorIEntityTypeConfiguration : IEntityTypeConfiguration<Distributor>
        {
            public void Configure(EntityTypeBuilder<Distributor> builder)
            {

                builder.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                builder.Property(b => b.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);
            }
        }
    }
}
