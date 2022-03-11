using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class VerifyToken
    {

        public string Token { get; set; }
        [Required]
            [EmailAddress]
            public string Email { get; set; }
           
        

}
}
