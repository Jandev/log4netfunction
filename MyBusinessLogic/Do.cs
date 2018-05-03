using System;
using log4net;

namespace MyBusinessLogic
{
    public class Do : IDo
    {
        private readonly ILog log;

        public Do(ILog log)
        {
            this.log = log;
        }
        public void Stuff()
        {
            if (new Random().Next() % 10 == 0)
            {
                throw new ArgumentException("Some major exception which isn't catched!");
            }
            log.Debug("My debug message");

            log.Info("Some informational message");

            log.Warn("My warning");

            log.Error("An error message");

            log.Fatal("The fatal message");
        }
    }
}
