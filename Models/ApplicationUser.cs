namespace InventoryApp.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Manager, User
    }
}
