using System;
using Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MyBusinessLogic;

namespace log4netfunction
{
    public static class EveryFiveSeconds
    {
        [FunctionName("EveryFiveSeconds")]
        public static void Run(
            [TimerTrigger("*/5 * * * * *", RunOnStartup = true)]TimerInfo myTimer, 
            ILogger logger)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Dependency.CreateContainer(logger);

            var doLogic = Dependency.Container.Resolve<IDo>();
            doLogic.Stuff();
        }
    }
}
