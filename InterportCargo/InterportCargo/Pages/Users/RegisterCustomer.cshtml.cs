using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InterportCargo.Pages.Users
{
    public class RegisterCustomerModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public RegisterCustomerModel(InterportCargoDBContext db) => _db = db;

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (_db.Customers.AsNoTracking().Any(c => c.Email == Customer.Email))
            {
                ModelState.AddModelError("Customer.Email", "Email is already registered.");
                return Page();
            }

            _db.Customers.Add(Customer);   // stores plain Password
            _db.SaveChanges();

            TempData["Success"] = "Registration successful. Please log in.";
            return Page();
            //return RedirectToPage("/Users/Login");
        }
    }

}
