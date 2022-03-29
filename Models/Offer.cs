using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Financial")]
        public int FinancialId { get; set; }
        public virtual FileData? Financial { get; set; }

        [ForeignKey("Technical")]
        public int TechnicalId { get; set; }
        public virtual FileData? Technical { get; set; }


        public virtual ICollection<FileData>? Other { get; set; }


        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        public virtual Tender? Tender {get; set ;}

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }


 
    } 
}
