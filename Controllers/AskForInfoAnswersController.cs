using E_proc.Models;
using E_proc.Models.StatusModel;
using E_proc.MyHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AskForInfoAnswersController : ControllerBase
    {
        private readonly AuthContext authContext;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public AskForInfoAnswersController(AuthContext authContext, IHubContext<NotificationHub> notificationHubContext)
        {
            this.authContext = authContext;
            _notificationHubContext = notificationHubContext;
        }
        // GET: api/<AskForInfoAnswersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AskForInfoAnswersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AskForInfoAnswersController>
        [HttpPost]
        public async Task<IActionResult> Post(AskForInfoAnswer askForInfoAnswer)
        {
            await _notificationHubContext.Clients.Group("AskInfoChat"+askForInfoAnswer.AskForInfoId).SendAsync("SendMessage", askForInfoAnswer);
            await _notificationHubContext.Clients.Group("AskInfoNotificationCitizen").SendAsync("NewAnswer", askForInfoAnswer);


            await _notificationHubContext.Clients.Group("citizenNotificationCenter").SendAsync("aswerAskInfoNotification", askForInfoAnswer);




            var answer = await authContext.AskForInfoAnswer.AddAsync(askForInfoAnswer);
        
           var askInfo= await authContext.AskForInfo.Where(o => o.Id == askForInfoAnswer.AskForInfoId).FirstOrDefaultAsync();
            askInfo.AskForInfoAnswer = askForInfoAnswer;
            askInfo.updatedAt= new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            await authContext.SaveChangesAsync();

            return new Success(true, "message.sucess", askForInfoAnswer);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id,AskForInfoAnswer askAnswer)
        {

            var answer = await authContext.AskForInfoAnswer.Where(o => o.Id.ToString() == id).FirstOrDefaultAsync();
            answer.message = askAnswer.message;
            answer.Seen = askAnswer.Seen;
            await authContext.SaveChangesAsync();
            return new Success(true, "message.success");

        }

        // DELETE api/<AskForInfoAnswersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
