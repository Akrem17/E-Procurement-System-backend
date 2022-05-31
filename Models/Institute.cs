using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public string NotificationEmail { get; set; }

        public int addressId { get; set; }
        [ForeignKey("addressId")]
        public virtual Address? address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int interlocutorId { get; set; }
        [ForeignKey("interlocutorId")]
        public virtual Representative? Interlocutor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]

        public virtual ICollection<Tender>? Tender { get;set;}
        [NotMapped]
        public ICollection<Tender>? Tenders
        {
            get;



            set;
        }
        public bool ShouldSerializeTender()
        {
            return false;
        }
        //[NotMapped]
        //public bool isFromInstitute { get; set; } = false;




    }
}
