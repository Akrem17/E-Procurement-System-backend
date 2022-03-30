using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_proc.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMontant { get; set; }
        public int? FinalTotalMontant { get; set; }

        public bool? isAccepted { get; set; }

        public virtual ICollection<FileData>? Files { get; set; }


        [ForeignKey("Tender")]
        public int TenderId { get; set; }

        [JsonIgnore]
        public virtual Tender? Tender {get; set ;}

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }


 
    } 
}
