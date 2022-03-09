using E_proc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Tender
    {
        [Key]
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

        [Required(ErrorMessage = "Please enter AddressReceipt")]
        virtual public Address? AddressReceipt { get; set; }

        [Required(ErrorMessage = "Please enter responsible")]
        virtual public Representative Responsible { get; set; }

        [Required(ErrorMessage = "Please enter institute")]
        virtual public Institute Institute { get; set; }

        virtual public ICollection<TenderClassification>? TenderClassification {get; set;}

        //crate specification model later
        public string specificationURL { get; set; }





        public string? createdAt { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        public string? updatedAt { get; set; }


    }
}
