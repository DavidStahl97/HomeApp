using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.UnitTests.Backend
{
    public class LoggerMock<T> : ILogger<T>
    {
        private readonly Action<LogLevel, string> _writeLogMock;

        public LoggerMock(Action<LogLevel, string> writeLogMock)
        {
            _writeLogMock = writeLogMock;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            _writeLogMock(logLevel, message);
        }
    }
}
