using E_proc.Models;
using E_proc.Models.StatusModel;
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
        public async Task<IActionResult> Get(string? email = null, bool? confirmed=null )
        {
           
            if (string.IsNullOrWhiteSpace(email) && confirmed == null)
            {
                var users = await _repos.ReadAsync();
                if (users == null) return new Success(false, "message.User not found");
                return new Success(true, "message.success", users);
            }
            else
            {
                var user = await _repos.FindBy(email, confirmed);
                if (user.Count()==0) return new Success(false, "message.User notFound", user);
                return new Success(true, "message.success", user);


            }
        }

        // get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
         

            var user = await _repos.Read(id);
            if (user == null) return new Success(false, "message.User not found");
            return  new Success(true, "message.success", user);
        }

  



        // add a user
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User? user)
        {

            if (user != null)
            {

                int status = await _repos.CreateAsync(user);

                if (status == 409) return new Success(false, "message.This email is already exists");

                return new Success(true, "message.success", user);
            }

          
            return new Success(false, "message.User is empty");


        }

        // update user
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {

            var newUser = await _repos.UpdateAsync(id, user);

            if(newUser == null) 
            return  new Success(false, "message.This email is already exists");
            return new Success(true, "message.success");

        }

        // delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
         var res=   await _repos.Delete(id);

            if (res == 200)
            return  new Success(true, "message.success");
           
            return new Success(false, "message.User not found");


        }
    }
}
