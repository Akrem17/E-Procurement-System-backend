using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class UserLogin
    {

        [Key]
        public int Id { get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
            
            
    }
}
