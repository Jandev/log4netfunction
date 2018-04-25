﻿using System;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;

namespace log4netfunction
{
    public class LoggingModule : Autofac.Module
    {
        private readonly ILogger logger;

        public LoggingModule(ILogger logger)
        {
            this.logger = logger;
            log4net.Config.BasicConfigurator.Configure();
        }
        private void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, GetLogger(instanceType), null);
            }
        }

        private ILog GetLogger(Type instanceType)
        {
            ILog log = LogManager.GetLogger(instanceType);
            Logger log4NetLogger = (Logger) log.Logger;
            log4NetLogger.RemoveAllAppenders();
            log4NetLogger.AddAppender(new FunctionLoggerAppender(this.logger));
            return log;
        }

        private void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter(
                        (p, i) => p.ParameterType == typeof(ILog),
                        (p, i) => GetLogger(p.Member.DeclaringType)
                    ),
                });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }
    }
}
