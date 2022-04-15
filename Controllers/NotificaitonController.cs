
using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace E_proc.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class NotificaitonController : ControllerBase
        {
            private readonly AuthContext _dbContext;

            public NotificaitonController(AuthContext context)
            {
                _dbContext = context;

            }
            // GET: api/<LicenceController>
            [HttpGet]
            public async Task<IActionResult> GetAsync()
            {
                var li = await _dbContext.Notification.Include(n=>n.Offer).ThenInclude(o => o.Supplier).Include(n=>n.Institute).ToListAsync();
                return new Success(true, "message.sucess", li);

            }

            // GET api/<LicenceController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {

                var licence = await this._dbContext.Notification.Include(n => n.Offer).ThenInclude(o => o.Supplier).Include(n => n.Institute).Where(l => l.Id == id).FirstOrDefaultAsync();
                return new Success(true, "message.sucess", licence);

            }


         [HttpGet("destination/{id}")]
        public async Task<IActionResult> GetByDestination(int id)
        {

            var notification = await this._dbContext.Notification.Include(n => n.Offer).ThenInclude(o => o.Supplier).Include(n => n.Institute).Where(l => l.InstituteId==id).ToListAsync();
            return new Success(true, "message.sucess", notification);

        }

    }
    }



