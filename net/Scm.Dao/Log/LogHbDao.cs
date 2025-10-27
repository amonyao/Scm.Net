using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Log
{
    /// <summary>
    /// 心跳日志
    /// </summary>
    [SugarTable("scm_log_hb")]
    public class LogHbDao : ScmDao
    {
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 内网地址
        /// </summary>
        [StringLength(128)]
        public string iip { get; set; }

        /// <summary>
        /// 外网地址
        /// </summary>
        public string oip { get; set; }

        /// <summary>
        /// 网卡地址
        /// </summary>
        [StringLength(128)]
        public string mac { get; set; }

        /// <summary>
        /// 主机名称
        /// </summary>
        [StringLength(256)]
        public string host { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        [StringLength(64)]
        public string os { get; set; }

        /// <summary>
        /// 运行环境
        /// </summary>
        [StringLength(16)]
        public string clr { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [StringLength(256)]
        public string user { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public long time { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public long create_time { get; set; }
    }
}
