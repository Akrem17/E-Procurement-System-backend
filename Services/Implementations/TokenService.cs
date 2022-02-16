using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_proc.Services
{
    public class TokenService : ITokenService
    {
		private readonly IHttpContextAccessor _context;
        public TokenService(IHttpContextAccessor context )
        {

				_context = context;
			

        }
		public string? ValidateJwtToken(string token)
        {

			var mySecret = "?v=f2IdQqpjR0c&ab_channel=CodewithJulian";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));

			var myIssuer = "https://localhost:7260/";
			var myAudience = "https://localhost:7260/";

			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = myIssuer,
					ValidAudience = myAudience,
					IssuerSigningKey = mySecurityKey
				}, out SecurityToken validatedToken);
				var res=principal.Claims.First().Value;
				return res;

			}
			catch
			{
				return null;
			}
		}
    }
}
