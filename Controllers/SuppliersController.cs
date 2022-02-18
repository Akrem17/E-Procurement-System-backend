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
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplier()
        {
              var  suppliers=await _reposSuuplier.ReadAsync();
            return Ok(suppliers);


        }

        //// GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {

            var supplier = await _reposSuuplier.ReadById(id);
            if (supplier == null) return Results.NotFound("User not found");
            return Results.Ok(supplier);
        }

        //// PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] Supplier supplier)
        {

            var newUser = await _reposSuuplier.UpdateAsync(id, supplier);

            if (newUser == null)
                return Results.NotFound("User not found or email already exists");
            return Results.Ok("User updated successfully "); ;

        }

        //// POST: api/Suppliers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<IResult> PostSupplier([FromBody] Supplier? supplier)
        {

            if (supplier != null)
            {


                Supplier status = await _reposSuuplier.CreateAsync(supplier);

                if (status == null) return Results.Conflict("This email is already exists");



                return Results.Ok(supplier);
            }


            return Results.Problem("User is empty");

        }

        //// DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var res = await _reposSuuplier.Delete(id);

            if (res == 200)
                return Results.Ok("Supplier deleted successfully ");
            return Results.NotFound("Supplier not found");



        }

    }
}
