using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FillPizzaShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace FillPizzaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="admin")]
    public class UserAccountsController : Controller
    {
        private readonly PizzaContext _context;

        public UserAccountsController(PizzaContext context)
        {
            _context = context;
        }

        // GET: Admin/UserAccounts
        public async Task<IActionResult> Index()
        {
            var pizzaContext = _context.UserAccounts.Include(u => u.Role);
            return View(await pizzaContext.ToListAsync());
        }

        // GET: Admin/UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccounts = await _context.UserAccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccounts == null)
            {
                return NotFound();
            }

            return View(userAccounts);
        }

        // GET: Admin/UserAccounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Admin/UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Phone,Name,Password,Type,RoleId")] UserAccounts userAccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", userAccounts.RoleId);
            return View(userAccounts);
        }

        // GET: Admin/UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccounts = await _context.UserAccounts.FindAsync(id);
            if (userAccounts == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", userAccounts.RoleId);
            return View(userAccounts);
        }

        // POST: Admin/UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Phone,Name,Password,Type,RoleId,Discount")] UserAccounts userAccounts)
        {
            if (id != userAccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountsExists(userAccounts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", userAccounts.RoleId);
            return View(userAccounts);
        }

        // GET: Admin/UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccounts = await _context.UserAccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccounts == null)
            {
                return NotFound();
            }

            return View(userAccounts);
        }

        // POST: Admin/UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccounts = await _context.UserAccounts.FindAsync(id);
            _context.UserAccounts.Remove(userAccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountsExists(int id)
        {
            return _context.UserAccounts.Any(e => e.Id == id);
        }
    }
}
