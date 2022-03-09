using E_proc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace E_proc.Models
{
    public class Tender

    {

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Business Kind")]
        public string BusinessKind { get; set; }

        [Required(ErrorMessage = "Please enter Financing ")]
        public string? Financing { get; set; }

        [Required(ErrorMessage = "Please enter Budget")]
        public string Budget { get; set; }

        [Required(ErrorMessage = "Please enter StartDate")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Please enter Evaluation Method")]
        public string? EvaluationMethod { get; set; }

        [Required(ErrorMessage = "Please Validate the GuaranteeType")]
        public string? GuaranteeType { get; set; }

        [Required(ErrorMessage = "Please enter Departement")]
        public string? Departement { get; set; }



        public int addressReceiptId { get; set; }
        [ForeignKey("addressReceiptId")]
        virtual public Address? AddressReceipt { get; set; }



        public int responsibleId { get; set; }
        [ForeignKey("responsibleId")]
        virtual public Representative Responsible { get; set; }

        public int instituteId { get; set; }
        [ForeignKey("instituteId")]
        virtual public Institute Institute { get; set; }

        [ForeignKey("tenderClassificationId")]
        virtual public ICollection<TenderClassification>? TenderClassification {get; set;}

        //crate specification model later
        public string specificationURL { get; set; }




        [JsonIgnore]
        public string? createdAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        [JsonIgnore]
        public string? updatedAt { get; set; }


    }
}
