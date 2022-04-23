using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyShopv2.Pages
{
    /// <summary>
    /// Interaction logic for addOrder.xaml
    /// </summary>
    public partial class addOrder : Window
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        
        public addOrder()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Orders.Load();

            // bind to the source
            
        }

        private void addOrderClick(object sender, TextChangedEventArgs e)
        {

        }
        private void cancelClick(object sender, TextChangedEventArgs e)
        {

        }

        private async void addOrderBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            int customerID = Int32.Parse(userName.Text);
            var orderStatus = status.Text;
            int orderAddres = Int32.Parse(address.Text);
            var createdAt = createdDay.Text;
            var productid = productID.Text;
            //var newProduct = await _context.Products.FindAsync(productid);
            var newOrder = new Order()
            {
                UserID = customerID,
                Status = orderStatus,
                ShippingAddressID = orderAddres,
                CreatedAt = "23/04/2022",
                UpdatedAt = "23/04/2022"
            };

            try
            {
                if (true) //Need handle user input
                {
                    await _context.Orders.AddAsync(newOrder);
                    _context.SaveChanges();
                    //this.userDataGrid.Items.Refresh();
                }
                else
                {
                    //Show optional alert
                    MessageBox.Show("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addDiscountBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
