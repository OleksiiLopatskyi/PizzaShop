using FillPizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.ViewModels
{
    public class OrderViewModel
    {
        public List<ShopCart> Cart { get; set; }
        public UserModel User{ get; set; }
    }
}
