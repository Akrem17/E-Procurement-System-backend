using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencesController : ControllerBase
    {

        private readonly AuthContext _dbContext;

        public LicencesController(AuthContext context)
        {
            _dbContext = context;

        }
        // GET: api/<LicenceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LicenceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var licence = await this._dbContext.Licence.Where(l => l.Id == id).FirstOrDefaultAsync();
            return new Success(true, "message.sucess", licence);

        }


        // PUT api/<LicenceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Licence licence)
        {
            var lic = await this._dbContext.Licence.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lic == null)
         
                return new Success(false, "message.failed");

            lic.ExpirationDate = licence.ExpirationDate;
            lic.RegistrationNumber = licence.RegistrationNumber;
            lic.Category = licence.Category;
            lic.Name = licence.Name;
            lic.IssuingInstitutionName = licence.IssuingInstitutionName;
            lic.AcquisitionDate = licence.AcquisitionDate;
            lic.ExpirationDate = licence.ExpirationDate;

            await _dbContext.SaveChangesAsync();
            return new Success(true, "message.success", licence);



        }

        // DELETE api/<LicenceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
