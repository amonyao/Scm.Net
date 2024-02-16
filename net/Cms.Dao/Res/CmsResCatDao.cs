using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 类别
    /// </summary>
    [SugarTable("cms_res_cat")]
    public class CmsResCatDao : ScmUnitDataDao
    {
        /// <summary>
        /// 类目类型，同文章类型
        /// </summary>
        [Required]
        public ArticleTypesEnum types { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Required]
        public long pid { get; set; }

        /// <summary>
        /// 顶级ID
        /// </summary>
        [Required]
        public long tid { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;

        /// <summary>
        /// 样式
        /// </summary>
        [Required]
        public long style_id { get; set; }
    }
}