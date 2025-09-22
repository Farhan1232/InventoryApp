using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Request> Requests { get; set; }

        // ✅ Add this line for Feedback
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
