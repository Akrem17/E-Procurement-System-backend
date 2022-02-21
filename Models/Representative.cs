using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Representative
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Position { get; set; }
        public string SocialSecurityNumberDate { get; set; }
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "email not validated")]

        public string Email { get; set; }
    }
}
