using E_proc.Models;

namespace E_proc.Services
{
    public interface ITokenService
    {

        public string GenerateTokenString(User user);
        public string? ValidateJwtToken(string token);
    }
}
