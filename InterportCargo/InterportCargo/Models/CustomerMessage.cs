using System.ComponentModel.DataAnnotations;
namespace InterportCargo.Models
{
    /// <summary>
    /// Represents a message sent from an officer to a customer,
    /// typically when a quotation request is rejected or updated.
    /// </summary>
    public class CustomerMessage
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public int RequestId { get; set; }

        [Required, EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required, StringLength(300)]
        public string MessageText { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
