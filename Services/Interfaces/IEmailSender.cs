namespace E_proc.Models
{
    public interface IEmailSender
    {

        void SendEmail(Mail message);
    }
}
