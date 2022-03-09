using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Supplier : User
    {
        [Required(ErrorMessage = "Please enter SupplierName")]

        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Please enter Category")]

        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter RegistrationNumber")]

        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please enter RegistrationDate")]

        public string RegistrationDate { get; set; }
        [Required(ErrorMessage = "Please enter TaxId")]

        public string TaxId { get; set; }

        [Required(ErrorMessage = "Please enter CNSSId")]

        public string CNSSId { get; set; }

        [Required(ErrorMessage = "Please enter BuisnessClassification")]

        public string BuisnessClassification { get; set; }
        [Required(ErrorMessage = "Please enter BuisnessType")]

        public string BuisnessType { get; set; }
        [EmailAddress(ErrorMessage = "email not validated")]

        public string ReplyEmail { get; set; }
        [Required(ErrorMessage = "Please enter CompanyName")]

        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter Phone")]

        public string Phone { get; set; }

        public string Fax { get; set; }

        [Required(ErrorMessage = "Please enter representative")]

        public virtual Representative? representative { get; set; }

        [Required(ErrorMessage = "Please enter licence")]

        public virtual Licence? licence { get; set; }
        [Required(ErrorMessage = "Please enter address")]

        public virtual Address? address { get; set; }


    }
}
