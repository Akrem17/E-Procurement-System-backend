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
using E_proc.Repositories.Interfaces;
using E_proc.Models.StatusModel;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _reposSuuplier;

        public SuppliersController(ISupplierRepository reposSuuplier)
        {
            _reposSuuplier = reposSuuplier;

        }



        // GET: api/Suppliers
        [HttpGet]
        public async Task<IActionResult> GetSupplier(string? email = null, bool? confirmed = null, string? phone = null, DateTime? date = null)
        {


            if (email == null && confirmed == null && phone == null && date == null)
            {
                var suppliers = await _reposSuuplier.ReadAsync();

                if (suppliers == null) return new Success(false, "message.UserNotFound");


                return new Success(true, "message.success", suppliers);

            }
            else
            {
                var suppliers = await _reposSuuplier.FindBy(email, confirmed, phone,date);

                if (suppliers.Count() != 0)
                {
                    return new Success(true, "message.success", suppliers);

                }
                return new Success(false, "message.not found");
            }

        }



        //// GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var supplier = await _reposSuuplier.ReadById(id);
            if (supplier == null) return new Success(false, "message.Usernot found");
            return new Success(true, "message.success", supplier);
        }




        //// PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Supplier supplier)
        {                                                                                                                                                                 
            var newUser = await _reposSuuplier.UpdateAsync(id, supplier);

            if (newUser == null)
                return new Success(false, "message.Cannot be updated");
            return new Success(true, "message.success",newUser);

        }



        //add supplier
        [HttpPost]

        public async Task<IActionResult> PostSupplier([FromBody] Supplier? supplier)
        {

            if (supplier != null)
            {


                Supplier status = await _reposSuuplier.CreateAsync(supplier);

                if (status == null) return new Success(false, "message.This email is already exists");



                return new Success(true, "message.success", supplier); ;
            }


            return new Success(false, "message.User is empty");


        }




        //// DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _reposSuuplier.Delete(id);

            if (res == 200)
                return new Success(true, "message.success"); ;

            return new Success(false, "messageSupplier not found");


        }

    }
}
