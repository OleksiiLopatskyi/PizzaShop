using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public string ProductAdditionals { get; set; }
        public float Price { get; set; }
        public int OrderId { get; set; }
    }
}
