using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_proc.Models
{
    public class AskForInfoAnswer
    {
        [Key]
        public int Id   { get; set; }
        public string message { get; set; }

        [ForeignKey("AskForInfo")]
        public int AskForInfoId { get; set; }

        [JsonIgnore]
        public AskForInfo? AskForInfo { get; set; }
        
        public string From { get; set; }

        public bool Seen    { get; set; }=false;

        public long? CreatedAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        


    }
}
