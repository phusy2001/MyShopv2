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
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Window
    {
        public OrderDetail(int orderId, string orderStatus, int userID, int userAddress, string createDay, string updateDay)
        {
            InitializeComponent();
            orderID.Text = orderId.ToString();
            status.Text = orderStatus;
            user.Text = userID.ToString();
            address.Text = userAddress.ToString();
            createdDate.Text = createDay;
            updatedDate.Text = updateDay;
        }
    }
}
