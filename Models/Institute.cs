using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{
    public class Institute : User
    {
        [Key]
        public int Id { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string TypeOfInstitute { get; set; }
        public string AreaType { get; set; }

        public string representativeName { get; set; }

        public string NotificationEmail {get;set;}
        public virtual Address? address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public virtual Representative? Interlocutor { get; set; }




    }
}
