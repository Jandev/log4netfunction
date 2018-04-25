using System.Net;
using System.Net.Http;
using Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyBusinessLogic;

namespace log4netfunction
{
    public static class LogViaHttp
    {
        [FunctionName("LogViaHttp")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req,
            ILogger logger)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            Dependency.CreateContainer(logger);

            var doLogic = Dependency.Container.Resolve<IDo>();
            doLogic.Stuff();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
