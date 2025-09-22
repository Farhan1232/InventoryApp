using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace InventoryApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                _context.AuditLogs.Add(new AuditLog
                {
                    Action = "Add",
                    ProductId = product.Id,
                    UserId = User.Identity.Name ?? "User",
                    Timestamp = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    _context.AuditLogs.Add(new AuditLog
                    {
                        Action = "Update",
                        ProductId = product.Id,
                        UserId = User.Identity.Name ?? "User",
                        Timestamp = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == product.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _context.AuditLogs.Add(new AuditLog
            {
                Action = "Delete",
                ProductId = id,
                UserId = User.Identity.Name ?? "User",
                Timestamp = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: User/About
        public IActionResult About()
        {
            return View();
        }
    }
}
