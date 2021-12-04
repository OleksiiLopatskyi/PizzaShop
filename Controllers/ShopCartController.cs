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
            Order order = new Order
            {
                ShopCart = _db.ShopCart.Include(i=>i.Product).ToList(),
                User = user
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
            _db.ShopCart.Remove(shopCartItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}
