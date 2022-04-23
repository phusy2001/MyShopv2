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
        List<Product> products = new List<Product>();
        List<Product> ProductsListToView = new List<Product> { };
        List<Product> SelectedProducts = new List<Product> { };

        int _totalItems = 0;
        int _currentPage = 0;
        int _totalPages = 0;
        int _rowsPerPage = 5;

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

            foreach(var C in products)
            {
                ProductsListToView.Add(C);
            }
            _currentPage = 1;
            _totalItems = products.Count;
            _totalPages = _totalItems / _rowsPerPage + (_totalItems % _rowsPerPage == 0 ? 0 : 1);
            List<int> NumOfProductPerpage = new List<int>() { 3, 5, 10 };
            NumOfProductsPerPageCombobox.ItemsSource = NumOfProductPerpage;
            PagesTextBlock.Text = $"{_currentPage}/{_totalPages}";

            SelectedProducts = products.Skip((_currentPage - 1) * _rowsPerPage).Take(_rowsPerPage).ToList();

            // bind to the source
            ProductViewSource.Source = SelectedProducts;
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

            ProductsListToView.Clear();

            foreach(var c in products)
            {
                if(c.CategoryID == catID)
                {
                    ProductsListToView.Add(c);
                }
            }

            update_productToShowOnScreen();
        }

        private void PrevProduct_pagesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                PagesTextBlock.Text = $"{_currentPage}/{_totalPages}";
                SelectedProducts = ProductsListToView.Skip((_currentPage - 1) * _rowsPerPage).Take(_rowsPerPage).ToList();
                ProductViewSource.Source = SelectedProducts;
            }
        }

        private void NextProduct_pagesBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            PagesTextBlock.Text = $"{_currentPage}/{_totalPages}";
            SelectedProducts = ProductsListToView.Skip((_currentPage - 1) * _rowsPerPage).Take(_rowsPerPage).ToList();
            ProductViewSource.Source = SelectedProducts;
        }

        private void NumOfProductsPerPageCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _rowsPerPage = (int)NumOfProductsPerPageCombobox.SelectedItem;
            update_productToShowOnScreen();
        }

        public void update_productToShowOnScreen()
        {
            _totalItems = ProductsListToView.Count;
            _totalPages = _totalItems / _rowsPerPage + (_totalItems % _rowsPerPage == 0 ? 0 : 1);
            _currentPage = 1;
            PagesTextBlock.Text = $"{_currentPage}/{_totalPages}";
            SelectedProducts = ProductsListToView.Skip((_currentPage - 1) * _rowsPerPage).Take(_rowsPerPage).ToList();
            ProductViewSource.Source = SelectedProducts;
        }

        private void SearchProduct_btn_Click(object sender, RoutedEventArgs e)
        {
            var prodname = SearchProd_TextBox.Text.Trim().ToLower();
            SearchProd_TextBox.Text = "Search product name";
            ProductViewSource.Source = from product in products
                                       where product.Name.ToLower() == prodname.ToLower()
                                       select product;
        }
    }
}
