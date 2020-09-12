using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HomeApp.Application
{
    public interface ILogger
    {
        /// <summary>
        /// internal control flow and diagnostic state dumps to facilitate pinpointing of recognised problems
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message, 
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// events of interest or that have relevance to outside observers; the default enabled minimum logging level
        /// </summary>
        /// <param name="message"></param>
        void Information(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// indicators of possible issues or service/functionality degradation
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// indicating a failure within the application or connected system
        /// </summary>
        /// <param name="message"></param>
        void Error(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// indicating a failure within the application or connected system
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(string message, Exception exception,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// critical errors causing complete failure of the application
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);

        /// <summary>
        /// critical errors causing complete failure of the application
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Fatal(string message, Exception exception,
            [CallerFilePath] string path = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int line = 0);        
    }
}
