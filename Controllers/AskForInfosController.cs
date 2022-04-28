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
        public async Task<IActionResult> Get(string? instituteId = null, bool? confirmed = null, string? phone = null, DateTime? date = null)
        {

            if (instituteId == null && confirmed == null && phone == null && date == null)
            {
                var askForInfos = await authContext.AskForInfo.ToListAsync();


                return new Success(true, "message.sucess", askForInfos);


            }
            else
            {

                var askForInfos = await authContext.AskForInfo
                         .Where(s => !string.IsNullOrEmpty(instituteId) ? s.Tender.instituteId.ToString() == instituteId : true)
                         .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                         .Where(s => confirmed.HasValue ? s.SendToChat == confirmed : true)
                        // .Where(s => date.HasValue ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)
                         .ToListAsync();
                return new Success(true, "message.sucess", askForInfos);


            }

        }


        [HttpGet("{id}")]
        
        public async Task<IActionResult> Get(int id)
        {

            var askForInfo = await authContext.AskForInfo.Where(o=>o.Id==id).FirstOrDefaultAsync();
            var askForInfoanswer = await authContext.AskForInfoAnswer.Where(o => o.AskForInfoId == id).FirstOrDefaultAsync();
            if(askForInfoanswer!=null)
            {
                askForInfo.AskForInfoAnswer= askForInfoanswer;
                askForInfo.AskForInfoAnswerId = askForInfoanswer.Id;

            }
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
