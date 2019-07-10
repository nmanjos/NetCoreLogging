using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace NetCoreLogger
{
    static public class LogstashLoggerExtentions
    {
        static public ILoggingBuilder AddLogstashLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider,
                                              LogstashLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton
               <IConfigureOptions<LogstashLoggerOptions>, LogstashLoggerOptionsSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton
               <IOptionsChangeTokenSource<LogstashLoggerOptions>,
               LoggerProviderOptionsChangeTokenSource<LogstashLoggerOptions, LogstashLoggerProvider>>());
            return builder;
        }

        static public ILoggingBuilder AddLogstashLogger
               (this ILoggingBuilder builder, Action<LogstashLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddLogstashLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
