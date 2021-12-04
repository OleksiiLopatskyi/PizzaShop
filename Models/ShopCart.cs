using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public float TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
