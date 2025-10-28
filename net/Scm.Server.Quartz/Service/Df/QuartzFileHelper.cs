using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Dao;
using Com.Scm.Utils;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace Com.Scm.Quartz.Service.Df
{
    public class QuartzFileHelper
    {
        private string _rootPath { get; set; }

        private string _dataPath { get; set; }

        private string _logPath { get; set; }

        private QuartzConfig _Config;

        public QuartzFileHelper(QuartzConfig config)
        {
            _Config = config;

            CreateQuartzRootPath();
        }

        /// <summary>
        /// 初始化
        /// 创建作业所在根目录及日志文件夹 
        /// </summary>
        /// <returns></returns>
        public string CreateQuartzRootPath()
        {
            if (!string.IsNullOrEmpty(_rootPath))
            {
                return _rootPath;
            }

            // 根目录
            _rootPath = _Config.BaseDir;
            if (string.IsNullOrEmpty(_rootPath))
            {
                _rootPath = Environment.CurrentDirectory;
            }

            // 数据目录
            _dataPath = Path.Combine(_rootPath, _Config.DataDir);
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }

            // 日志目录
            _logPath = Path.Combine(_rootPath, _Config.LogsDir);
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }

            return _rootPath;
        }

        /// <summary>
        /// 获取jobs
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<QuarzTaskDao> GetJobs(Expression<Func<QuarzTaskDao, bool>> where = null)
        {
            List<QuarzTaskDao> list = new List<QuarzTaskDao>();

            string path = Path.Combine(_dataPath, _Config.JobFile);
            if (!File.Exists(path))
            {
                return list;
            }

            var tasks = ReadFile(path);
            if (string.IsNullOrEmpty(tasks))
            {
                return null;
            }

            var _taskList = tasks.AsJsonObject<List<QuarzTaskDao>>();
            if (where == null)
            {
                return _taskList;
            }

            return _taskList.Where(where.Compile()).ToList();
        }

        /// <summary>
        /// 读取任务日志
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="groupName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<QuarzTaskLogDao> GetJobRunLog(string taskName, string groupName, int page, int pageSize = 100)
        {
            List<QuarzTaskLogDao> list = new List<QuarzTaskLogDao>();

            string path = Path.Combine(_logPath, groupName, taskName);
            if (!File.Exists(path))
            {
                return list;
            }

            var logs = ReadPageLine(path, page, pageSize, true);
            foreach (string item in logs)
            {
                string[] arr = item?.Split('_');
                if (item == "" || arr == null || arr.Length == 0)
                    continue;
                if (arr.Length != 3)
                {
                    list.Add(new QuarzTaskLogDao() { remark = item });
                    continue;
                }
                list.Add(new QuarzTaskLogDao() { begin_time = Convert.ToDateTime(arr[0]), end_time = Convert.ToDateTime(arr[1]), remark = arr[2] });
            }

            return list.OrderByDescending(x => x.begin_time).ToList();
        }

        /// <summary>
        /// 写入任务(全量)
        /// </summary>
        /// <param name="taskList"></param>
        public void WriteJobConfig(List<QuarzTaskDao> taskList)
        {
            string jobs = taskList.ToJsonString();
            //写入配置文件
            WriteFile(_dataPath, _Config.JobFile, jobs);
        }

        public void WriteStartLog(string content)
        {
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + content + Environment.NewLine;

            WriteFile(_logPath, "start.txt", content, true);
        }

        public void WriteJobLogs(QuarzTaskLogDao log)
        {
            var content = log.ToJsonString() + Environment.NewLine;
            WriteFile(_logPath, "logs.txt", content, true);
        }

        public List<QuarzTaskLogDao> GetJobsLog(int pageSize = 1)
        {
            string path = Path.Combine(_logPath, "logs.txt");
            if (!File.Exists(path))
                return null;

            var listlogs = ReadPageLine(path, pageSize, 5000, true).ToList();
            List<QuarzTaskLogDao> listtasklogs = new List<QuarzTaskLogDao>();
            foreach (var item in listlogs)
            {
                listtasklogs.Add(JsonSerializer.Deserialize<QuarzTaskLogDao>(item));
            }
            return listtasklogs;
        }

        public string RootPath
        {
            get { return _rootPath; }
        }

        public string DataPath
        {
            get { return _dataPath; }
        }

        public string LogPath
        {
            get { return _logPath; }
        }

        /// <summary>
        /// 读取本地txt日志内容
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="seekEnd"></param>
        /// <returns></returns>
        public IEnumerable<string> ReadPageLine(string fullPath, int page, int pageSize, bool seekEnd = false)
        {
            if (page <= 0)
            {
                page = 1;
            }
            var lines = File.ReadLines(fullPath, Encoding.UTF8);
            if (seekEnd)
            {
                int lineCount = lines.Count();
                int linPageCount = (int)Math.Ceiling(lineCount / (pageSize * 1.00));
                //超过总页数，不处理
                if (page > linPageCount)
                {
                    page = 0;
                    pageSize = 0;
                }
                else if (page == linPageCount)//最后一页，取最后一页剩下所有的行
                {
                    pageSize = lineCount - (page - 1) * pageSize;
                    if (page == 1)
                    {
                        page = 0;
                    }
                    else
                    {
                        page = lines.Count() - page * pageSize;
                    }
                }
                else
                {
                    page = lines.Count() - page * pageSize;
                }
            }
            else
            {
                page = (page - 1) * pageSize;
            }
            lines = lines.Skip(page).Take(pageSize);

            var enumerator = lines.GetEnumerator();
            int count = 1;
            while (enumerator.MoveNext() || count <= pageSize)
            {
                yield return enumerator.Current;
                count++;
            }
            enumerator.Dispose();
        }

        public string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }

            using (StreamReader stream = new StreamReader(path))
            {
                return stream.ReadToEnd(); // 读取文件
            }
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public void WriteFile(string path, string fileName, string content, bool appendToLast = false)
        {
            if (!Directory.Exists(path))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            var file = Path.Combine(path, fileName);
            using (FileStream stream = File.Open(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] by = Encoding.Default.GetBytes(content);
                if (appendToLast)
                {
                    stream.Position = stream.Length;
                }
                else
                {
                    stream.SetLength(0);
                }
                stream.Write(by, 0, by.Length);
            }
        }
    }
}
