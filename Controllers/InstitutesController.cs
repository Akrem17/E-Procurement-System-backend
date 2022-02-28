#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_proc.Models;
using E_proc.Repositories.Interfaces;
using E_proc.Models.StatusModel;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly IInstituteRepository _reposInstit;

        public InstitutesController(IInstituteRepository reposInstit)
        {
            _reposInstit = reposInstit;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetInstitute(string? email = null, bool? confirmed = null, string? phone = null)
        {


            if (email == null && confirmed == null)
            {
                var institutes = await _reposInstit.ReadAsync();

                if (institutes == null) return new Success(false, "message.UserNotFound", new { });


                return new Success(true, "message.sucess", institutes);


            }
            else
            {
                var citizens = await _reposInstit.FindBy(email, confirmed, phone);
                return new Success(true, "message.sucess", citizens);
            }

        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Institute>> GetInstitute(int id)
        {

            var supplier = await _reposInstit.ReadById(id);
            if (supplier == null) return NotFound("User not found");
            return Ok(supplier);
        }

        // PUT: api/Institutes/5
      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute(int id, Institute institute)
        {
            var newUser = await _reposInstit.UpdateAsync(id, institute);

            if (newUser == null)
                return NotFound("User not found or email already exists");
            return Ok("User updated successfully "); 
        }

        // POST: api/Institutes
     
        [HttpPost]
        public async Task<ActionResult<Institute>> PostInstitute(Institute institute)
        {

            if (institute != null)
            {


                Institute status = await _reposInstit.CreateAsync(institute);

                if (status == null) return Conflict("This email is already exists");



                return Ok(institute);
            }


            return Problem("User is empty");
        }

        // DELETE: api/Institutes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute(int id)
        {
            var res = await _reposInstit.Delete(id);

            if (res == 200)
                return Ok("Institute deleted successfully ");
            return NotFound("Institute not found");
        }

    
    }
}
