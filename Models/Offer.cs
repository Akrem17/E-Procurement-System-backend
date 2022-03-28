namespace E_proc.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Tender Tender {get; set ;}
        public Supplier Supplier { get; set; }


 
    } 
}
