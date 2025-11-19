using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterportCargo.Models
{
    public class QuotationRequest
    {
        [Key] public int RequestId { get; set; }

        // keep it simple: store email string (no FK for prototype)
        [Required, EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Source { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Destination { get; set; } = string.Empty;

        [Range(1, 999)]
        public int ContainerCount { get; set; }

        [Required, StringLength(200)]
        public string PackageNature { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string CustomerName { get; set; } = string.Empty;   


        // job nature
        public bool IsImport { get; set; }
        public bool IsPack { get; set; }
        public bool RequiresQuarantine { get; set; }
        public bool RequiresFumigation { get; set; }

        // basic status text
        [Required]
        public string Status { get; set; } = "Submitted";

        // Models/QuotationRequest.cs
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
