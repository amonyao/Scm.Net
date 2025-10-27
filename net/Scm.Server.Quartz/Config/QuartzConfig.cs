namespace Com.Scm.Quartz.Config
{
    public class QuartzConfig
    {
        public const string NAME = "Quartz";

        /// <summary>
        /// 数据模式（文件或数据库）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 任务文件
        /// </summary>
        public string JobFile { get; set; }

        /// <summary>
        /// 默认目录
        /// </summary>
        public string BaseDir { get; set; }

        /// <summary>
        /// 数据目标
        /// </summary>
        public string DataDir { get; set; }

        /// <summary>
        /// 日志目录
        /// </summary>
        public string LogsDir { get; set; }

        public void LoadDef()
        {
            if (string.IsNullOrWhiteSpace(Type))
            {
                Type = "file";
            }

            if (string.IsNullOrWhiteSpace(BaseDir))
            {
                BaseDir = "quartz";
            }

            if (string.IsNullOrWhiteSpace(DataDir))
            {
                DataDir = "data";
            }

            if (string.IsNullOrWhiteSpace(LogsDir))
            {
                LogsDir = "logs";
            }

            if (string.IsNullOrEmpty(JobFile))
            {
                JobFile = "jobs.json";
            }
        }
    }
}
