using E_proc.Models;
using E_proc.Services;
using E_proc.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_proc.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _repos;
        private readonly IUserRepository _Userrepos;

        public AuthController(IUserService repos, IUserRepository Userrepos)
        {
            _repos = repos;
           _Userrepos = Userrepos;

        }


        
        // POST login
        [HttpPost("/signup")]

        public async Task<IResult> Signup([FromBody] User? user)
        {

            if (user != null)
            {

           

                int status = await _repos.Signup(user);

                if (status == 409) return Results.Conflict("This email is already exists");

                return Results.Ok(user);
            }


            return Results.Problem("User is empty");


        }

        // POST login
        [HttpPost("/login")]

        public async Task<IResult> Login([FromBody] UserLogin? user)
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            if (user != null)
            {

               var userFound= await _repos.GetByEmailAndPassword(user);

               if (userFound != null)
                {

                    var loggedUser = await _Userrepos.Read(userFound.Id);
                  
                     if (loggedUser == null) return Results.NotFound("User not found");


                                    var claims = new[]
                                    {
                                    new Claim(ClaimTypes.Email,loggedUser.Email),
                                    new Claim(ClaimTypes.GivenName,loggedUser.FirstName),
                                    new Claim(ClaimTypes.Surname,loggedUser.LastName),
                                    new Claim(ClaimTypes.Role,loggedUser.Type)
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

                    return Results.Ok(tokenString);
                

                }

                return Results.NotFound("Email or password is wrong");

               



            }


            return Results.Problem("User is empty");


        }
    }
}
