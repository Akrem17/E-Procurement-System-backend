namespace E_proc.Services.Interfaces
{
    public interface IEncryptionService
    {
        public  string Encrypt(string text);
        public  string Decrypt(string criptedtext);


    }
}
