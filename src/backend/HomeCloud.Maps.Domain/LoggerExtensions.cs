using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Domain
{
    public static class LoggerExtensions
    {
        public static void WriteInformation<T>(this ILogger<T> logger, string title, 
            params (string Name, object Value)[] properties)
        {
            string message = title;

            foreach (var (Name, Value) in properties)
            {
                message += " \n " + Name + ": {" + Name + "}";
            }

            var values = properties.Select(x => x.Value).ToArray();
            logger.LogInformation(message, values);
        }
    }
}
