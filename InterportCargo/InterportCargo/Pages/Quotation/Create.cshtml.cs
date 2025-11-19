using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Quotation
{
    public class CreateModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public CreateModel(InterportCargoDBContext db) { _db = db; }

        [BindProperty]
        public QuotationRequest Request { get; set; } = new();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid) return Page();

            _db.QuotationRequests.Add(Request);
            _db.SaveChanges();

            TempData["Success"] = "Quotation request submitted.";
            return RedirectToPage("/Index");
        }
    }
}
