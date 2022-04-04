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

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly AuthContext _context;
        private IFileDataRepository _fileRepository;
        private IOfferRepository _offerRepository;
        public OffersController(AuthContext context, IFileDataRepository fileRepository, IOfferRepository offerRepository )
        {
            _context = context;
            _fileRepository = fileRepository;
            _offerRepository = offerRepository;

        }

        // GET: api/Offers
        [HttpGet]
        public async Task<IActionResult> GetOffer(int? skip=0,int? take=10,string? supplierId=null, string? supplierEmail = null)
        {
            if (supplierId == null) { 
            var offer = await _offerRepository.ReadAsync((int)skip,(int) take);
                var itemsNumber = _offerRepository.CountData();

            if (offer == null) return new Success(false, "message.OfferNotFound");

            return new Success(true, "message.sucess", offer);
            }
            else
            {
                var offer = await _offerRepository.FindBy(supplierId,supplierEmail);


                if (offer == null) return new Success(false, "message.UserNotFound");

                return new Success(true, "message.sucess", offer);
            }
            return new Success(false, "message.failed");


        }

        // GET: api/Offers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffer(int id)

        { 
            var offer = await _offerRepository.ReadById(id);
            if (offer == null) return new Success(false, "message.user Not Found");
            return new Success(true, "message.success",  offer );
  
        }

        // PUT: api/Offers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffer(int id, Offer offer)
        {
            var newOffer = await _offerRepository.UpdateAsync(id, offer);

            if (newOffer == null)
                return new Success(false, "message.User not found ");
            return new Success(true, "message.success", newOffer);
        }

        [HttpPost("files")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostAsync([FromForm] FileModel model, int? tenderId)
        {


            try
            {
                List<FileRecord> files = await _fileRepository.SaveFileAsync(model.MyFile);


                if (files.Count() != 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        files[i].AltText = model.AltText;
                        files[i].Description = model.Description;
                    }



                    var file = _fileRepository.SaveToDBForOffer(files, tenderId);
                    return new Success(true, "message.success", file);
                }
                else
                    return new Success(false, "message.failed");

            }
            catch (Exception ex)
            {
                return new Success(false, "message." + ex.Message);
            }
        }



        // POST: api/Offers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Offer>> PostOffer(Offer offer)
        {
            _context.Offer.Add(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffer", new { id = offer.Id }, offer);
        }

        // DELETE: api/Offers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offer = await _context.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.Offer.Remove(offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.Id == id);
        }
    }
}
