namespace Signet.Core.Logging
{
    using System;
    using Signet.Core.Extensions;

    public class ConsoleLogger
    {
        private ConsoleLogger() { }

        public static void Log(LogLevel level, string message, params object[] args)
        {
            Logger.Info(message.FormatWith(args));
            message = level.ToString() + ": " + message;
            Console.WriteLine(message, args);
        }
    }

    public enum LogLevel
    {
        Debug = 1000,
        Verbose = 2000,
        Info = 3000,
        Warning = 4000,
        Error = 5000,
        None = 9999,
    }
}
