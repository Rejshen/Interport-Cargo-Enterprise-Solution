using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InterportCargo.Pages.Users
{
    public class RegisterEmployeeModel : PageModel
    {

        private readonly InterportCargoDBContext _db;
        public RegisterEmployeeModel(InterportCargoDBContext db) => _db = db;

        [BindProperty]
        public Employee Employee { get; set; } = new();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (_db.Employees.AsNoTracking().Any(e => e.Email == Employee.Email))
            {
                ModelState.AddModelError("Employee.Email", "Email is already registered.");
                return Page();
            }

            _db.Employees.Add(Employee);   // stores plain Password
            _db.SaveChanges();

            TempData["Success"] = "Employee registered. Please log in.";
            return Page();
            //return RedirectToPage("/Users/Login");
        }
    }
}
