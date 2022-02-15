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
        // get all users
        [HttpGet]
        public async Task<IResult> Get()
        {
            var users = await _repos.ReadAsync();
            if (users == null) return Results.NotFound();
            return Results.Ok(users);
        }

        // get user by id
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {

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

                //JsonSerializerOptions options = new()
                //{
                //    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                //    WriteIndented = true
                //};
                //string tylerJson = JsonSerializer.Serialize(user, options);
                //User tylerDeserialized =
                //JsonSerializer.Deserialize<User>(tylerJson, options);
                //Console.WriteLine(x.Id);

                int status = await _repos.CreateAsync(user);

                if (status == 409) return Results.Conflict("This email is already exists");





                return Results.Ok(user);
            }


            return Results.Problem("User is empty");


        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] User user)
        {

            var newUser = await _repos.UpdateAsync(id, user);

            if(newUser == null) 
                return Results.NotFound("User not found or email already exists");
                  return Results.Ok("User updated successfully "); ;

        }

        // DELETE api/<ValuesController>/5
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
