using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Quotation
{
    public class DetailsModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public DetailsModel(InterportCargoDBContext db) => _db = db;

        
        public Models.Quotation? Quote { get; set; }

        [BindProperty]
        public string? RejectReason { get; set; }

        public IActionResult OnGet(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/Users/Login");

            Quote = _db.Quotations.FirstOrDefault(q => q.QuotationId == id && q.CustomerEmail == email);
            if (Quote == null) return RedirectToPage("/Quotation/MyQuotations");

            return Page();
        }

        public IActionResult OnPostAccept(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/Users/Login");

            var q = _db.Quotations.FirstOrDefault(x => x.QuotationId == id && x.CustomerEmail == email);
            if (q == null) return RedirectToPage("/Quotation/MyQuotations");

            q.CustomerResponse = "Accepted";
            q.Status = "Customer Accepted";

            _db.OfficerNotifications.Add(new OfficerNotification // Create notification for officers
            {
                QuotationId = q.QuotationId,
                CustomerEmail = q.CustomerEmail,
                Action = "Accepted",
                Note = null
            });


            _db.SaveChanges();

            TempData["Success"] = $"You accepted Quotation #{id}.";
            return RedirectToPage("/Quotation/MyQuotations");
        }

        public IActionResult OnPostReject(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/Users/Login");

            var q = _db.Quotations.FirstOrDefault(x => x.QuotationId == id && x.CustomerEmail == email);
            if (q == null) return RedirectToPage("/Quotation/MyQuotations");

            q.CustomerResponse = "Rejected";
            q.Status = "Customer Rejected";
            if (!string.IsNullOrWhiteSpace(RejectReason))
                q.Scope += $" (Customer note: {RejectReason})"; // simple prototype note

            _db.OfficerNotifications.Add(new OfficerNotification // Create notification for officers
            {
                QuotationId = q.QuotationId,
                CustomerEmail = q.CustomerEmail,
                Action = "Rejected",
                Note = RejectReason
            });

            _db.SaveChanges();

            TempData["Success"] = $"You rejected Quotation #{id}.";
            return RedirectToPage("/Quotation/MyQuotations");


        }
    }
}
