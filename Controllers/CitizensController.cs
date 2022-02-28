#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_proc.Models;
using E_proc.Repositories;
using E_proc.Models.StatusModel;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenRepository _reposCitizen;

        public CitizensController(ICitizenRepository reposCitizen)
        {
            _reposCitizen = reposCitizen;
        }

        // get all citizens
        [HttpGet]
        public async Task<Success> GetCitizen(string? email=null, bool? confirmed=null, string? cin = null)

        {
                
            if(email == null && confirmed==null)
            {
                var citizens = await _reposCitizen.ReadAsync();

                if (citizens == null) return new Success(false, "message.UserNotFound", new { });
                return new Success(true, "message.sucess", citizens);
            }
            else  {
                var citizens = await _reposCitizen.FindBy(email, confirmed,cin);
                return new Success(true, "message.sucess", citizens);
            }


       


        }


        //// get citizenById
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var citizen = await _reposCitizen.ReadById(id);
            if (citizen == null) return NotFound("User not found");
            return Ok(citizen);
        }

        //// update citizen
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Citizen user)
        {

            var newUser = await _reposCitizen.UpdateAsync(id, user);

            if (newUser == null)
                return NotFound("User not found or email already exists");
            return Ok("User updated successfully "); ;

        }

        // create citizen
        [HttpPost]

        public async Task<IActionResult> PostCitizen([FromBody] Citizen? citizen)
        {

            if (citizen != null)
            {


                Citizen status = await _reposCitizen.CreateAsync(citizen);
      
                if (status == null) return Conflict("This email is already exists");



                return Ok( citizen);
            }


            return Problem("User is empty");

        }

        // delete citizen
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _reposCitizen.Delete(id);

            if (res == 200)
                return Ok("Citizen deleted successfully ");
            return NotFound("Citizen not found");



        }
        
    }

}

