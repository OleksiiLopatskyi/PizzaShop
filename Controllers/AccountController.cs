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

namespace FillPizzaShop.Controllers
{
    public class AccountController : Controller
    {
        private PizzaContext db;
        public AccountController(PizzaContext shoppingContext)
        {
            db = shoppingContext;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccounts user = await db.UserAccounts.Include(u => u.Role)
                    .FirstOrDefaultAsync(i => i.Name == model.Login || i.Email == model.Login || i.Phone==model.Login && i.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    if (user.RoleId == 1)
                        return RedirectToAction("Index", "Admin");
                    else return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Login or(and) password is(are) incorrect");
            }
            return View(model);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccounts user = await db.UserAccounts.FirstOrDefaultAsync(u => u.Name == model.Name || u.Email == model.Email);
                if (user == null)
                {
                    user = new UserAccounts
                    {
                        Email = model.Email,
                        Phone = model.Phone,
                        Name = model.Name,
                        Password = model.Password,
                        Type = UserType.regular
                    };
                    Role role = await db.Roles.FirstOrDefaultAsync(i => i.Name == "user");
                    if (role != null)
                        user.RoleId=2;

                    db.UserAccounts.Add(user);
                    await db.SaveChangesAsync();
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Incorrect login and(or)password");
            }
            return View(model);
        }
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
