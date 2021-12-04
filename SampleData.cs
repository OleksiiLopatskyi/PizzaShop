using FillPizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop
{
    public class SampleData
    {
        public static void Initialize(PizzaContext db)
        {
            if (db.Products.Any()) return;
            var products = new List<Product>() {
            new Product
            {
                Name="Margarita",
                Type=ProductType.Pizza,
                Price=145
            },
             new Product
            {
                Name="Cola",
                Type=ProductType.Drink,
                Price=15
            },
              new Product
            {
                Name="Cesar",
                Type=ProductType.Salat,
                Price=150
            },
            };
            var shopcart = new ShopCart
            {
                Product = products[0],
                Count=1
            };
            var Order = new Order
            {
                OrderDetails = new List<OrderDetail>() {},
                User = new UserModel
                {
                    Name = "oleksiy",
                    Address = "sutuhiv",
                    Phone = "380688303152"
                },
            };
            db.Products.AddRange(products);
            db.SaveChanges();
            db.Orders.AddRange(Order);
            db.SaveChanges();
        }
    }
}
