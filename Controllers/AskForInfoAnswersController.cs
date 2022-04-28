using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AskForInfoAnswersController : ControllerBase
    {
        private readonly AuthContext authContext;
        public AskForInfoAnswersController(AuthContext authContext)
        {
            this.authContext = authContext;
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
            await authContext.AskForInfoAnswer.AddAsync(askForInfoAnswer);
            await authContext.SaveChangesAsync();

            return new Success(true, "message.sucess", askForInfoAnswer);

        }

        // PUT api/<AskForInfoAnswersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AskForInfoAnswersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
