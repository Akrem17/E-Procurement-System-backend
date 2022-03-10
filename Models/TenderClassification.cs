using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_proc.Models
{
    public class TenderClassification
    {

        [Key]
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Description ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Amount ")]
        public string Amount { get; set; }

        public int tenderId { get; set; }

        [ForeignKey("tenderId")]
        [JsonIgnore]
        virtual public Tender? Tender { get; set; }



    }
}
