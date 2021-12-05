using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FillPizzaShop.Models;
using FillPizzaShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FillPizzaShop.Areas.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private PizzaContext db;
        public AdminAccountController(PizzaContext shoppingContext)
        {
            db = shoppingContext;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Login == "admin"|| model.Login == "admin@gmail.com")
                {
                    UserAccounts user = await db.UserAccounts.FirstOrDefaultAsync(i => i.Name == model.Login ||
                    i.Email == model.Login);
                    await Authenticate(user);
                    return RedirectToAction("Index","Home");
                }
                else ModelState.AddModelError("", "Login or(and) password is(are) incorrect");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        private async Task Authenticate(UserAccounts user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim("userType", user.Type.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                                                   ClaimsIdentity.DefaultNameClaimType,
                                                   ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
