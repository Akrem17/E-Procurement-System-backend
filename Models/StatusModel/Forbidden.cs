using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_proc.Models.StatusModel
{
    public class Forbidden : IActionResult
    {
        public bool status { get; set; }
        public string message { get; set; }

        public Object data { get; set; }

        public Forbidden(bool status, string message, object? data = null)
        {
            data = data ?? null;
            this.status = status;
            this.message = message;
            this.data = data;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var respone = context.HttpContext.Response;
            respone.StatusCode = 403;
            var myData = new
            {
                status,
                message,
                data,

            };
            return respone.WriteAsJsonAsync(myData);


        }
    }

}
