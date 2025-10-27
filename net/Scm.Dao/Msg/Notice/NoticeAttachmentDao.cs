using Com.Scm.Dao;

namespace Com.Scm.Msg.Notice
{
    /// <summary>
    /// 附件
    /// </summary>
    [SqlSugar.SugarTable("scm_msg_notice_attachment")]
    public class NoticeAttachmentDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long notice_id { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string url { get; set; }
    }
}
