using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public UserModel User { get; set; }
       /* public Order()
        {
            ShopCart = new List<ShopCart>();
        }*/
    }
}
