using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreLogger
{
    [Microsoft.Extensions.Logging.ProviderAlias("Logstash")]
    public class LogstashLoggerProvider : LoggerProvider
    {
        bool Terminated;
        int delay = 100;
 
        ConcurrentQueue<LogEntry> InfoQueue = new ConcurrentQueue<LogEntry>();

        void SendLogLine()
        {
            // TODO send Log Line to server
            // use http post protocol
            // autenticate if username is not empty
            // rotate throug servers if there is more than one.
            if (this.Settings.Servers.Count > 0)  // if by any chance the Server settings is not loaded, do nothing
            {
                // TODO Send Code here
            }
        }

        void ThreadProc()
        {
            Task.Run(() => {

                while (!Terminated)
                {
                    try
                    {
                        
                        SendLogLine();
                        System.Threading.Thread.Sleep(delay);
                    }
                    catch // (Exception ex)
                    {
                    }
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            Terminated = true;
            
            InfoQueue.Clear();
            base.Dispose(disposing);
        }


        public LogstashLoggerProvider(IOptionsMonitor<LogstashLoggerOptions> Settings)
            : this(Settings.CurrentValue)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens
            SettingsChangeToken = Settings.OnChange(settings => {
                this.Settings = settings;
            });
        }

        public LogstashLoggerProvider(LogstashLoggerOptions Settings)
        {
            
            this.Settings = Settings;

            

            ThreadProc();
        }

        public override bool IsEnabled(LogLevel logLevel)
        {
            bool Result = logLevel != LogLevel.None
                && this.Settings.LogLevel != LogLevel.None
                && Convert.ToInt32(logLevel) >= Convert.ToInt32(this.Settings.LogLevel);

            return Result;
        }

        public override void WriteLog(LogEntry Info)
        {
            InfoQueue.Enqueue(Info);
        }

        internal LogstashLoggerOptions Settings { get; private set; }
    }
}
