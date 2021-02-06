using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MyApplyConfigurationsApp
{
    class ChatContextDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContextDB()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8KV4L2A\SQLEXPRESS;Initial Catalog=MessageAppDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var myModelBuilder = new MyModelBuilder(modelBuilder);
            myModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
