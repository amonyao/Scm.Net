using Com.Scm.Dvo;

namespace Com.Scm.Fms.Doc.Dvo
{
    /// <summary>
    /// 图片数据
    /// </summary>
    public class FileDvo : ScmDataDvo
    {
        /// <summary>
        /// 类别
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int types { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int modes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 短链编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 文件摘要
        /// </summary>
        public string hash { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string exts { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long size { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        public int refs { get; set; } = 0;

        /// <summary>
        /// 标签数量
        /// </summary>
        public int tags { get; set; } = 0;

        /// <summary>
        /// 收藏数量
        /// </summary>
        public int favs { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 文件创建时间
        /// </summary>
        public long file_create_time { get; set; }

        /// <summary>
        /// 文件修改时间
        /// </summary>
        public long file_modify_time { get; set; }
    }
}