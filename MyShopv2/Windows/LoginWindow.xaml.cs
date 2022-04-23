using MyShop.Data;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using MyShop.Models;
using System.Configuration;
using System.Collections.Generic;

namespace MyShopv2.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        MainWindow mainWindow = new MainWindow();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private User _user = new User()
        {
            Id = 1,
            UserName = "admin",
            Password = "e10adc3949ba59abbe56e057f20f883e",
            IsAdmin = true,
            IsDeleted = false,
            CreatedAt = "22/04/2022",
            UpdatedAt = "22/04/2022"
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(GetConfiguration("Username") != "" && GetConfiguration("Password") != "")
            {
                this.Close();
                mainWindow.Show();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            byte[] password = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
            
            //var user = _context.Users.SingleOrDefault(x => x.UserName == username);
            var user = _user;

            var hashPassword = MD5.HashData(password);

            if (user == null)
            {
                MessageBox.Show("Đăng nhập không thành công");
                return;
            }


            if (user.Password == Convert.ToHexString(hashPassword).ToLower())
            {
                // do something (correct password)
                if (user.IsAdmin )
                {
                    if(ckbSave.IsChecked == true)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        AddUpdateAppSettings("Username", user.UserName);
                        AddUpdateAppSettings("Password", user.Password);
                        this.Close();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        this.Close();
                        mainWindow.Show();
                    }
                }
            }
            else
            {
                // something else (incorrect password)
                MessageBox.Show("Đăng nhập không thành công");
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "App.config";
                Configuration configFile = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public string GetConfiguration(string key)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "App.config";
            Configuration configFile = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, "");
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            string ans = configFile.AppSettings.Settings[key].Value;
            return ans;
        }
    }
}
