using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace E_proc.Models
{
    [JsonObject(IsReference = true)]


    public class User
    {
        [Key]
        public int Id { get; set; }
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public string Email { get; set; }

        public string Password { get; set; }
 
        public string Type { get; set; }




    }
}
