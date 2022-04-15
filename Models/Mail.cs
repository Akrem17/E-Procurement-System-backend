using MimeKit;

namespace E_proc.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }




        public Mail(IEnumerable<string> to, string subject, string content)
        {

            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x,x)));
            Subject = subject;
            Content = content;

        }

    }
}
