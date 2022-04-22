using MyShopv2.Pages;
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

namespace MyShopv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            Main.Content = new Dashboard();
            Main.NavigationService.RemoveBackEntry();
        }

        private void NavigateToUser(object sender, RoutedEventArgs e)
        {
            Main.Content = new UserManagement();
            Main.NavigationService.RemoveBackEntry();
        }

        private void NavigateToOrder(object sender, RoutedEventArgs e)
        {
            Main.Content = new OrderManagement();
            Main.NavigationService.RemoveBackEntry();
        }

        private void NavigateToProduct(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProductManagement();
            Main.NavigationService.RemoveBackEntry();
        }

        private void NavigateToCoupon(object sender, RoutedEventArgs e)
        {
            Main.Content = new CouponManagement();
            Main.NavigationService.RemoveBackEntry();
        }
    }
}
