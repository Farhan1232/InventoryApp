using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "User"); // redirect to product list or confirmation
            }
            return View(feedback);
        }

        // Admin view: GET: Feedback/List
        public async Task<IActionResult> List()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            return View(feedbacks);
        }
    }
}
