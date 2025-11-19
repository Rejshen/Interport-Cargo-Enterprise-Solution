using System.ComponentModel.DataAnnotations;
namespace InterportCargo.Models
{
    public class RateItem
    {
        [Key] public int Id { get; set; }

        [Required, StringLength(80)]
        public string FeeType { get; set; } = string.Empty;   

        [Range(0, 999999)]
        public decimal TwentyFt { get; set; }   

        [Range(0, 999999)]
        public decimal FortyFt { get; set; }    
    }
}
