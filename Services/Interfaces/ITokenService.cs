namespace E_proc.Services
{
    public interface ITokenService
    {

        public string? ValidateJwtToken(string token);
    }
}
