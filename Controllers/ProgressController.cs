using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Controllers
{
    public class ProgressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
    }
}
