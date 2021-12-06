using FillPizzaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Controllers
{
    public class ProductsController : Controller
    {
        private PizzaContext _db;
        public ProductsController(PizzaContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var user = _db.UserAccounts.FirstOrDefault(i=>i.Email==User.Identity.Name);
            var Products = _db.Products.ToList();
            if (User.HasClaim(i => i.Type == "userType" && i.Value == "golden"))
            {
                float discount = 0;
                foreach (var item in Products.Where(i=>i.HasDiscount))
                {
                    discount = (item.Price * item.Discount) / 100;
                    item.Price -= discount;
                }
                return View(Products);
            }
            else return View(Products);
            
        }
        [HttpGet]
        public async Task<IActionResult> Details(int?id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (User.HasClaim(i => i.Type == "userType" && i.Value == "golden")&&product.HasDiscount)
            {
                product.Price = product.Price-(product.Price * product.Discount / 100);
            }
            return View(await _db.Products.FirstOrDefaultAsync(i=>i.Id==id));
        }
        [HttpPost]
        public async Task<IActionResult> Details(Product product)
        {
            var findProduct = await _db.Products.FirstOrDefaultAsync(i=>i.Id==product.Id);          
            findProduct.Salt = product.Salt;
            findProduct.Cheese = product.Cheese;
            ShopCart shopCart = new ShopCart
            {
                Product = findProduct,
                OrderId = _db.Orders.OrderBy(i => i).LastOrDefault().Id,
                TotalPrice=findProduct.Price
            };

            var ShopCardContain = _db.ShopCart.Include(i => i.Product).FirstOrDefault(i => i.Product.Name == findProduct.Name);

            if (ShopCardContain != null)
            {
                ShopCardContain.Count++;
                _db.ShopCart.Update(ShopCardContain);
                _db.SaveChanges();
            }
            else
            {
                shopCart.Count = 1;
                _db.ShopCart.Add(shopCart);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");

        }   
    }
}
