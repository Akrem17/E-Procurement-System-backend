using E_proc.Models;
using E_proc.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _repos;



        public UsersController(IUserRepository repos)
        {
            _repos = repos;
        }
        // get all users or find users by email
        [HttpGet]
        public async Task<IResult> Get(string? email = null)
        {
           
            if (string.IsNullOrWhiteSpace(email))
            {
                var users = await _repos.ReadAsync();
                if (users == null) return Results.NotFound();
                return Results.Ok(users);
            }
            else
            {

                var user = await _repos.FindBy(email);
                return Results.Ok(user);

            }
        }

        // get user by id
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id, string email= null)
        {
            Console.WriteLine(email);

            var user = await _repos.Read(id);
            if (user == null) return Results.NotFound("User not found");
            return Results.Ok(user);
        }

  



        // add a user
        [HttpPost]
        public async Task<IResult> Post([FromBody] User? user)
        {

            if (user != null)
            {

                int status = await _repos.CreateAsync(user);

                if (status == 409) return Results.Conflict("This email is already exists");

                return Results.Ok(user);
            }

            return Results.Problem("User is empty");


        }

        // update user
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] User user)
        {

            var newUser = await _repos.UpdateAsync(id, user);

            if(newUser == null) 
                return Results.NotFound("User not found or email already exists");
                  return Results.Ok("User updated successfully "); ;

        }

        // delete a user
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
         var res=   await _repos.Delete(id);

            if (res == 200)
           return Results.Ok("User deleted successfully ");
           return Results.NotFound("User not found");

        }
    }
}
