using System;
using System.IO;
using log4net;
using log4net.Config;

namespace SSM.Common
{
    public class Logger
    {
        private static ILog logger;

        protected static ILog LoggerInstance
        {
            get
            {
                if (logger == null)
                {
                    var fileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                    XmlConfigurator.Configure(new FileInfo(fileName));
                    logger = LogManager.GetLogger(typeof(Logger));//LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);//
                }
                return logger;
            }
        }

        public static void Log(string message)
        {
            LoggerInstance.Info(message);
        }

        public static void Log(string messageFormat, params object[] args)
        {
            LoggerInstance.InfoFormat(messageFormat, args);
        }

        public static void LogDebug(string message)
        {
            LoggerInstance.Debug(message);
        }

        public static void LogDebug(string messageFormat, params object[] args)
        {
            LoggerInstance.DebugFormat(messageFormat, args);
        }

        public static void LogError(Exception exception)
        {
            LogError(exception.ToString());
            if (exception.InnerException != null)
                LogError(exception.InnerException);
        }

        public static void LogError(string message)
        {
            LoggerInstance.Error(message);
        }

        public static void LogError(string messageFormat, params object[] args)
        {
            LoggerInstance.ErrorFormat(messageFormat, args);
        }
    }
}
