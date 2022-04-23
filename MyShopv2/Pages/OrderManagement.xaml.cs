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

        private async void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderListView.SelectedItem as Order;

            //var book = OrderListView.SelectedCells;

            var result = MessageBox.Show($"Bạn thật sự muốn xóa đơn hàng {selectedItem.Id}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (MessageBoxResult.Yes == result)
            {
                try
                {
                    var tarOrder = await _context.Orders.FindAsync(selectedItem.Id); //Pass id tarUser deleted
                    if (tarOrder != null)
                    {
                        _context.Orders.Remove(tarOrder);
                        _context.SaveChanges();
                        this.OrderListView.Items.Refresh();
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

            var ketqua = from order in _context.Orders
                         where order.UserID == 1
                         select order;
            foreach (var order in ketqua)
                Console.WriteLine(order.ToString());
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderListView.SelectedItem as Order;

            var ketqua = from order in _context.Orders
                         where order.UserID == 1
                         select order;
            foreach (var order in ketqua)
                Console.WriteLine(order.ToString());
        }

        private void orderDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderListView.SelectedItem as Order;
            var orderDetail = new OrderDetail(selectedItem.Id, selectedItem.Status, selectedItem.UserID,selectedItem.ShippingAddressID, selectedItem.CreatedAt, selectedItem.UpdatedAt);
            orderDetail.ShowDialog();
        }

        private void filterByDayBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        int _curPage = 0;

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            var totalOrder = from order in _context.Orders.Local.ToObservableCollection()
                             select order;
            if (totalOrder.Count() == 0)
            {
                return;
            }

            if (_curPage < 1)
            {
                _curPage++;
                OrderViewSource.Source = totalOrder.Skip(_curPage * 5).Take(5).ToList();
            }

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
