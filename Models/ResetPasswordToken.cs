using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class ResetPasswordToken
    {
        [Required(ErrorMessage = " Email is required"), EmailAddress]
        public string Email { get; set; }
    }
}
