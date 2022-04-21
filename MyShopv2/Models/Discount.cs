using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Discount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Discount()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public float DiscountPercentage { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int LimitationTimes { get; set; }
        public int MaximumDiscountedQuantity { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
