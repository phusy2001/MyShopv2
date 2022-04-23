using Aspose.Cells;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        List<Category> categories = new List<Category>();
        BindingList<Product> products = new BindingList<Product>();

        public ProductManagement()
        {
            InitializeComponent();
            ProductViewSource = (CollectionViewSource)FindResource(nameof(ProductViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                string filename = screen.FileName;
                Debug.WriteLine(filename);
                var workbook = new Workbook(filename);
                var tabs = workbook.Worksheets;

                foreach (var tab in tabs)
                {
                    var column = 'B';
                    var row = 4;
                    if (tab.Name.Equals("Product"))
                    {
                        _context.Products.Load();
                        var cell = tab.Cells[$"{column}{row}"];

                        while (cell.Value != null)
                        {
                            int id = tab.Cells[$"{ column}{row}"].IntValue;
                            string name = tab.Cells[$"C{row}"].StringValue;
                            string description = tab.Cells[$"D{row}"].StringValue;
                            int price = tab.Cells[$"E{row}"].IntValue;
                            string imageURL = tab.Cells[$"F{row}"].StringValue;
                            int quantity = tab.Cells[$"G{row}"].IntValue;
                            int categoryId = tab.Cells[$"H{row}"].IntValue;
                            string createdAt = tab.Cells[$"I{row}"].StringValue;
                            string updateAt = tab.Cells[$"J{row}"].StringValue;

                            var p = new Product()
                            {
                                Id = id,
                                Name = name,
                                Description = description,
                                Price = price,
                                imageURL = imageURL,
                                Quantity = quantity,
                                CategoryID = categoryId,
                                CreatedAt = createdAt,
                                UpdatedAt = updateAt,
                            };

                            products.Add(p);
                            //_context.Products.Add(p);
                            //_context.SaveChanges();
                            row++;
                            cell = tab.Cells[$"{column}{row}"];
                        }

                    }
                    else if (tab.Name.Equals("Category"))
                    {
                        var cell = tab.Cells[$"{column}{row}"];
                        _context.Categories.Load();

                        while (cell.Value != null)
                        {
                            int id = tab.Cells[$"{ column}{row}"].IntValue;
                            string name = tab.Cells[$"C{row}"].StringValue;
                            string createdAt = tab.Cells[$"D{row}"].StringValue;
                            string updateAt = tab.Cells[$"E{row}"].StringValue;

                            var p = new Category()
                            {
                                Id = id,
                                Name = name,
                                CreatedAt = createdAt,
                                UpdatedAt = updateAt,
                            };

                            categories.Add(p);
                            //_context.Categories.Add(p);
                            //_context.SaveChanges();
                            row++;
                            cell = tab.Cells[$"{column}{row}"];
                        }
                    }
                }
            }

            Category_CbBox.ItemsSource = categories;

            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Products.Load();

            // bind to the source
            ProductViewSource.Source = from product in products
                                       select product;
        }

        private void Category_CbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int catID = 0;
            string cat = Category_CbBox.SelectedItem.ToString();
            foreach (var c in categories)
            {
                if (c.Name == cat)
                {
                    catID = c.Id;
                }
            }
            ProductViewSource.Source = from product in products
                                       where product.CategoryID == catID
                                       select product;
        }

        private void PrevProduct_pagesBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextProduct_pagesBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
