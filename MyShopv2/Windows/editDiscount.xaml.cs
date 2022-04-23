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
    /// Interaction logic for editDiscount.xaml
    /// </summary>
    public partial class editDiscount : Window
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public editDiscount(string name, string code, float percent, string start, string end, int limit, int quantity)
        {
            InitializeComponent();
            discountName.Text = name;
            discountCode.Text = code;
            discountPercent.Text = percent.ToString();
            startdDay.Text = start;
            endDay.Text = end; 
            limitation.Text = limit.ToString();
            maxQuantity.Text = quantity.ToString();
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Orders.Load();

            // bind to the source
        }

        private async void editDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tarDis = await _context.Discounts.FindAsync(discountCode); //Pass id tarUser needed updated
                if (tarDis != null)
                {
                    tarDis.Name = discountName.Text;
                    tarDis.CouponCode = discountCode.Text;
                    tarDis.DiscountPercentage = float.Parse(discountPercent.Text);
                    tarDis.StartDate = startdDay.Text;
                    tarDis.EndDate = endDay.Text;
                    tarDis.LimitationTimes = Int32.Parse(limitation.Text);
                    tarDis.MaximumDiscountedQuantity = Int32.Parse(maxQuantity.Text);
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
