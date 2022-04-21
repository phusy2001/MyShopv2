using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Order()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Status { get; set; }
        public int UserID { get; set; }
        public int ShippingAddressID { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        
        public User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
