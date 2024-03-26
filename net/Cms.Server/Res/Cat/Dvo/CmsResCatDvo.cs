using Com.Scm.Dvo;

namespace Com.Scm.Cms.Res.Cat.Dvo
{
    /// <summary>
    /// 类别
    /// </summary>
    public class CmsResCatDvo : ScmDataDvo
    {
        /// <summary>
        /// 类目类型，同文章类型
        /// </summary>
        public int types { get; set; } = 0;

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; } = 0;

        /// <summary>
        /// 名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 顶级ID
        /// </summary>
        public long tid { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        public int qty { get; set; } = 0;

        /// <summary>
        /// 样式
        /// </summary>
        public long style_id { get; set; }
    }
}