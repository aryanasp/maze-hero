using UnityEngine;

namespace Log
{
    public static class Logger
    {
        private static LoggerConfig _loggerConfig;
        static Logger()
        {
            _loggerConfig = Resources.Load<LoggerConfig>("Log/LoggerConfig");
        }

        public static void Log(string message, bool forceLog = false)
        {
            if (forceLog)
            {
                Debug.Log(message);
            }
            if (_loggerConfig.log)
            {
                Debug.Log(message);
            }
        }


        public static void Warning(string message, bool forceLog = false)
        {
            if (forceLog)
            {
                Debug.LogWarning(message);
            }
            if (_loggerConfig.log)
            {
                Debug.LogWarning(message);
            }
        }
        
        public static void Error(string message, bool forceLog = false)
        {
            if (forceLog)
            {
                Debug.LogError(message);
            }
            if (_loggerConfig.log)
            {
                Debug.LogError(message);
            }
        }
    }
}