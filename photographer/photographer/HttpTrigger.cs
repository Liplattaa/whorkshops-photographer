
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace photographer
{
    public static class HttpTrigger
    {
        [FunctionName("HttpTrigger")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")]HttpRequest req,
            [Queue("demomessages", Connection = "queueConn")] ICollector<string> outputMessage, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string email = data?.email;
            string photoWidth = data?.photoWidth;
            string photoHeight= data?.photoHeight;
            string photoName = data?.photoName;

            if (data == null || name == null || email == null || photoWidth == null || photoHeight == null ||  photoName == null)
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            return (ActionResult)new OkObjectResult($"Hello, {name}");
          
        }
    }
}
