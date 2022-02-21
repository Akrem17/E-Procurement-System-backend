using E_proc.Models;
using E_proc.Repositories.Interfaces;
using E_proc.Services;
using E_proc.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace E_proc.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _repos;
        private readonly IUserRepository _Userrepos;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly ISupplierRepository _reposSupplier;

        IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();

        public AuthController(IUserService repos, IUserRepository Userrepos, ITokenService tokenService, IEmailSender emailSender,ISupplierRepository reposSupplier)
        {
            _repos = repos;
           _Userrepos = Userrepos;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _reposSupplier = reposSupplier;


        }




        // singup a citizen route
        [HttpPost("/signup/citizen")]

        public async Task<IResult> Signup([FromBody] Citizen? user)
        {


            if (user != null)
            {

                Citizen status = await _repos.SignupCitizen(user);

                if (status == null) return Results.Conflict("This email is already exists");

              
                var tokenString = _tokenService.GenerateTokenString(status);



                var message = new Mail(new string[] { status.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + status.Id + "/" + tokenString);
                _emailSender.SendEmail(message);

                return Results.Ok(new { tokenString, status });
            }


            return Results.Problem("User is empty");


        }
        // singup a citizen route
        [HttpPost("/signup/supplier")]

        public async Task<IResult> Signup([FromBody] Supplier? user)
        {


            if (user != null)
            {
                Supplier supplier = await _reposSupplier.CreateAsync(user);

                if (supplier == null) return Results.Conflict("This email is already exists");


                var tokenString = _tokenService.GenerateTokenString(supplier);



                var message = new Mail(new string[] { supplier.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + supplier.Id + "/" + tokenString);
                _emailSender.SendEmail(message);

                return Results.Ok(new { tokenString, supplier });
            }


            return Results.Problem("User is empty");


        }



        //  login a user
        [HttpPost("/login")]

        public async Task<IActionResult> Login([FromBody] UserLogin? user)
        {
           
            if (user != null)
            {

               var userFound= await _repos.GetByEmailAndPassword(user);

                if (userFound != null)
                {

                    var loggedUser = await _Userrepos.Read(userFound.Id);

                    if (loggedUser == null) return NotFound("User not found");
                    var tokenString = _tokenService.GenerateTokenString(loggedUser);

                    if (loggedUser.EmailConfirmed == true) {
                        _tokenService.GenerateTokenString(loggedUser);
                

                    return Ok(new {tokenString, loggedUser });

                    }
                    else
                    {
                        //verify if token expired
                      
                        var message = new Mail(new string[] { loggedUser.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + loggedUser.Id + "/" + tokenString);
                        _emailSender.SendEmail(message);
                        return Unauthorized("account not verified, check your email");

                    }
                }            
                return Unauthorized();

            }

            return Problem("User is empty");

        }
        // verify Email
        [HttpGet("Verify/{id}/{token}")]

        public async Task<IResult> VerifyConfirmation(int id,string token)
        {

            var email = _tokenService.ValidateJwtToken(token);
            if (email != null) { 
                
                var user = await _Userrepos.Read(id);
                if (user != null)

                {   
                    if(user.Email== email) { 

                    user.EmailConfirmed = true;
                    var updatedUser = await _Userrepos.UpdateAsync(id, user);

                   
                    return Results.Ok("Email confirmed ");
                    }
                   
                     return Results.BadRequest("Token didn't match with user");
                    
                }
                return Results.NotFound("User not found");

            }


            return Results.Problem("Token Invalid");

        }

        }
}
