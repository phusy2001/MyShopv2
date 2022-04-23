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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShopv2.Pages
{
    /// <summary>
    /// Interaction logic for CouponManagement.xaml
    /// </summary>
    public partial class CouponManagement : Page
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private CollectionViewSource DiscountViewSource;
        public CouponManagement()
        {
            InitializeComponent();
            DiscountViewSource = (CollectionViewSource)FindResource(nameof(DiscountViewSource));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Discounts.Load();

            // bind to the source
            DiscountViewSource.Source = _context.Discounts.Local.ToObservableCollection();
        }

        private void addDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            var addCoupon = new addDiscount();
            DiscountListView.Items.Refresh();
        }
        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new addOrder();
            addOrderWindow.ShowDialog();
            DiscountListView.Items.Refresh();
            var ketqua = from order in _context.Orders
                         where order.UserID == 1
                         select order;
            foreach (var order in ketqua)
                Console.WriteLine(order.ToString());
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new addOrder();
            addOrderWindow.ShowDialog();
            DiscountListView.Items.Refresh();
            var ketqua = from order in _context.Orders
                         where order.UserID == 1
                         select order;
            foreach (var order in ketqua)
                Console.WriteLine(order.ToString());
        }
    }
}