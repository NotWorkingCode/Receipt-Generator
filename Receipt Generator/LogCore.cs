using System;
using System.IO;

namespace Receipt_Generator
{
    internal static class LogCore
    {
        private static StreamWriter _log;
        public static void Initialization()
        {
            string date = DateTime.Now.ToString("d/MM/yyyy");

            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            string logFilePath = "Logs\\" + date + ".log";

            FileStream stream = File.Exists(logFilePath) ? File.Open(logFilePath, FileMode.Append) : File.Create(logFilePath);
            _log = new StreamWriter(stream);
            
        }
        public static void Destroy()
        {
            _log.Close();
        }
        public static void Info(string message)
        {
            try { _log.WriteLineAsync("[" + DateTime.Now + "] [INFO] " + message); }
            catch { }
        }
        public static void Debug(string message)
        {
            try
            {
                _log.WriteLineAsync("[" + DateTime.Now + "] [DEBUG] " + message);
            }
            catch { }
        }
        public static void Error(string message)
        {
            try
            {
                _log.WriteLineAsync("[" + DateTime.Now + "] [ERROR] " + message);
            }
            catch { }
        }
        public static void Warning(string message)
        {
            try
            {
                _log.WriteLineAsync("[" + DateTime.Now + "] [WARNING] " + message);
            }
            catch { }
        }
    }
}
