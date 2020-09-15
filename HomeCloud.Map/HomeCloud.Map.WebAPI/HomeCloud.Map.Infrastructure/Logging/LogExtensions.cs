using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HomeCloud.Infrastructure.Logging
{
    static class LogExtensions
    {
        public static ILogger AddCodeInformations(this ILogger logger, string path, string method, int line)
        {
            var classString = path.Split('\\').LastOrDefault();
            return logger
                .ForContext("class", classString)
                .ForContext("method", method)
                .ForContext("line", line);
        }
    }
}
