#nullable disable
using Microsoft.AspNetCore.Mvc;
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

            _reposRepresentative = reposRepresentative;
        }

        // GET: api/Representatives
        [HttpGet]
        public async Task<IActionResult> GetRepresentative(string? socialSecurityNumber)
        {

            if (socialSecurityNumber == null)
            {


                var representative = await _reposRepresentative.ReadAsync();

                if (representative == null) return new Success(false, "message.UserNotFound");


                return new Success(true, "message.sucess", representative);
            }
            else
            {

                var representative = await _reposRepresentative.FindBy(socialSecurityNumber);
                if (representative.Count() != 0)
                {
                    return new Success(true, "message.sucess", representative);

                }
                return new Success(false, "message.not found");
            }
        }




        // GET: api/Representatives/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Representative>> GetRepresentative(int id)
        //{
        //    return await _dbContext. .FirstOrDefaultAsync(user => user.Id == id);


        //}

        // PUT: api/Representatives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepresentative(int id, Representative representative)
        {

            var repres = await _reposRepresentative.UpdateAsync(id, representative);
            if (repres == null) return new Success(false, "message.notFound");
            return new Success(true, "message.Representative Updated", repres);

        }



        [HttpPost]
        public async Task<IActionResult> PostRepresentative(Representative representative)
        {

            if (representative != null)
            {


                Representative status = await _reposRepresentative.CreateAsync(representative);

                if (status == null)

                    return new Success(false, "message.This email is already exists");



                return new Success(true, "message.success", new { representative });
            }


            return new Success(false, "message.User is empty");


        }


        //// DELETE: api/Representatives/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRepresentative(int id)
        //{

        //}

        //private bool RepresentativeExists(int id)
        //{

        //}


    }
}