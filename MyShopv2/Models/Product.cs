using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Product()
        {
            this.Discounts = new HashSet<Discount>();
            this.Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string imageURL { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }


        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Category Category { get; set; }
    }
}
