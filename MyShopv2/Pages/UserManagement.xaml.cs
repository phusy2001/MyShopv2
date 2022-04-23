using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Page
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private CollectionViewSource UserViewSource;

        public List<User> userList = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "nnpsy",
                    CustomerName = "Nguyễn Ngọc Phú Sỹ",
                    IsAdmin = true,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 2,
                    UserName = "nqbao",
                    CustomerName = "Nguyễn Quốc Bảo",
                    CustomerType = "Loyal member",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 3,
                    UserName = "vqhiep",
                    CustomerName = "Vũ Quang Hiệp",
                    CustomerType = "Normal",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 4,
                    UserName = "nhtbao",
                    CustomerName = "Nguyễn Huỳnh Thế Bảo",
                    CustomerType = "Vip",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 5,
                    UserName = "vqhiep",
                    CustomerName = "Vũ Quang Hiệp",
                    CustomerType = "Normal",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 6,
                    UserName = "nhtbao",
                    CustomerName = "Nguyễn Huỳnh Thế Bảo",
                    CustomerType = "Vip",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 7,
                    UserName = "vqhiep",
                    CustomerName = "Vũ Quang Hiệp",
                    CustomerType = "Normal",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 8,
                    UserName = "nhtbao",
                    CustomerName = "Nguyễn Huỳnh Thế Bảo",
                    CustomerType = "Vip",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 9,
                    UserName = "vqhiep",
                    CustomerName = "Vũ Quang Hiệp",
                    CustomerType = "Normal",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
                new User()
                {
                    Id = 10,
                    UserName = "nhtbao",
                    CustomerName = "Nguyễn Huỳnh Thế Bảo",
                    CustomerType = "Vip",
                    IsAdmin = false,
                    IsDeleted = false,
                    CreatedAt = "22/04/2022",
                    UpdatedAt = "22/04/2022"
                },
            };

        public UserManagement()
        {
            InitializeComponent();
            UserViewSource = (CollectionViewSource)FindResource(nameof(UserViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Products.Load();

           var users =  from user in userList
                        where user.IsAdmin == false
                        select user;
            // bind to the source
            UserViewSource.Source = users;
            
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var customerName = SearchField.Text.Trim().ToLower();
            SearchField.Text = "";
            UserViewSource.Source = from user in userList
                                    where user.IsAdmin == false && user.CustomerName.ToLower() == customerName.ToLower()
                                    select
                                    new
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        CustomerName = user.CustomerName,
                                        CustomerType = user.CustomerType,
                                        IsDeleted = user.IsDeleted,
                                        CreatedAt = user.CreatedAt,
                                        UpdatedAt = user.UpdatedAt,
                                    };
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            UserViewSource.Source = from user in userList
                                    where user.IsAdmin == false
                                    select user;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            var totalUser = from user in userList
                            where user.IsAdmin == false
                            select user;
            if (totalUser.Count() == 0)
            {
                return;
            }
            UserViewSource.Source = totalUser.Skip(1 * 5).Take(5).ToList();
        }

        private void BtnPre_Click(object sender, RoutedEventArgs e)
        {
            var totalUser = from user in userList
                            where user.IsAdmin == false
                            select user;

            if (totalUser.Count() == 0)
            {
                return;
            }

            UserViewSource.Source = totalUser.Skip(0 * 5).Take(5).ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ProductListView.SelectedItem.ToString());
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
