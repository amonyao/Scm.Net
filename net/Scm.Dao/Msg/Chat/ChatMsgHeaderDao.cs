﻿using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Msg.Chat
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_msg_chat_header")]
    public class ChatMsgHeaderDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string namec { get; set; }
    }
}
