using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Document number is required")]
        [MaxLength(20, ErrorMessage = "Document number cannot exceed 20 characters")]
        public string DocumentNumber { get; set; }

        [MinLength(1, ErrorMessage = "At least one phone number is required")]
        public List<string> PhoneNumbers { get; set; }

        public Guid? ManagerId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "Employee";

        [Required(ErrorMessage = "Hierarchical level is required")]
        public int HierarchicalLevel { get; set; } // New property for hierarchical level
    }
}
