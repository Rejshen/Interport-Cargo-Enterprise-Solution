using InterportCargo.Data;
using InterportCargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterportCargo.Pages.Officer
{
    public class RatesModel : PageModel
    {

        private readonly InterportCargoDBContext _db;
        public RatesModel(InterportCargoDBContext db) { _db = db; }

        public List<RateItem> Items { get; set; } = new();
        public void OnGet()
        {
            Items = _db.RateItems.OrderBy(r => r.Id).ToList();
        }
    }
}
