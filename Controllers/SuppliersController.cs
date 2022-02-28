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
        public async Task<IActionResult> GetSupplier(string? email = null, bool? confirmed = null, string? phone = null)
        {


            if (email == null && confirmed == null)
            {
                var suppliers = await _reposSuuplier.ReadAsync();

                if (suppliers == null) return new Success(false, "message.UserNotFound", new { });


                return new Success(true, "message.sucess", suppliers);


            }
            else
            {
                var suppliers = await _reposSuuplier.FindBy(email, confirmed, phone);
                
                if (suppliers.Count() != 0)
                {
                    return new Success(true, "message.sucess", suppliers);

                }
                return new Success(false, "message.not found");
            }



        }

        //// GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {

            var supplier = await _reposSuuplier.ReadById(id);
            if (supplier == null) return NotFound("User not found");
            return Ok(supplier);
        }

        //// PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Supplier supplier)
        {

            var newUser = await _reposSuuplier.UpdateAsync(id, supplier);

            if (newUser == null)
                return NotFound("User not found or email already exists");
            return Ok("User updated successfully "); ;

        }

        
        [HttpPost]

        public async Task<ActionResult> PostSupplier([FromBody] Supplier? supplier)
        {

            if (supplier != null)
            {


                Supplier status = await _reposSuuplier.CreateAsync(supplier);

                if (status == null) return Conflict("This email is already exists");



                return Ok(supplier);
            }


            return Problem("User is empty");

        }

        //// DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _reposSuuplier.Delete(id);

            if (res == 200)
                return Ok("Supplier deleted successfully ");
            return NotFound("Supplier not found");



        }

    }
}
