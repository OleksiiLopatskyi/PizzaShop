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
        public async Task<IActionResult> Details(int?id)
        {
            return View(await _db.Products.FirstOrDefaultAsync(i=>i.Id==id));
        }
        [HttpGet]
        public async Task<IActionResult> AddToShopCard(int? id)
        {
            var curProduct = await _db.Products.FirstOrDefaultAsync(i => i.Id == id);

            ShopCart shopCart = new ShopCart
            {
                Product = curProduct,
                OrderId = _db.Orders.OrderBy(i=>i).LastOrDefault().Id,
                
            };
          
               var ShopCardContain =  _db.ShopCart.Include(i=>i.Product).FirstOrDefault(i => i.Product.Name == curProduct.Name);

            if (ShopCardContain!=null)
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
           
            return View("Index",_db.Products);
        }
       
    }
}
