using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Fav.Uri.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsFavUriDvo : ScmDataDvo
    {
        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public UriTypesEnum types { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string uri { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public int top { get; set; }

        /// <summary>
        /// 访问频次
        /// </summary>
        public int qty { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}