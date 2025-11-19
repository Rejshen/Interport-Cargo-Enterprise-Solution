using System.ComponentModel.DataAnnotations;
namespace InterportCargo.Models
{
    public class Quotation
    {
        [Key] 
        public int QuotationId { get; set; }

        [Required] 
        public int RequestId { get; set; }

        [Required, EmailAddress] 
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required, StringLength(120)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, StringLength(10)] 
        public string ContainerSize { get; set; } = "20ft";

        [Required, StringLength(300)] 
        public string Scope { get; set; } = string.Empty; // description of the job

        // Money fields
        public decimal Subtotal { get; set; }
        public decimal Gst { get; set; }        // 10%
        public decimal Total { get; set; }      // Subtotal + Gst

        [Required] 
        public string Status { get; set; } = "Draft"; // later can be "Sent"
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required, StringLength(20)]
        public string CustomerResponse { get; set; } = "Pending";
        // could be: Pending / Accepted / Rejected
    }
}
