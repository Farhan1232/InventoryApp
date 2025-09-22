using Microsoft.AspNetCore.Mvc;
using InventoryApp.Data;
using InventoryApp.Models;
using System.Linq;

namespace InventoryApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Progress()
        {
            var progress = _context.AuditLogs
                .GroupBy(x => x.UserId)
                .Select(g => new ProgressViewModel
                {
                    UserId = g.Key,
                    AddCount = g.Count(x => x.Action == "Add"),
                    UpdateCount = g.Count(x => x.Action == "Update"),
                    DeleteCount = g.Count(x => x.Action == "Delete")
                }).ToList();

            return View(progress);
        }

        // Optional: redirect /Manager to /Manager/Progress
        public IActionResult Index()
        {
            return RedirectToAction("Progress");
        }
    }
}
