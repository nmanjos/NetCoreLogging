using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace NetCoreLogger
{
    internal class LogstashLoggerOptionsSetup : ConfigureFromConfigurationOptions<LogstashLoggerOptions>
    {
        public LogstashLoggerOptionsSetup(ILoggerProviderConfiguration<LogstashLoggerProvider>
                                      providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
