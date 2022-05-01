using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_proc.Models
{
    public class FileData
    {

        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string MimeType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }

        [ForeignKey("Tender")]
       
        public int ? TenderId { get; set; }
      
        [JsonIgnore]
        public virtual Tender? Tender { get; set; }

        [ForeignKey("Offer")]

        public int? OfferId { get; set; }

        [JsonIgnore]
        public virtual Offer? Offer { get; set; }
     





    }
}
