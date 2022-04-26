using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AskForInfosController : ControllerBase
    {
        private readonly AuthContext authContext;
        public AskForInfosController(AuthContext authContext)
        {
                this.authContext = authContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
      
                var askForInfos = await authContext.AskForInfo.ToListAsync();

          
            return new Success(true, "message.sucess", askForInfos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var askForInfo = await authContext.AskForInfo.Where(o=>o.Id==id).FirstOrDefaultAsync();

            return new Success(true, "message.success", askForInfo);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post( AskForInfo askForInfo)
        {
            var added=  await authContext.AskForInfo.AddAsync(askForInfo);
            await authContext.SaveChangesAsync();
            return new Success(true, "message.success");

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
