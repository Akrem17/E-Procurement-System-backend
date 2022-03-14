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
    public class RepresentativesController : ControllerBase
    {
        private readonly IRepresentativeRepository _reposRepresentative;

        public RepresentativesController(IRepresentativeRepository reposRepresentative)
        {

            _reposRepresentative= reposRepresentative;
        }

        // GET: api/Representatives
        [HttpGet]
        public async Task<IActionResult > GetRepresentative(string? socialSecurityNumber)
        {
            if(socialSecurityNumber == null)
            {

          
            var representative = await _reposRepresentative.ReadAsync();

            if (representative == null) return new Success(false, "message.UserNotFound");


            return new Success(true, "message.sucess", representative);
            }
            else { 

            var representative = await _reposRepresentative.FindBy(socialSecurityNumber);
            if (representative.Count() != 0)
            {
                return new Success(true, "message.sucess", representative);

            }
            return new Success(false, "message.not found");
            }
        }


    }

    // GET: api/Representatives/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Representative>> GetRepresentative(int id)
    //{
    //    return await _dbContext. .FirstOrDefaultAsync(user => user.Id == id);


    //}

    //// PUT: api/Representatives/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutRepresentative(int id, Representative representative)
    //{

    //}

    //// POST: api/Representatives
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<Representative>> PostRepresentative(Representative representative)
    //{


    //// DELETE: api/Representatives/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteRepresentative(int id)
    //{

    //}

    //private bool RepresentativeExists(int id)
    //{

    //}
}

