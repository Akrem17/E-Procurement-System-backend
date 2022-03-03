using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class UserLogin
    {

        [Key]
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "email not validated")]

        public string Email { get; set; }
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
