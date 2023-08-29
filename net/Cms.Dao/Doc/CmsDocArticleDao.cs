using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 文章
    /// </summary>
    [SugarTable("cms_doc_article")]
    public class CmsDocArticleDao : ScmUnitDataDao
    {
        /// <summary>
        /// 键
        /// </summary>
        [StringLength(32)]
        public string key { get; set; }

        /// <summary>
        /// 盐
        /// </summary>
        [Required]
        public string salt { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public ArticleTypesEnum types { get; set; } = 0;

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string sub_title { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty0 { get; set; } = 0;

        /// <summary>
        /// 收藏数量
        /// </summary>
        [Required]
        public int fav_qty { get; set; } = 0;

        /// <summary>
        /// 留言数量
        /// </summary>
        [Required]
        public int msg_qty { get; set; } = 0;

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
        /// 样式
        /// </summary>
        [Required]
        public long style_id { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(64)]
        public string image { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required]
        public int files { get; set; } = 0;

        /// <summary>
        /// 背景颜色
        /// </summary>
        [StringLength(16)]
        public string back_color { get; set; }

        /// <summary>
        /// 背景图像
        /// </summary>
        [StringLength(64)]
        public string back_image { get; set; }

        /// <summary>
        /// 文本颜色
        /// </summary>
        [StringLength(16)]
        public string text_color { get; set; }

        /// <summary>
        /// 字体名称
        /// </summary>
        [StringLength(16)]
        public string font_name { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        [Required]
        public int font_size { get; set; } = 12;

        /// <summary>
        /// 布局
        /// </summary>
        [StringLength(8)]
        public string layout { get; set; }

        /// <summary>
        /// 排列方式
        /// </summary>
        [Required]
        public OriginEnum origin_types { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public VisibleEnum visible { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required, StringLength(1024)]
        public string summary { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.salt = new Random().Next(10000).ToString("d4");
            this.key = this.id + this.salt;
        }
    }
}