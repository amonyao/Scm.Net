using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc.Poetry.Dvo
{
    public class PoetryDvo : ScmDataDvo
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 国别
        /// </summary>
        [Required]
        public long nation_id { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        [Required]
        public long dynasty_id { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required]
        public long author_id { get; set; }

        /// <summary>
        /// 出处
        /// </summary>
        [Required]
        public long origin_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public VisibleEnum visible { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string content { get; set; }

        /// <summary>
        /// 是否批量添加
        /// </summary>
        public bool batch { get; set; }

        /// <summary>
        /// 换行数量
        /// </summary>
        public int blank { get; set; }
    }
}
