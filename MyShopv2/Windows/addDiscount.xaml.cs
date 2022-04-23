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
    /// Interaction logic for addDiscount.xaml
    /// </summary>
    public partial class addDiscount : Window
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public addDiscount()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Discounts.Load();

            // bind to the source

        }

        private async void addDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            var newCoupon = new Discount() //create newUser with optional data
            {
                Name = discountName.Text,
                CouponCode = discountCode.Text,
                DiscountPercentage = float.Parse( discountPercent.Text),
                StartDate = "23/04/2022",
                EndDate = "30/04/2022",
                LimitationTimes = Int32.Parse(limitation.Text),
                MaximumDiscountedQuantity = Int32.Parse(quantity.Text),
            };

            try
            {
                if (true) //Need handle user input
                {
                    await _context.Discounts.AddAsync(newCoupon);
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
    }
}
