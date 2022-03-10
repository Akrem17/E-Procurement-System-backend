using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    [JsonObject(IsReference = true)]


    public class User
    {
   
        [Key]
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Lastname")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please Validate the email")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Type { get; set; }

        [DefaultValue(false)]
        public bool EmailConfirmed { get; set; }



    
        public string? createdAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
   
        public string? updatedAt { get; set; }


        
         
        
        public bool IsConfirmed()
        {
            return EmailConfirmed;
        }

    }
}
