#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_proc.Models;
using E_proc.Repositories.Interfaces;
using E_proc.Models.StatusModel;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderClassificationsController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly ITenderClassificationRepository _tenderClassificationRepository;

        public TenderClassificationsController(AuthContext context, ITenderClassificationRepository tenderClassificationRepository)
        {
           _tenderClassificationRepository= tenderClassificationRepository;
        }

        // GET: api/TenderClassifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenderClassification>>> GetTenderClassification()
        {
            return await _context.TenderClassification.ToListAsync();
        }

        // GET: api/TenderClassifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TenderClassification>> GetTenderClassification(int id)
        {
            var tenderClassification = await _context.TenderClassification.FindAsync(id);

            if (tenderClassification == null)
            {
                return NotFound();
            }

            return tenderClassification;
        }

        // PUT: api/TenderClassifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenderClassification(int id, TenderClassification tenderClassification)
        {
            var tc = await _tenderClassificationRepository.UpdateAsync(id, tenderClassification);
            if (tc == null) return new Success(false, "message.notFound");
            return new Success(true, "message.Tender Classification Updated", tc);
        }

        // POST: api/TenderClassifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TenderClassification>> PostTenderClassification(TenderClassification tenderClassification)
        {
            _context.TenderClassification.Add(tenderClassification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTenderClassification", new { id = tenderClassification.Id }, tenderClassification);
        }

        // DELETE: api/TenderClassifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenderClassification(int id)
        {
            var tenderClassification = await _context.TenderClassification.FindAsync(id);
            if (tenderClassification == null)
            {
                return NotFound();
            }

            _context.TenderClassification.Remove(tenderClassification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TenderClassificationExists(int id)
        {
            return _context.TenderClassification.Any(e => e.Id == id);
        }
    }
}
