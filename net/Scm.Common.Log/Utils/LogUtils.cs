using Serilog;
using Serilog.Filters;
using System;
using System.IO;

namespace Com.Scm.Utils
{
    public class LogUtils
    {
        private static string _LogDir = "logs";
        private static string DbLog = "db";
        private static string ApiLog = "api";
        private static string ErrorLog = "error";

        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <param name="root">日志根目录</param>
        public static void Setup(string logDir = null)
        {
            if (string.IsNullOrWhiteSpace(logDir))
            {
                logDir = "logs";
            }
            _LogDir = logDir;

            CreateFolder(Path.Combine(_LogDir, DbLog));
            CreateFolder(Path.Combine(_LogDir, ApiLog));
            CreateFolder(Path.Combine(_LogDir, ErrorLog));

            var template = "{NewLine}Time：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}"
                + "{NewLine}Level：{Level}"
                + "{NewLine}Message：{Message}"
                + "{NewLine}{Exception}"
                + new string('-', 100);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                //.MinimumLevel.Information()
                //.WriteTo.File(formatter:new CompactJsonFormatter(),"logs\\test.txt",rollingInterval:RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == DbLog)).WriteTo.Async(a => a.File(GetLogFile(DbLog), rollingInterval: RollingInterval.Day, outputTemplate: template)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == ApiLog)).WriteTo.Async(a => a.File(GetLogFile(ApiLog), rollingInterval: RollingInterval.Day, outputTemplate: template)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("position", p => p == ErrorLog)).WriteTo.Async(a => a.File(GetLogFile(ErrorLog), rollingInterval: RollingInterval.Day, outputTemplate: template)))
                .CreateLogger();
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fileName"></param>
        public static void Debug(string msg, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = ApiLog;
            }

            Log.Debug($"{{position}}:{msg}", fileName);
        }

        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fileName"></param>
        public static void Info(string msg, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = ApiLog;
            }

            //输入其他的话，还是存放到ApiLog文件夹
            Log.Information($"{{position}}:{msg}", fileName);
        }

        ///// <summary>
        ///// 消息日志
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="obj">对象</param>
        ///// <param name="fileName"></param>
        //public static void Info(string msg, object obj, string fileName = null)
        //{
        //    if (string.IsNullOrWhiteSpace(fileName))
        //    {
        //        fileName = ApiLog;
        //    }

        //    //输入其他的话，还是存放到ApiLog文件夹
        //    Log.Information($"{{position}}:{msg}-{obj.ToJsonString()}", fileName);
        //}

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            Log.Error(ex, "{position}:" + ex.Message, ErrorLog);
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Log.Error($"{{position}}:{msg}", ErrorLog);
        }

        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetLogFile(string fileName)
        {
            return Path.Combine(_LogDir, fileName, "log.log");
        }
    }
}