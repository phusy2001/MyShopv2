using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShopv2.Windows;
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
            DiscountViewSource.Source = _context.Discounts.Local.ToObservableCollection().Skip(0 * 5).Take(5).ToList();
        }

        private void addDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            var addCoupon = new addDiscount();
            addCoupon.ShowDialog();
            DiscountListView.Items.Refresh();
        }
        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DiscountListView.SelectedItem as Discount;
            var editCouponWindow = new editDiscount(selectedItem.Name, selectedItem.CouponCode, selectedItem.DiscountPercentage, selectedItem.StartDate, selectedItem.EndDate, selectedItem.LimitationTimes, selectedItem.MaximumDiscountedQuantity);
            editCouponWindow.ShowDialog();
            DiscountListView.Items.Refresh();

        }

        private async void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DiscountListView.SelectedItem as Discount;

            //var book = OrderListView.SelectedCells;

            var result = MessageBox.Show($"Bạn thật sự muốn xóa voucher {selectedItem.Name}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (MessageBoxResult.Yes == result)
            {
                try
                {
                    var tarDiscount = await _context.Discounts.FindAsync(selectedItem.Id); //Pass id tarUser deleted
                    if (tarDiscount != null)
                    {
                        _context.Discounts.Remove(tarDiscount);
                        _context.SaveChanges();
                        DiscountListView.Items.Refresh();
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
        }

        int _curPage = 0;
        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            var totalDiscount = from Discount in _context.Discounts.Local.ToObservableCollection() select Discount;
            if (totalDiscount.Count() == 0)
            {
                return;
            }

            if (_curPage > 0)
            {
                _curPage--;
                DiscountViewSource.Source = totalDiscount.Skip(_curPage * 5).Take(5).ToList();
            }

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            var totalDiscount = from Discount in _context.Discounts.Local.ToObservableCollection()
                             select Discount;
            if (totalDiscount.Count() == 0)
            {
                return;
            }

            if (_curPage < 1)
            {
                _curPage++;
                DiscountViewSource.Source = totalDiscount.Skip(_curPage * 5).Take(5).ToList();
            }
        }
    }
}