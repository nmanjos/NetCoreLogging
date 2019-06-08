using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLogging
{
    public class FileLoggerOptions
    {
        string fFolder;
        int fMaxFileSizeInMB;
        int fRetainPolicyFileCount;

        public FileLoggerOptions()
        {
        }

        public LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;

        public string Folder
        {
            get
            {
                return !string.IsNullOrWhiteSpace(fFolder) ?
                fFolder : System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
            }
            set { fFolder = value; }
        }

        public int MaxFileSizeInMB
        {
            get { return fMaxFileSizeInMB > 0 ? fMaxFileSizeInMB : 2; }
            set { fMaxFileSizeInMB = value; }
        }

        public int RetainPolicyFileCount
        {
            get { return fRetainPolicyFileCount < 5 ? 5 : fRetainPolicyFileCount; }
            set { fRetainPolicyFileCount = value; }
        }
    }
}
