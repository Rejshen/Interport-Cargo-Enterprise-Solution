using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Officer
{
    public class RequestsModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public RequestsModel(InterportCargoDBContext db) { _db = db; }

        public List<QuotationRequest> Requests { get; set; } = new();
        public void OnGet()
        {
            Requests = _db.QuotationRequests.ToList();
        }

        public IActionResult OnPostAccept(int id)
        {
            var req = _db.QuotationRequests.FirstOrDefault(x => x.RequestId == id);
            if (req != null)
            {
                req.Status = "Accepted";
                _db.SaveChanges();
                TempData["Msg"] = $"Request #{id} set to Accepted.";
                
            }
            return RedirectToPage();
        }

        public IActionResult OnPostReject(int id)
        {
            var req = _db.QuotationRequests.FirstOrDefault(x => x.RequestId == id);
            if (req != null)
            {
                req.Status = "Rejected";
                //  New: create a customer-visible message on rejection (new added)
                _db.CustomerMessages.Add(new CustomerMessage
                {
                    RequestId = req.RequestId,
                    CustomerEmail = req.CustomerEmail,
                    MessageText = $"Your quotation request (ID #{req.RequestId}) was rejected by the Quotation Officer. " +
                                  "Please review your details and resubmit if necessary."
                });

                _db.SaveChanges();
                TempData["Msg"] = $"Request #{id} set to Rejected.";
            }
            return RedirectToPage();
        }
    }
}
