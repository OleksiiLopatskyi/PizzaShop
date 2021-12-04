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
            return View(_db.Products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int?id)
        {
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

            return View("Index", _db.Products);

        }   
    }
}
