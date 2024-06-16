using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 规格
    /// </summary>
    public class PosSkuDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 规格编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [StringLength(64)]
        public string barcode { get; set; }

        /// <summary>
        /// 规格简称
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 规格摘要
        /// </summary>
        [StringLength(32)]
        public string hash { get; set; }

        /// <summary>
        /// 规格内容
        /// </summary>
        [StringLength(1024)]
        public string spec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        [StringLength(256)]
        public string notice { get; set; }

        /// <summary>
        /// 规格特色
        /// </summary>
        [StringLength(256)]
        public string feature { get; set; }

        /// <summary>
        /// 详细介绍
        /// </summary>
        [StringLength(1024)]
        public string desc { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public int weight { get; set; }
    }
}