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
using E_proc.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace E_proc.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "citizen")]
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenRepository _reposCitizen;
        private readonly IEncryptionService _encryptionService;

        public CitizensController(ICitizenRepository reposCitizen, IEncryptionService encryptionService)
        {
            _reposCitizen = reposCitizen;
            _encryptionService = encryptionService;
        }

        // get all citizens
        [HttpGet]
        public async Task<Success> GetCitizen(string? email=null, bool? confirmed=null, string? cin = null,string? phone=null)

        {
                
            if(email == null && confirmed==null)
            {
                var citizens = await _reposCitizen.ReadAsync();

                if (citizens == null) return new Success(false, "message.UserNotFound", new { });

                
                return new Success(true, "message.sucess", citizens);


            }
            else  {
                var citizens = await _reposCitizen.FindBy(email, confirmed,cin,phone);
                return new Success(true, "message.sucess", citizens);
            }

                
       


        }


        //// get citizenById
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var citizen = await _reposCitizen.ReadById(id);
            if (citizen == null) return new Success(false, "message.user Not Found");
            return  new Success(true, "message.success", new { citizen });
        }

        //// update citizen
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Citizen user)
        {

            var newUser = await _reposCitizen.UpdateAsync(id, user);

            if (newUser == null)
             
            return new Success(false, "message.User not found or email already exists");
       
            return new Success(true, "message.User updated successfully");
        }

        // create citizen
        [HttpPost]

        public async Task<IActionResult> PostCitizen([FromBody] Citizen? citizen)
        {

            if (citizen != null)
            {


                Citizen status = await _reposCitizen.CreateAsync(citizen);
      
                if (status == null) return new Success(false, "message.This email is already exists");



                return  new Success(true, "message.success", new { citizen });
            }


            return new Success(false, "message.User is empty");

        }

        // delete citizen
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _reposCitizen.Delete(id);

            if (res == 200)
                return new Success(true, "message.success");

        
            return new Success(false, "message.Citizen not found");

        }
        
    }

}

