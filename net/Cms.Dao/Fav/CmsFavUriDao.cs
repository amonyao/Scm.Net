using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fav
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_fav_uri")]
    public class CmsFavUriDao : ScmUnitDataDao
    {
        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public UriTypesEnum types { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [StringLength(1024)]
        public string uri { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        [Required]
        public int top { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 访问频次
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public string remark { get; set; }
    }
}