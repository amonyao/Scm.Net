using Com.Scm.Dao.Unit;
using Com.Scm.Enums;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 分享头档
    /// </summary>
    [SugarTable("cms_share_header")]
    public class CmsDocShareHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 公开类型
        /// </summary>
        public ShareTypesEnums types { get; set; }
        /// <summary>
        /// 公开方式
        /// </summary>
        public ShareModesEnums modes { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public long app_id { get; set; }

        /// <summary>
        /// 引用ID
        /// </summary>
        public long ref_id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(128)]
        public string title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(128)]
        public string image { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [StringLength(256)]
        public string summary { get; set; }
    }
}
