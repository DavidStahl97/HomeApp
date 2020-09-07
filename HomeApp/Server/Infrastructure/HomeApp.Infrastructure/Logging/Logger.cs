using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HomeApp.Infrastructure.Logging
{
    public class Logger : Application.ILogger
    {
        public void Debug(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Debug(message);
        }

        public void Error(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Error(message);
        }

        public void Error(string message, Exception exception,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Error(exception, message);
        }

        public void Fatal(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Fatal(message);
        }

        public void Fatal(string message, Exception exception,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Fatal(exception, message);
        }

        public void Information(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {
            Logging(path, method, line).Information(message);
        }

        public void Warning(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0)
        {            
            Logging(path, method, line).Information(message);
        }

        private static ILogger Logging(string path, string method, int line)
        {
            return Log.Logger.AddCodeInformations(path, method, line);
        }
    }
}
