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

            string date = req.Form["Date"];
            string level = req.Form["Level"];
            string logger = req.Form["Logger"];
            string message = req.Form["Message"];

            date = ExtractData(date);
            level = ExtractData(level);
            logger = ExtractData(logger);
            message = ExtractData(message);

            log.LogInformation($"{date}-{level}-{logger}-{message}");

            return new OkObjectResult(null);
        }

        private static string ExtractData(string data){
            var index = data.IndexOf(":");
            var newStr = data.Substring(index+1);
            Regex rx = new Regex("\"(.*?)\"");
            var result = rx.Match(newStr).Value;
            result = result.Replace("\"","");
            return result;
        }
    }
}
