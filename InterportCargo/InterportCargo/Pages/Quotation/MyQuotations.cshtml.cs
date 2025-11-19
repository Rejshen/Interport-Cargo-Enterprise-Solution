using InterportCargo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Quotation
{
    public class MyQuotationsModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public MyQuotationsModel(InterportCargoDBContext db) => _db = db;

        public List<Models.Quotation> Items { get; set; } = new();

        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Please login first.";
                return RedirectToPage("/Users/Login");
            }

            Items = _db.Quotations
                        .Where(q => q.CustomerEmail == email)
                        .OrderByDescending(q => q.CreatedAt)
                        .ToList();

            return Page();
        }
    }
    
    
}
