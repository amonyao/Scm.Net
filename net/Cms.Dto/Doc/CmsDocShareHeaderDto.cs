using Com.Scm.Dto;
using Com.Scm.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    public class CmsDocShareHeaderDto : ScmDataDto
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

        /// <summary>
        /// 
        /// </summary>
        public List<CmsDocShareDetailDto> details { get; set; }
    }
}
