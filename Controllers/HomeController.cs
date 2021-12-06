using FillPizzaShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PizzaContext _db;

        public HomeController(ILogger<HomeController> logger,PizzaContext context)
        {
            _logger = logger;
            _db = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.TextIndex = "Hello " + User.Identity.Name;

            return View();
        }
        public IActionResult Discount()
        {
            return View(_db.Products.Where(i=>i.HasDiscount));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
