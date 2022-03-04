using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Citizen : User
    {
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Phone number is not validated")]
        public string Phone { get; set; }

        public string? CIN { get; set; }

        public Citizen()
        {

         this.createdAt=   new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        }

    }
}
