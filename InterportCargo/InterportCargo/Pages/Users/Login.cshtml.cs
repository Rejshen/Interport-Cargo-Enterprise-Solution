using InterportCargo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InterportCargo.Pages.Users
{
    public class LoginModel : PageModel
    {
        public class InputModel
        {
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        [BindProperty] public InputModel Input { get; set; } = new();

        private readonly InterportCargoDBContext _db; // <-- change to InterportCargoDBContext if that's your class name

        public LoginModel(InterportCargoDBContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            // 1) Try Customer (plain-text comparison)
            var cust = _db.Customers.FirstOrDefault(c => c.Email == Input.Email);
            if (cust != null && cust.Password == Input.Password)
            {
                HttpContext.Session.SetString("UserEmail", cust.Email);
                HttpContext.Session.SetString("UserRole", "Customer");
                TempData["Success"] = "Logged in as Customer.";
                return RedirectToPage("/Index");
            }

            // 2) Try Employee (plain-text comparison)
            var emp = _db.Employees.FirstOrDefault(e => e.Email == Input.Email);
            if (emp != null && emp.Password == Input.Password)
            {
                HttpContext.Session.SetString("UserEmail", emp.Email);
                HttpContext.Session.SetString("UserRole", emp.EmployeeType.ToString());
                TempData["Success"] = $"Logged in as {emp.EmployeeType}.";
                return RedirectToPage("/Index");
            }

            // Failed
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }
    }
}
