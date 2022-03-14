﻿#nullable disable
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
using System.Text.Json;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TendersController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly ITenderRepository _reposTender;

        public TendersController(AuthContext context,ITenderRepository reposTender)
        {

            _reposTender = reposTender;
            _context = context;

        }

      
        // GET: api/Tenders
        [HttpGet]
        public async Task<IActionResult> GetTender()
        {
            var tenders = await _reposTender.ReadAsync();

            if (tenders == null) return new Success(false, "message.UserNotFound", new { });


            return new Success(true, "message.success", tenders);
        }

        // GET: api/Tenders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTender(int id)
        {
            var tender = await _reposTender.ReadById(id);
            tender.Institute = null;
         //  var res= JsonSerializer.Serialize(tender);
            if (tender == null) return new Success(false, "message.Usernot found");
            return new Success(true, "message.success", tender);
        }

        // PUT: api/Tenders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTender(int id, Tender tender)
        {
            var newUser = await _reposTender.UpdateAsync(id, tender);

            if (newUser == null)
                return new Success(false, "message.User not found ");
            return new Success(true, "message.success", newUser);
        }

        // POST: api/Tenders
        [HttpPost]
        public async Task<IActionResult> PostTender([FromBody] Tender tender)
        {
            if (tender != null)
            {

                //verify institute
                var status = await _reposTender.CreateAsync(tender);


                if (status!=null)
                return new Success(true, "message.success", tender);

            }


            return new Success(false, "message.User is empty");

        }

        // DELETE: api/Tenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTender(int id)
        {
            var res = await _reposTender.Delete(id);
            if (res == 200)
                return new Success(true, "message.success"); ;

            return new Success(false, "Tender not found");

        }

        private bool TenderExists(int id)
        {
            return _context.Tender.Any(e => e.Id == id);
        }
    }
}