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
    /// Interaction logic for editProductWindow.xaml
    /// </summary>
    public partial class editProductWindow : Window
    {
        public delegate void ProductIDChangedHandler(int NewID);
        public event ProductIDChangedHandler IDChanged;

        public delegate void NameChangedHandler(string NewName);
        public event NameChangedHandler NameChanged;

        public delegate void ProductDescriptionChangedHandler(string NewDescription);
        public event ProductDescriptionChangedHandler DescriptionChanged;

        public delegate void ProductPriceChangedHandler(int NewPrice);
        public event ProductPriceChangedHandler PriceChanged;

        public delegate void ProductQuantityChangedHandler(int NewQuantity);
        public event ProductQuantityChangedHandler QuantityChanged;

        public delegate void ProductCategoryIDChangedHandler(int NewCategoryID);
        public event ProductCategoryIDChangedHandler CategoryIDChanged;

        public int NewID { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public int NewPrice { get; set; }
        public int NewQuantity { get; set; }
        public int NewCategoryID { get; set; }

        public editProductWindow(int ID, string Name, string Description, int Quantity, int CategoryID, int Price)
        {
            InitializeComponent();
            NewID = ID;
            NewName = Name;
            NewDescription = Description;
            NewPrice = Price;
            NewQuantity = NewQuantity;
            NewCategoryID = CategoryID;
            this.DataContext = this;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            int NewIDVal = Int16.Parse(ProductID.Text);
            if (IDChanged != null)
            {
                IDChanged.Invoke(NewIDVal);
            }

            int NewCategoryID = Int16.Parse(category.Text);
            if (CategoryIDChanged != null)
            {
                CategoryIDChanged.Invoke(NewCategoryID);
            }

            string NewProdName = productName.Text;
            if(NameChanged != null)
            {
                NameChanged.Invoke(NewProdName);
            }

            string NewProdDescript = description.Text;
            if(DescriptionChanged != null)
            {
                DescriptionChanged.Invoke(NewProdDescript);
            }

            int NewProdQuantity = Int16.Parse(quantity.Text);
            if (QuantityChanged != null)
            {
                QuantityChanged.Invoke(NewProdQuantity);
            }

            int NewProdPrice = Int16.Parse(price.Text);
            if (PriceChanged != null)
            {
                PriceChanged.Invoke(NewProdPrice);
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
