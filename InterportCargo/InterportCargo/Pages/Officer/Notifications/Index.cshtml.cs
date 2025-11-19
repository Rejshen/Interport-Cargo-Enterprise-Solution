using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Officer.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public IndexModel(InterportCargoDBContext db) => _db = db;

        public List<OfficerNotification> Items { get; set; } = new();

        bool IsOfficer()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "QuotationOfficer" || role == "Admin" || role == "Manager";
        }

        public IActionResult OnGet()
        {
            if (!IsOfficer()) { TempData["Error"] = "Officer access only."; return RedirectToPage("/Index"); }

            Items = _db.OfficerNotifications
                       .OrderByDescending(n => n.CreatedAt)
                       .Take(50)
                       .ToList();
            return Page();
        }

        public IActionResult OnPostMarkRead(int id)
        {
            if (!IsOfficer()) return RedirectToPage("/Index");

            var n = _db.OfficerNotifications.FirstOrDefault(x => x.Id == id);
            if (n != null)
            {
                n.IsRead = true;
                _db.SaveChanges();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostMarkAllRead()
        {
            if (!IsOfficer()) return RedirectToPage("/Index");

            var unread = _db.OfficerNotifications.Where(x => !x.IsRead).ToList();
            foreach (var n in unread) n.IsRead = true;
            _db.SaveChanges();
            return RedirectToPage();
        }
    }
}
