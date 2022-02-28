using E_proc.Models;
using E_proc.Models.StatusModel;
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
        private readonly IInstituteRepository _reposInstit;


        IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();

        public AuthController(IUserService repos, IUserRepository Userrepos, ITokenService tokenService, IEmailSender emailSender,ISupplierRepository reposSupplier, IInstituteRepository reposInstit)
        {
            _repos = repos;
           _Userrepos = Userrepos;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _reposSupplier = reposSupplier;
            _reposInstit = reposInstit;
            

        }




        // singup a citizen route
        [HttpPost("/signup/citizen")]

        public async Task<IActionResult> Signup([FromBody] Citizen? user)
        {


            if (user != null)
            {

                Citizen status = await _repos.SignupCitizen(user);

                if (status == null) return new Success(false, "message.email already exsits", new { });


                var tokenString = _tokenService.GenerateTokenString(status);



                var message = new Mail(new string[] { status.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + status.Id + "/" + tokenString);
                _emailSender.SendEmail(message);

               
                return new Success(true, "message.sucess", new { tokenString, status });
            }

            return new Success(false, "message.user is empty", new { });


        }
        // singup a supplier route
        [HttpPost("/signup/supplier")]

        public async Task<IActionResult> Signup([FromBody] Supplier? user)
        {


            if (user != null)
            {
                Supplier supplier = await _reposSupplier.CreateAsync(user);

                if (supplier == null) return new Success(false, "message.email already exsits", new {});


                var tokenString = _tokenService.GenerateTokenString(supplier);



                var message = new Mail(new string[] { supplier.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + supplier.Id + "/" + tokenString);
                _emailSender.SendEmail(message);

            
                return new Success(true, "message.sucess", new { tokenString, supplier });
               
            }


            return new Success(false, "message.user is empty", new { });


        }


        // singup a institute route
        [HttpPost("/signup/institute")]

        public async Task<IActionResult> Signup([FromBody] Institute? user)
        {


            if (user != null)
            {
                Institute supplier = await _reposInstit.CreateAsync(user);

                if (supplier == null) return new Success(false, "message.email already exsits", new {});


                var tokenString = _tokenService.GenerateTokenString(supplier);



                var message = new Mail(new string[] { supplier.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + supplier.Id + "/" + tokenString);
                _emailSender.SendEmail(message);





                return new Success(true, "message.sucess", new { tokenString, supplier });

            }


            return new Success(false, "message.user is empty", new { });


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

                    if (loggedUser == null) return new Success(false, "message.User not found", new { });

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

        public async Task<IActionResult> VerifyConfirmation(int id,string token)
        {

            var email = _tokenService.ValidateJwtToken(token);
            if (email != null) { 
                
                var user = await _Userrepos.Read(id);
                if (user != null)

                {   
                    if(user.Email== email) { 

                    user.EmailConfirmed = true;
                    var updatedUser = await _Userrepos.UpdateAsync(id, user);


                        return new Success(true, "message.Email confirmed ", new { });

                    }
                   
                    return BadRequest("Token didn't match with user");
                    
                }
                return new Success(false, "message.user not found ", new { });

            }


            return new Success(false, "message.Token not valid ", new { });

        }

        }
}
