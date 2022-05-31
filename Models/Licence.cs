using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Licence
    {
        [Key]
       
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string AcquisitionDate { get; set; }
        public string ExpirationDate { get; set; }
        public string IssuingInstitutionName { get; set; }



    }
}
