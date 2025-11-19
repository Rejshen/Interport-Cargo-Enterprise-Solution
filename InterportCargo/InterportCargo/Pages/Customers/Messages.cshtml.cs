using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Customers
{
    public class MessagesModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public MessagesModel(InterportCargoDBContext db) => _db = db;

        public List<CustomerMessage> Messages { get; set; } = new();
        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/Users/Login");

            Messages = _db.CustomerMessages
                          .Where(m => m.CustomerEmail == email)
                          .OrderByDescending(m => m.CreatedAt)
                          .ToList();

            return Page();
        }

        public IActionResult OnPostMarkRead(int id)
        {
            var msg = _db.CustomerMessages.FirstOrDefault(m => m.Id == id);
            if (msg != null)
            {
                msg.IsRead = true;
                _db.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
