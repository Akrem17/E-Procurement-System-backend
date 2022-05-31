using E_proc.Models;
using E_proc.Models.StatusModel;
using E_proc.MyHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AskForInfosController : ControllerBase
    {
        private readonly AuthContext authContext;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public AskForInfosController(AuthContext authContext, IHubContext<NotificationHub> notificationHubContext)
        {
                this.authContext = authContext;
            this._notificationHubContext = notificationHubContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get(string? instituteId = null, string? citizenId = null, string? phone = null, DateTime? date = null)
        {

            if (instituteId == null && citizenId == null && phone == null && date == null)
            {
                var askForInfos = await authContext.AskForInfo.Include(o=>o.AskForInfoAnswer).ToListAsync();


                    return new Success(true, "message.sucess", askForInfos);


            }
            else
            {

                var askForInfos = await authContext.AskForInfo
                    .Include(o => o.AskForInfoAnswer)

                         .Where(s => !string.IsNullOrEmpty(instituteId) ? s.Tender.instituteId.ToString() == instituteId : true)
                         .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                         .Where(s => !string.IsNullOrEmpty(citizenId) ? s.CitizenId.ToString() == citizenId : true)
                                                  // .Where(s => date.HasValue ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)
                        .OrderBy(s => !string.IsNullOrEmpty(citizenId) ? s.AskForInfoAnswer.CreatedAt : Convert.ToInt64(s.createdAt))

                         //.OrderBy(o => o.AskForInfoAnswer.CreatedAt)
                         .Reverse()
                         
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

           // var id = authContext.AskForInfo.Ma;
            var added=  await authContext.AskForInfo.AddAsync(askForInfo);
            await authContext.SaveChangesAsync();
            

            await _notificationHubContext.Clients.Group("askInfoNotificationInstitute").SendAsync("NewAsk", askForInfo);
            //lezm tshouf kifesh trajaa id de askForInfo whekek tnajem tfiltrihom fl front
            return new Success(true, "message.success");

        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put( AskForInfo askForInfo)
        {

           var askInfo = await authContext.AskForInfo.Where(o=>o.Id==askForInfo.Id).FirstOrDefaultAsync();
            askInfo.Seen = askForInfo.Seen;
            await authContext.SaveChangesAsync();
            return new Success(true, "message.success");

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
