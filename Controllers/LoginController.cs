using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace InventoryApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connStr = _configuration.GetConnectionString("inventoryApp");

                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    string query = "SELECT * FROM users WHERE username=@username AND password=@password";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", model.Username);
                    cmd.Parameters.AddWithValue("@password", model.Password);

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        HttpContext.Session.SetString("Username", model.Username);
                        string role = reader["Role"].ToString();
                        HttpContext.Session.SetString("Role", role);

                        if (role == "Admin")
                            return RedirectToAction("Index", "Admin");
                        else if (role == "Manager")
                            return RedirectToAction("Index", "Manager");
                        else
                            return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid credentials";
                    }
                }
            }

            return View();
        }
    }
}
