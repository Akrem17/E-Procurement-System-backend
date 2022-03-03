using E_proc.Models;

namespace E_proc.Services
{
    public interface ITokenService
    {

        public string GenerateTokenString(User user);
        public string? ValidateJwtToken(string token);
        public bool ValidateToken(string token);
        public string GenerateTokenStringPasswordReset(VerifyCodeModel model);
    }
}
