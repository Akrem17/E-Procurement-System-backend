using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_proc.Models
{
    public class AskForInfo
    {
        [Key]
        public int Id { get; set; }
        public string Information { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public bool Seen { get; set; }=false;

        public bool SendToEmail { get; set; }=false;
        public bool SendToAddress { get; set; }=false;
        public bool SendToChat { get; set; } = true;
        [ForeignKey("Citizen")]
        public int CitizenId { get; set; }
        public Citizen? Citizen { get; set; }
        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        public Tender? Tender { get; set; }


        [ForeignKey("AskForInfoAnswer")]
        public int? AskForInfoAnswerId { get; set; }
        public AskForInfoAnswer? AskForInfoAnswer { get; set; }
        public string? createdAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        public string? updatedAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();




    }
}
