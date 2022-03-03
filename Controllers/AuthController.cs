using E_proc.Models;
using E_proc.Models.StatusModel;
using E_proc.Repositories;
using E_proc.Repositories.Interfaces;
using E_proc.Services;
using E_proc.Services.Interfaces;
using E_proc.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly ICitizenRepository _reposCitizen;

        IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();

        public AuthController(IUserService repos, IUserRepository Userrepos, ITokenService tokenService, IEmailSender emailSender, ISupplierRepository reposSupplier, IInstituteRepository reposInstit, IMemoryCache memoryCache, ICitizenRepository reposCitizen)
        {
            _repos = repos;
            _Userrepos = Userrepos;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _reposSupplier = reposSupplier;
            _reposInstit = reposInstit;
            _memoryCache = memoryCache;
            _reposCitizen= reposCitizen;

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

                if (supplier == null) return new Success(false, "message.email already exsits", new { });


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

                if (supplier == null) return new Success(false, "message.email already exsits", new { });


                var tokenString = _tokenService.GenerateTokenString(supplier);


                                                                
                var message = new Mail(new string[] { supplier.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify-account/" + supplier.Id + "/" + tokenString);
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

                var userFound = await _repos.GetByEmailAndPassword(user);


                if (userFound != null)
                {

                    var loggedUser = await _Userrepos.Read(userFound.Id);

                    if (loggedUser == null) return new Success(false, "message.User not found", new { });

                    var tokenString = _tokenService.GenerateTokenString(loggedUser);

                    if (loggedUser.EmailConfirmed == true) {
                        _tokenService.GenerateTokenString(loggedUser);

                        return new Success(true, "message.success", new { tokenString, loggedUser });

                    }
                    else
                    {

                        var message = new Mail(new string[] { loggedUser.Email }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify-account/" + loggedUser.Id + "/" + tokenString);
                        _emailSender.SendEmail(message);

                        return new Forbidden(false, "message.account not verified, check your email");

                    }
                }
                return new Success(false, "message.Password is incorrect");


            }

            return new Success(false, "message.User is empty");

        }
        // verify Email
        [HttpGet("verify-account/{id}/{token}")]

        public async Task<IActionResult> VerifyConfirmation(int id, string token)
        {

            var email = _tokenService.ValidateJwtToken(token);
            if (email != null) {

                var user = await _Userrepos.Read(id);
                if (user != null)

                {
                    if (user.Email == email) {

                        user.EmailConfirmed = true;
                        var updatedUser = await _Userrepos.UpdateAsync(id, user);


                        return new Success(true, "message.Email confirmed ", new { });

                    }

                    return new Forbidden(false, "message.Token didn't match with user");
                }
                return new Success(false, "message.user not found ", new { });

            }


            return new Success(false, "message.Token not valid ", new { });

        }



        [HttpPost("reset-password-token")]

        public async Task<IActionResult> ResetPasswordCode([FromBody] ResetPasswordToken? model)
        {
            var users = await _Userrepos.FindBy(model.Email, null);
            if (users.Count() == 0) return new Success(false, "message.user Not Found");
            var user = users?[0];

            //generate code
            var verificationCode = "1234";
            //store the code
            
            _memoryCache.Set(model.Email, verificationCode,TimeSpan.FromSeconds(20));
            
            var code = _memoryCache.Get(model.Email);



            
            return new Success(true, "message.success",new {code=code });
        }

        [HttpPost("verify-code")]

        public async Task<IActionResult> VerifyTokenCode([FromBody] VerifyCodeModel? model)
        {
            string token="";
            var code = _memoryCache.Get(model.Email)?.ToString();
            Console.WriteLine(code);
            if (code != null)
            {
                if (string.Equals(code, model.Code) )
                {
                    //generate token

                     token = _tokenService.GenerateTokenStringPasswordReset(model);
                    //delete code from cache

                    _memoryCache.Set("passwordToken", token, TimeSpan.FromSeconds(50));
                    _memoryCache.Remove(model.Email);
                    return new Success(true, "message.code verified", new {token});


                }
                return new Success(false, "message.Code is not verified");

            }
            return new Success(false, "message.Code isExpired");

        }






        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel? model)
        {

            var verifiedToken = string.Equals(_memoryCache.Get("passwordToken")?.ToString(), model.Token);

            if (!verifiedToken) return new Success(false, "message.Token not verified");

            var email = _tokenService.ValidateJwtToken(model.Token); 
            if (email != model.Email)
            return new Success(false, "message.Email not the same");

            var users = await _Userrepos.FindBy(model.Email, null);
            if (users.Count() == 0) return new Success(false, "message.user Not Found");

            var user = users?[0];
            
           if( model.NewPassword != model.ConfirmPassword) return new Success(false, "message.Password not confirmed");


            var update = await _Userrepos.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (update != null)
            {
                _memoryCache.Remove("passwordToken");
                return new Success(true, "message.Password updated successfully", update);
            }
             return new Success(false, "message.User not added");


        }


        [HttpPost("connected-user")]

        public async Task<IActionResult> GetConnectedUser([FromBody] string? token)
        {

            var email =  _tokenService.ValidateJwtToken(token);
            
            if (email == null) return new Success(false, "message.token not verified");
            var users = await _Userrepos.FindBy(email, null);
            if (users.Count() == 0) return new Success(false, "message.user Not Found");

            var user = users?[0];
         

            return new Success(true, "message.success",user);






        }

    }
}
