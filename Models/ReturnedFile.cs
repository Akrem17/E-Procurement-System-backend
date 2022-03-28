namespace E_proc.Models
{
    public class ReturnedFile
    {
        public MemoryStream memory { get; set; }
        public string contentType { get; set; }
        
        public string fileName { get; set; }

    }
}
