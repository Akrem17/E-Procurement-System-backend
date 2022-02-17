﻿using E_proc.Models;
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
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();

        public AuthController(IUserService repos, IUserRepository Userrepos,ITokenService tokenService, IEmailSender emailSender)
        {
            _repos = repos;
           _Userrepos = Userrepos;
            _tokenService = tokenService;
            _emailSender = emailSender;


        }




        // POST login
        [HttpPost("/signup/citizen")]

        public async Task<IResult> Signup([FromBody] Citizen? user)
        {


            if (user != null)
            {



                Citizen status = await _repos.SignupCitizen(user);

                if (status == null) return Results.Conflict("This email is already exists");

              
                var tokenString = _tokenService.GenerateTokenString(status);



                var message = new Mail(new string[] { "akrem.hammami041798@gmail.com" }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + status.Id + "/" + tokenString);
                _emailSender.SendEmail(message);

                return Results.Ok(new { tokenString, status });
            }


            return Results.Problem("User is empty");


        }


        // POST login
        [HttpPost("/signup")]

        public async Task<IResult> Signup([FromBody] User? user)
        {
       
       

            if (user != null)
            {

           

                int status = await _repos.Signup(user);

                if (status == 409) return Results.Conflict("This email is already exists");

             
                var tokenString = _tokenService.GenerateTokenString(user);
                    
                   


                var message = new Mail(new string[] { "akrem.hammami041798@gmail.com" }, "Email Confirmation E-PROC", "Welcome to E-proc. /n Your Confirmation Link is \n https://localhost:7260/verify/" + user.Id+"/"+ tokenString);
                _emailSender.SendEmail(message);

                return Results.Ok(new { tokenString, user });
            }


            return Results.Problem("User is empty");


        }

        // POST login
        [HttpPost("/login")]

        public async Task<IResult> Login([FromBody] UserLogin? user)
        {
           
            if (user != null)
            {

               var userFound= await _repos.GetByEmailAndPassword(user);

                if (userFound != null)
                {

                 

                    var loggedUser = await _Userrepos.Read(userFound.Id);

                    if (loggedUser == null) return Results.NotFound("User not found");
                    if (loggedUser.EmailConfirmed == true) {
                        _tokenService.GenerateTokenString(loggedUser);
                
                    var tokenString = _tokenService.GenerateTokenString(loggedUser);

                    return Results.Ok(new {tokenString, loggedUser });

                    }
                    else
                    {
                        return Results.NotFound("account not verified, check your email");

                    }
                }
              

                return Results.NotFound("Email or password is wrong");

               



            }


            return Results.Problem("User is empty");


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
                   
                     return Results.Problem("Token didn't match with user");
                    
                }
                return Results.NotFound("User not found");

            }


            return Results.Problem("Token Invalid");

        }

        }
}
