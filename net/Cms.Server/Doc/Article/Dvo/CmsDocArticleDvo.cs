using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 文章
    /// </summary>
    public class CmsDocArticleDvo : ScmDataDvo
    {
        /// <summary>
        /// 键
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ArticleTypesEnum types { get; set; } = 0;

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sub_title { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; } = 0;

        /// <summary>
        /// 收藏数量
        /// </summary>
        public int fav_qty { get; set; } = 0;

        /// <summary>
        /// 留言数量
        /// </summary>
        public int msg_qty { get; set; } = 0;

        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 国别
        /// </summary>
        public long nation_id { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        public long dynasty_id { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public long author_id { get; set; }

        /// <summary>
        /// 出处
        /// </summary>
        public long origin_id { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public long style_id { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public int files { get; set; } = 0;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string back_color { get; set; }

        /// <summary>
        /// 背景图像
        /// </summary>
        public string back_image { get; set; }

        /// <summary>
        /// 文本颜色
        /// </summary>
        public string text_color { get; set; }

        /// <summary>
        /// 字体名称
        /// </summary>
        public string font_name { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int font_size { get; set; } = 12;

        /// <summary>
        /// 布局
        /// </summary>
        public string layout { get; set; }

        /// <summary>
        /// 排列方式
        /// </summary>
        public OriginEnum origin_types { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public VisibleEnum visible { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string style { get; set; }
    }
}