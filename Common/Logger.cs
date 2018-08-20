using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class Logger
    {

        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void LoadLogger<T>() {
            logger = NLog.LogManager.GetCurrentClassLogger(typeof(T));
        }
               
        public static void LogError(string message, object logData = null, Dictionary<string, string> parameters = null)
        {
            Log(message, NLog.LogLevel.Error, logData, parameters);
        }
               
        public static void Log(string message, NLog.LogLevel type, object logData = null, Dictionary<string, string> parameters = null)
        {
            var msg = $"{message} {Environment.NewLine} ";
            if (logData != null) msg += $" Details: {Environment.NewLine} {JsonConvert.SerializeObject(logData)} ";
            if (parameters != null) msg += $" Parameters: {Environment.NewLine} {JsonConvert.SerializeObject(parameters)} ";

            logger.Log(type, message: msg);
        }
               
        public static void LogException(string message, Exception exception, object logData = null, Dictionary<string, string> parameters = null)
        {
            var msg = $"{message} {Environment.NewLine} ";
            if (logData != null) msg += $" Details: {Environment.NewLine} {JsonConvert.SerializeObject(logData)} ";
            if (parameters != null) msg += $" Parameters: {Environment.NewLine} {JsonConvert.SerializeObject(parameters)} ";

            logger.Log(NLog.LogLevel.Error, exception: exception, message: msg);
        }


    }
}
