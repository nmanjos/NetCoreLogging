using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreLogger
{
    public class LogstashLoggerOptions
    {
        List<LogstashServers> lServers = new List<LogstashServers>();
        


        public LogstashLoggerOptions()
        {
        }

        public LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;

        /// <summary>
        /// defauts to http://localhost:9200 if nothing is passed
        /// </summary>
        public List<LogstashServers> Servers
        {
            get
            {
                if (lServers.Count == 0)
                {
                    lServers.Add(new LogstashServers { Url = "http://localhost:9200", Username="",Password= "" });
                }
                return lServers;
            }
            set { lServers = value; }
        }

    }
    
}
