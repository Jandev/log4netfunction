using log4net.Appender;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace log4netfunction
{
    internal class FunctionLoggerAppender : AppenderSkeleton
    {
        private readonly ILogger logger;

        public FunctionLoggerAppender(ILogger logger)
        {
            this.logger = logger;
        }
        protected override void Append(LoggingEvent loggingEvent)
        {
            switch (loggingEvent.Level.Name)
            {
                case "DEBUG":
                    this.logger.LogDebug($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
                case "INFO":
                    this.logger.LogInformation($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
                case "WARN":
                    this.logger.LogWarning($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
                case "ERROR":
                    this.logger.LogError($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
                case "FATAL":
                    this.logger.LogCritical($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
                default:
                    this.logger.LogTrace($"{loggingEvent.LoggerName} - {loggingEvent.RenderedMessage}");
                    break;
            }
        }
    }
}
