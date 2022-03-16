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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

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
        public async Task<IActionResult> GetTendersOfInstitute(int id)
        {

            var tenders = await _reposInstit.getTendersOfInstitute(id);
            return new Success(true, "message.sucess", new { tenders });

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitute(int id)
        {

            var institute = await _reposInstit.ReadById(id);
            var tenders = await _reposInstit.getTendersOfInstitute(id);
            var t = tenders.ToList();
            t.ForEach(tend => { tend.Institute = null;});
            institute.Tenders = t;
            if (institute == null) return new Success(false, "message.User not found");
            return new Success(true, "message.sucess", new { institute});
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute(int id, Institute institute)
        {
            var newUser = await _reposInstit.UpdateAsync(id, institute);

            if (newUser == null)

                return new Success(false, "message.User not found or email already exists");
            return new Success(true, "message.success");

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
