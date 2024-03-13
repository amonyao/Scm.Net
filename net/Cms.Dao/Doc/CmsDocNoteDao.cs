using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 记事功能
    /// </summary>
    [SugarTable("cms_doc_note")]
    public class CmsDocNoteDao : ScmUnitDataDao
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
        [StringLength(4)]
        public string salt { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public NoteTypesEnum types { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(128)]
        public string title { get; set; }

        /// <summary>
        /// 子标题
        /// </summary>
        [StringLength(256)]
        public string sub_title { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        [Required]
        public int fav_qty { get; set; }

        /// <summary>
        /// 留言数量
        /// </summary>
        [Required]
        public int msg_qty { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Required]
        [StringLength(1024)]
        public string summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(2048)]
        public string content { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required]
        public int files { get; set; }

        public int ver { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.salt = new Random().Next(10000).ToString("d4");
            this.key = this.id + this.salt;
            this.ver = 1;
        }

        public override void PrepareUpdate(long userId, long unitId = 0)
        {
            base.PrepareUpdate(userId, unitId);

            this.ver += 1;
        }
    }
}
