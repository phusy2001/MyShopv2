using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyShopv2.Pages
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Page
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private CollectionViewSource ProductViewSource;

        public ProductManagement()
        {
            InitializeComponent();
            ProductViewSource = (CollectionViewSource)FindResource(nameof(ProductViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "San pham thu 1",
                    Description = "Mo ta san pham",
                    Price = 2000000,
                    imageURL = "No image",
                    Quantity = 5,
                    CategoryID = 1,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                }
            };
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Products.Load();

            // bind to the source
            ProductViewSource.Source = _context.Products.Local.ToObservableCollection();
        }

    }
}
