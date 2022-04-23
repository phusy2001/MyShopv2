using Microsoft.EntityFrameworkCore;
using MyShop.Data;
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

namespace MyShopv2.Windows
{
    /// <summary>
    /// Interaction logic for EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        int orderID;
        public EditOrder(int order, int userID, string orderStatus, int userAddress)
        {
            InitializeComponent();
            orderID = order;
            status.Text = orderStatus;
            userName.Text = userID.ToString();
            address.Text = userAddress.ToString();
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Orders.Load();

            // bind to the source
        }

        private async void editOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tarOrder = await _context.Orders.FindAsync(orderID); //Pass id tarUser needed updated
                if (tarOrder != null)
                {
                    tarOrder.UserID = Int32.Parse(userName.Text);
                    tarOrder.Status = status.Text;
                    tarOrder.ShippingAddressID = Int32.Parse(address.Text);
                    tarOrder.UpdatedAt = DateTime.Now.ToString();
                    _context.SaveChanges();
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
    }
}
