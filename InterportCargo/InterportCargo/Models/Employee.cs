using System.ComponentModel.DataAnnotations;

namespace InterportCargo.Models
{
    public enum EmployeeType
    {
        Admin,
        QuotationOfficer,
        BookingOfficer,
        WarehouseOfficer,
        Manager,
        CIO
    }
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Family name is required.")]
        [StringLength(50, ErrorMessage = "Family name cannot exceed 50 characters.")]
        public string FamilyName { get; set; } 

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Employee type is required.")]
        public EmployeeType EmployeeType { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } 

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; }

        
    }
}
