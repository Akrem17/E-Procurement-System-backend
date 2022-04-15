using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly AuthContext _dbContext;

        public ConnectionsController(AuthContext context)
        {
                _dbContext = context;
                
        }
        // GET: api/<LicenceController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var li= await _dbContext.Connection.ToListAsync();
            return new Success(true, "message.sucess", li);

        }

        // GET api/<LicenceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var licence = await this._dbContext.Connection.Where(l => l.Id == id).FirstOrDefaultAsync();
            return new Success(true, "message.sucess", licence);

        }

    }
}
