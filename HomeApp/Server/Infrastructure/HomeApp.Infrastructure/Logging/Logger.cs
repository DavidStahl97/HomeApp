using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApp.Infrastructure.Logging
{
    public class Logger : Application.ILogger
    {
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(exception, message);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(exception, message);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Information(message);
        }
    }
}
