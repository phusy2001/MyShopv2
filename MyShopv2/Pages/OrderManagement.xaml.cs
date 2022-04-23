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
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Page
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private CollectionViewSource OrderViewSource;
        public OrderManagement()
        {
            InitializeComponent();
            OrderViewSource = (CollectionViewSource)FindResource(nameof(OrderViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Orders.Load();

            // bind to the source
            OrderViewSource.Source = _context.Orders.Local.ToObservableCollection();
        }

        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new addOrder();
            addOrderWindow.ShowDialog();
            var ketqua = from order in _context.Orders
                         where order.UserID == 1
                         select order;
            foreach (var order in ketqua)
                Console.WriteLine(order.ToString());
        }
    }
}
