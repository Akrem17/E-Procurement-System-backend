using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class VerifyCodeModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
