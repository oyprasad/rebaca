namespace Signet.Core.Logging
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using log4net;
    using log4net.Config;

    public static class Logger
    {
        private static readonly bool isDebugEnabled;
        private static readonly bool isErrorEnabled;
        private static readonly bool isInfoEnabled;
        private static readonly bool isWarnEnabled;
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static Logger()
        {
            Init();
            isDebugEnabled = logger.IsDebugEnabled;
            isErrorEnabled = logger.IsErrorEnabled;
            isInfoEnabled = logger.IsInfoEnabled;
            isWarnEnabled = logger.IsWarnEnabled;
        }

        public static void Debug(string message)
        {
            if (isDebugEnabled)
            {
                GetLogger().Debug(message);
            }
        }

        public static void Error(string message)
        {
            if (isErrorEnabled)
            {
                GetLogger().Error(message);
            }
        }

        public static void Error(string message, Exception exception)
        {
            if (isErrorEnabled)
            {
                GetLogger().Error(message, exception);
            }
        }

        private static ILog GetLogger()
        {
            return logger;
        }

        public static void Info(string message)
        {
            if (isInfoEnabled)
            {
                GetLogger().Info(message);
            }
        }

        private static void Init()
        {
            FileInfo fi = new FileInfo(ConfigurationManager.AppSettings["log4net.config"]);
            if (fi.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(fi);
            }
            else
            {
                BasicConfigurator.Configure();
            }
        }

        public static void Warning(string message)
        {
            if (isWarnEnabled)
            {
                GetLogger().Warn(message);
            }
        }
    }
}