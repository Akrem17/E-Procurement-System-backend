namespace E_proc.Models
{
    public class EmailConfig
    {

        public int Id { get; set; }
        public string From { get; set; }

        public int Port { get; set; }
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
