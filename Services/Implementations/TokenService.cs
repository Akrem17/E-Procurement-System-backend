using E_proc.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace E_proc.Services
{
    public class TokenService : ITokenService
    {
		private readonly IHttpContextAccessor _context;
		IConfiguration config = new ConfigurationBuilder()
 .AddJsonFile("appsettings.json")
 .Build();

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

		public string GenerateTokenString(User user)
		{
			var claims = new[]
								{
									new Claim(ClaimTypes.Email,user.Email),
									new Claim(ClaimTypes.GivenName,user.FirstName),
									new Claim(ClaimTypes.Surname,user.LastName),
									new Claim(ClaimTypes.Role,user.Type),
									new Claim("Email",user.Email),
									new Claim("Type",user.Type)
								
					};
			var token = new JwtSecurityToken(
								 issuer: config["Jwt:Issuer"],
								 audience: config["Jwt:Audience"],
								 claims: claims,
								 expires: DateTime.UtcNow.AddDays(60),
								notBefore: DateTime.UtcNow,
								 signingCredentials: new SigningCredentials(
									 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])), SecurityAlgorithms.HmacSha256
									)
								 );
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenString;
		}

		public string GenerateTokenStringPasswordReset(VerifyCodeModel model)
		{
			var claims = new[]
								{
									new Claim(ClaimTypes.Email,model.Email),
									new Claim(type: "code", value: model.Code),
									
					};
			var token = new JwtSecurityToken(
								 issuer: config["Jwt:Issuer"],
								 audience: config["Jwt:Audience"],
								 claims: claims,
								 expires: DateTime.UtcNow.AddDays(60),
								notBefore: DateTime.UtcNow,
								 signingCredentials: new SigningCredentials(
									 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])), SecurityAlgorithms.HmacSha256
									)
								 );
			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenString;
		}


		public  bool ValidateToken(string authToken)
		{

			var mySecret = "?v=f2IdQqpjR0c&ab_channel=CodewithJulian";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));

			var myIssuer = "https://localhost:7260/";
			var myAudience = "https://localhost:7260/";

			var tokenHandler = new JwtSecurityTokenHandler();
			var res = true;
			try
            {


            
			SecurityToken validatedToken;
			IPrincipal principal = tokenHandler.ValidateToken(authToken,  new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidIssuer = myIssuer,
				ValidAudience = myAudience,
				IssuerSigningKey = mySecurityKey
			}, out validatedToken);

				
			

            }
            catch 
            {
				 res = false;
				

			}

			return res;
		}

	
		
	
}
}
