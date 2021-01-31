using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyDbContext _context = new MyDbContext();
        public MainWindow()
        {
            InitializeComponent();
            _context.Users.Load();
            userDataGrid.ItemsSource = _context.Users.Local.ToObservableCollection();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _context.SaveChangesAsync();
            userDataGrid.Items.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _context.Dispose();
        }
    }
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8KV4L2A\SQLEXPRESS; Initial Catalog = UserDB; Integrated Security = true");
        }
    }
}
