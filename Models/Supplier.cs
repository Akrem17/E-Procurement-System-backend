namespace E_proc.Models
{
    public class Supplier:User
    {
        public string SupplierName { get; set; }
        public string Category { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string TaxId { get; set; }
        public string CNSSId { get; set; }
        public string BuisnessClassification { get; set; }
        public string BuisnessType { get; set; }
        public string ReplyEmail { get; set; }
        public string CompanyName { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
         public virtual Representative? representative { get; set; }
         public virtual Licence? licence { get; set; }
         public virtual Address? address { get; set; }





    }
}
