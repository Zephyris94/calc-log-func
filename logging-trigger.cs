using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AsterCalc.Function
{
    public static class logging_trigger
    {
        [FunctionName("logging_trigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string body = req.Form["Log"];
            int logIndex = body.IndexOf('=');
            string message = body.Substring(logIndex);

            var pattern = new Regex("[\"\\=<>/]");
            message = pattern.Replace(message,"");

            log.LogInformation($"{message}");

            return new OkObjectResult(null);
        }
    }
}
