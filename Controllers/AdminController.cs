using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Areas.Controllers
{ 
   public class AdminController : Controller
    {
        public  IActionResult Index()
        {
            return RedirectToRoute("Admin");
        }
    }
}
