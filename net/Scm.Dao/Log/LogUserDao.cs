using Com.Scm.Dao;
using Com.Scm.Enums;
using SqlSugar;

namespace Com.Scm.Log
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    [SugarTable("scm_log_user")]
    public class LogUserDao : ScmDao
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public ScmClientTypeEnum client { get; set; }
        /// <summary>
        /// 登录模式
        /// </summary>
        public ScmLoginModeEnum mode { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 登录地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>
        public ScmBoolEnum result { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { get; set; }
    }
}
