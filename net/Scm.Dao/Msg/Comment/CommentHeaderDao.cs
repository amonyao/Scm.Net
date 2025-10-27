using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Msg.Comment
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_msg_comment_header")]
    public class CommentHeaderDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string codec { get; set; }
        /// <summary>
        /// 引用ID
        /// </summary>
        public long rid { get; set; }

        /// <summary>
        /// 主题说明
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 评分分值
        /// </summary>
        [Required]
        public int score_value { get; set; }
        /// <summary>
        /// 评分次数
        /// </summary>
        public int score_count { get; set; }
    }
}