#nullable disable
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetInstitute(string? email = null, bool? confirmed = null, string? phone = null, DateTime? date = null)
        {


            if (email == null && confirmed == null && phone==null && date==null)
            {
                var institutes = await _reposInstit.ReadAsync();
               

                if (institutes == null) return new Success(false, "message.UserNotFound");
                
                return new Success(true, "message.sucess",institutes);


            }

            else
            {
                var institutes = await _reposInstit.FindBy(email, confirmed, phone,date);
                if (institutes.Count() != 0)
                {
                    return new Success(true, "message.sucess", institutes);

                }
                return new Success(false, "message.not found");

            }

        }





        [HttpGet("{id}/tenders")]
        public async Task<IActionResult> GetTendersOfInstitute(int id, int? skip = 0, int? take = 10)
        {

            var tenders = await _reposInstit.getTendersOfInstitute(id, (int)skip, (int)take);
            if (tenders.Count()==0) return new Success(false, "message.notFound");
            var items = _reposInstit.getTendersOfInstituteCountData(id).Result;
            
            return new Success(true, "message.sucess", new { tenders, items });


        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitute(int id, int? skip = 0, int? take = 10)
        {

            var institute = await _reposInstit.ReadById(id);
            var tenders = await _reposInstit.getTendersOfInstitute(id, (int)skip, (int)take);
           // var specifications=await _reposInstit.getInstituteSpecifications(id);
            var t = tenders.ToList();
            t.ForEach(tend => { tend.Institute = null;});
            institute.Tenders = t;
            //institute.Specifications = specifications;

            if (institute == null) return new Success(false, "message.User not found");
            return new Success(true, "message.sucess", institute);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute(int id, Institute institute)
        {
            var newUser = await _reposInstit.UpdateAsync(id, institute);

            if (newUser == null)

                return new Success(false, "message.User not found ");
            return new Success(true, "message.success", institute);

        }





        // POST: api/Institutes

        [HttpPost]
        public async Task<IActionResult> PostInstitute(Institute institute)
        {

            if (institute != null)
            {


                Institute status = await _reposInstit.CreateAsync(institute);

                if (status == null)

                    return new Success(false, "message.This email is already exists");



                return new Success(true, "message.success", new { institute });
            }


            return Problem("User is empty");
        }



        // DELETE: api/Institutes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute(int id)
        {
            var res = await _reposInstit.Delete(id);

            if (res == 200)
                return new Success(true, "message.success");
            return new Success(false, "message.User not found");
        }


    }
}
