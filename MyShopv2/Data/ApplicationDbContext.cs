using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
<<<<<<< HEAD
           optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=MyShop;Trusted_Connection=True;MultipleActiveResultSets=true");
=======
            optionsBuilder.UseSqlServer("Server=DESKTOP-EQA7TFT\\SYNGUYEN;Database=MyShop;Trusted_Connection=True;MultipleActiveResultSets=true");
>>>>>>> 51768aa937df7f8f68042a564d66f59fdf9573da
        }
    }
}
