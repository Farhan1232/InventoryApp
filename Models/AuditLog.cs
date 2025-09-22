using System;

namespace InventoryApp.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
