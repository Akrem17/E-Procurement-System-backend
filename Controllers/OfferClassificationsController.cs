using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OfferClassificationsController : ControllerBase
    {

        private readonly AuthContext _context;

        public OfferClassificationsController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/oc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferClassification>>> GetAddress()
        {

            return await _context.OfferClassification.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferClassification>> GetOfferClassification(int id)
        {
            var offerClassification = await _context.OfferClassification.FindAsync(id);

            if (offerClassification == null)
            {
                return NotFound();
            }

            return offerClassification;
        }

        // PUT: api/TenderClassifications/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTenderClassification(int id, TenderClassification tenderClassification)
        //{
        //    var tc = await _tenderClassificationRepository.UpdateAsync(id, tenderClassification);
        //    if (tc == null) return new Success(false, "message.notFound");
        //    return new Success(true, "message.Tender Classification Updated", tc);
        //}

        // POST: api/TenderClassifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTenderClassification(OfferClassification offerClassification)
        {
           await _context.OfferClassification.AddAsync(offerClassification);
            await _context.SaveChangesAsync();

               return new Success(true, "message.offer Classification crated");
        }


    }
}
