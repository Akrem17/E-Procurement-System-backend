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
        public async Task<IResult> GetCitizen()

        {
            var citizens = await _reposCitizen.ReadAsync();
            if (citizens == null) return Results.NotFound("No users found");
            return Results.Ok(citizens);
        }


        //// get citizenById
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {

            var citizen = await _reposCitizen.ReadById(id);
            if (citizen == null) return Results.NotFound("User not found");
            return Results.Ok(citizen);
        }

        //// update citizen
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] Citizen user)
        {

            var newUser = await _reposCitizen.UpdateAsync(id, user);

            if (newUser == null)
                return Results.NotFound("User not found or email already exists");
            return Results.Ok("User updated successfully "); ;

        }

        // create citizen
        [HttpPost]

        public async Task<IResult> PostCitizen([FromBody] Citizen? citizen)
        {

            if (citizen != null)
            {


                Citizen status = await _reposCitizen.CreateAsync(citizen);
      
                if (status == null) return Results.Conflict("This email is already exists");



                return Results.Ok( citizen);
            }


            return Results.Problem("User is empty");

        }

        // delete citizen
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var res = await _reposCitizen.Delete(id);

            if (res == 200)
                return Results.Ok("Citizen deleted successfully ");
            return Results.NotFound("Citizen not found");



        }
        
    }

}

