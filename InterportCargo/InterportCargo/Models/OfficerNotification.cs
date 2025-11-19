using System.ComponentModel.DataAnnotations;
namespace InterportCargo.Models
{
    public class OfficerNotification
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public int QuotationId { get; set; }

        [Required, EmailAddress] 
        public string CustomerEmail { get; set; } = string.Empty;

        [Required, StringLength(20)] 
        public string Action { get; set; } = string.Empty;

        [StringLength(300)] 
        public string? Note { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
