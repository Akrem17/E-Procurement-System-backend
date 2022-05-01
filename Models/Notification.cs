using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_proc.Models
{
    public class Notification
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("Offer")]

        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        [ForeignKey("Institute")]

        public int InstituteId { get; set; }
        public Institute Institute { get; set; }

        public string message { get; set; }

    }
}
