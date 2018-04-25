using Autofac;
using Microsoft.Extensions.Logging;
using MyBusinessLogic;

namespace log4netfunction
{
    internal class Dependency
    {
        internal static IContainer Container { get; private set; }
        public static void CreateContainer(ILogger logger)
        {
            if (Container == null)
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<Do>().As<IDo>();

                builder.RegisterModule(new LoggingModule(logger));

                Container = builder.Build();
            }
        }
    }
}
