using Com.Scm.Dvo;

namespace Com.Scm.Cms.Res.Author.Dvo
{
    /// <summary>
    /// 作者
    /// </summary>
    public class CmsResAuthorDvo : ScmDataDvo
    {
        /// <summary>
        /// 国别
        /// </summary>
        public long nation_id { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        public long dynasty_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; } = 0;

        /// <summary>
        /// 编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string namee { get; set; }
    }
}