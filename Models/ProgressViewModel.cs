namespace InventoryApp.Models
{
    public class ProgressViewModel
    {
        // Product stats
        public int TotalProducts { get; set; }
        public int LowStockProducts { get; set; }
        public int OutOfStockProducts { get; set; }

        // Audit log stats
        public string UserId { get; set; }
        public int AddCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
    }
}
