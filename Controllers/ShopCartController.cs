using FillPizzaShop.Models;
using FillPizzaShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Controllers
{
    public class ShopCartController : Controller
    {
        private PizzaContext _db;
        public ShopCartController(PizzaContext context)
        {
            _db = context;
        }
        public  IActionResult Index()
        {
            IEnumerable<ShopCart> shopCarts = _db.ShopCart.Include(i=>i.Product);
            foreach (var item in shopCarts)
            {
                if (User.HasClaim(i => i.Type == "userType" && i.Value == "golden"))
                {
                    item.Product.Price = item.Product.Price - (item.Product.Price * item.Product.Discount / 100);
                }
                item.TotalPrice = item.Count * item.Product.Price;
            }
            return View(shopCarts);
        }
        [HttpGet]
        public IActionResult CreateOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                var findUser = _db.UserAccounts.FirstOrDefault(i=>i.Email==User.Identity.Name);
                return View(new UserModel { 
                Name=findUser.Name,
                Phone=findUser.Phone
                });
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrder(UserModel user)
        {
            float OrderPrice = 0; 
            var shopCart = _db.ShopCart.Include(i => i.Product);
            List<OrderDetail> details = new List<OrderDetail>();
            foreach (var item in shopCart)
            {
                var currentItemPrice = item.TotalPrice * item.Count;
                    var OrderDetailsToAdd = new OrderDetail
                    {
                        ProductName = item.Product.Name,
                        ProductCount = item.Count,
                    };
                if (item.Product.Cheese)
                {
                    OrderDetailsToAdd.ProductAdditionals += "Cheese ";
                    currentItemPrice += 35;
                }
                if (item.Product.Salt)
                {
                    OrderDetailsToAdd.ProductAdditionals += "Salt ";
                    currentItemPrice += 15;
                } 
                 OrderPrice += currentItemPrice-(currentItemPrice*item.Product.Discount/100);
                OrderDetailsToAdd.Price=currentItemPrice;
                details.Add(OrderDetailsToAdd);
            }
         
            Order order = new Order
            {
                Date = DateTime.Now,
                OrderDetails = details,
                User = user,
                OrderPrice = OrderPrice
            };

            _db.Orders.Add(order);
            _db.SaveChanges();
            _db.ShopCart.RemoveRange(_db.ShopCart);
            _db.SaveChanges();
            return RedirectToAction("Index","Products");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var shopCartItem = await _db.ShopCart.FirstOrDefaultAsync(i=>i.Id==id);
            if (shopCartItem.Count > 1)
            {
                shopCartItem.Count -= 1;
                _db.SaveChanges();
            }
            else
            {
                _db.ShopCart.Remove(shopCartItem);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
      
    }
}
