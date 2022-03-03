using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class ResetPasswordModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "New Password is required")]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = " ConfirmPassword is required")]

        public string ConfirmPassword { get; set; }
        public string Token { get; set; }




    }
}
