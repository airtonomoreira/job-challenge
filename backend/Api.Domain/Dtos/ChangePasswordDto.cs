using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
