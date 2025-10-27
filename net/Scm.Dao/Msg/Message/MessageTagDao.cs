using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Msg.Message
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_msg_message_tag")]
    public class MessageTagDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long message_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long tag_id { get; set; }

        /// <summary>
        /// 冗余
        /// </summary>
        public string label { get; set; }
    }
}
