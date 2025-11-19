using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using QuotationEntity = InterportCargo.Models.Quotation; // avoid any namespace clash

namespace InterportCargo.Pages.Officer.Quotations
{
    public class CreateModel : PageModel
    {
        private readonly InterportCargoDBContext _db;
        public CreateModel(InterportCargoDBContext db) { _db = db; }

        // Header info
        public QuotationRequest? RequestRow { get; set; }

        // Keep inputs minimal (like your Register page)
        [BindProperty]
        public InputModel Input { get; set; } = new();

        // Show a preview after save
        public QuotationEntity? Preview { get; set; }

        public string? DiscountMessage { get; set; }


        public class InputModel
        {
            public int RequestId { get; set; }

            [Required, StringLength(10)]
            public string ContainerSize { get; set; } = "20ft";

            [Required, StringLength(300)]
            public string Scope { get; set; } = string.Empty;
        }


        private decimal GetDiscountPercent(QuotationRequest req)
        {
            // Rules (pick the highest that applies):
            // >10 containers AND quarantine AND fumigation => 10%
            if (req.ContainerCount > 10 && req.RequiresQuarantine && req.RequiresFumigation)
                return 0.10m;

            // >5 containers AND quarantine AND fumigation => 5%
            if (req.ContainerCount > 5 && req.RequiresQuarantine && req.RequiresFumigation)
                return 0.05m;

            // >5 containers AND (quarantine OR fumigation) => 2.5%
            if (req.ContainerCount > 5 && (req.RequiresQuarantine || req.RequiresFumigation))
                return 0.025m;

            return 0m;
        }


        // Officer-only guard using your Session approach
        private bool IsOfficer()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "QuotationOfficer" || role == "Admin" || role == "Manager";
        }

        public IActionResult OnGet(int requestId)
        {
            if (!IsOfficer())
            {
                TempData["Error"] = "Officer access only.";
                return RedirectToPage("/Index");
            }

            RequestRow = _db.QuotationRequests.FirstOrDefault(r => r.RequestId == requestId);
            if (RequestRow == null || RequestRow.Status != "Accepted")
            {
                TempData["Error"] = "Only Accepted requests can be quoted.";
                return RedirectToPage("/Officer/Requests");
            }

            Input.RequestId = requestId;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IsOfficer())
            {
                TempData["Error"] = "Officer access only.";
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid) return Page();

            RequestRow = _db.QuotationRequests.FirstOrDefault(r => r.RequestId == Input.RequestId);
            if (RequestRow == null || RequestRow.Status != "Accepted")
            {
                TempData["Error"] = "Request not found or not accepted.";
                return RedirectToPage("/Officer/Requests");
            }

            // Sum all rate items for selected size, multiply by container count, add 10% GST
            var items = _db.RateItems.OrderBy(i => i.Id).ToList();
            var perContainer = Input.ContainerSize == "20ft"
                                ? items.Sum(i => i.TwentyFt)
                                : items.Sum(i => i.FortyFt);

            var count = Math.Max(1, RequestRow.ContainerCount);
            var subtotal = perContainer * count;
            var gst = Math.Round(subtotal * 0.10m, 2);
            var total = subtotal + gst;

            var q = new QuotationEntity
            {
                RequestId = RequestRow.RequestId,
                CustomerEmail = RequestRow.CustomerEmail,
                CustomerName = RequestRow.CustomerName,
                ContainerSize = Input.ContainerSize,
                Scope = Input.Scope,
                Subtotal = subtotal,
                Gst = gst,
                Total = total,
                Status = "Draft",
                CreatedAt = DateTime.Now
            };

            _db.Quotations.Add(q);
            _db.SaveChanges();


            // Check for discount eligibility based on the request and show a banner if applicable
            var pct = GetDiscountPercent(RequestRow);
            if (pct > 0)
            {
                // store a simple banner message; we¡¦ll render buttons in the .cshtml
                DiscountMessage = $"Discount available: {(pct * 100):0.#}%";
                // pass the quotation id + percent back when clicking Apply
                ViewData["DiscountPercent"] = pct;
                ViewData["QuotationId"] = q.QuotationId;
            }

            Preview = q;
            TempData["Success"] = $"Quotation #{q.QuotationId} saved (Draft).";
            return Page(); // stay and show preview
        }

        public IActionResult OnPostApplyDiscount(int quotationId, decimal percent)
        {
            // officer guard optional (reuse your IsOfficer() if present)
            var q = _db.Quotations.FirstOrDefault(x => x.QuotationId == quotationId);
            if (q == null) { TempData["Error"] = "Quotation not found."; return RedirectToPage(); }

            // Apply percent to Subtotal, then recompute GST/Total
            var discountAmount = Math.Round(q.Subtotal * percent, 2);
            var newSubtotal = q.Subtotal - discountAmount;
            var gst = Math.Round(newSubtotal * 0.10m, 2);
            var total = newSubtotal + gst;

            q.Subtotal = newSubtotal;
            q.Gst = gst;
            q.Total = total;
            // optional: mark status variant so you can see it in lists
            q.Status = "Draft (Discount Applied)";

            _db.SaveChanges();
            TempData["Success"] = $"Applied {(percent * 100):0.#}% discount to Quotation #{q.QuotationId}.";
            return RedirectToPage(new { requestId = q.RequestId });
        }

        public IActionResult OnPostDeclineDiscount(int quotationId)
        {
            var q = _db.Quotations.FirstOrDefault(x => x.QuotationId == quotationId);
            if (q == null) { TempData["Error"] = "Quotation not found."; return RedirectToPage(); }

            q.Status = "Draft (No Discount)";
            _db.SaveChanges();
            TempData["Success"] = $"Discount declined for Quotation #{q.QuotationId}.";
            return RedirectToPage(new { requestId = q.RequestId });
        }

    }
}
