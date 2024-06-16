using Com.Scm.Dto;
using Com.Scm.Enums;
using Com.Scm.Pos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 商品
    /// </summary>
    public class PosSpuDto : ScmDataDto
    {
        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 商品类型（实物、虚拟）
        /// </summary>
        public SpuTypesEnums types { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [StringLength(64)]
        public string barcode { get; set; }

        /// <summary>
        /// 商品简称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

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
        /// 商品特色
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
        /// 展示状态（显示、隐藏）
        /// </summary>
        public SpuShowEnums show_status { get; set; }

        /// <summary>
        /// 销售状态（可售、禁售）
        /// </summary>
        public SpuSaleEnums sale_status { get; set; }

        /// <summary>
        /// 售后状态（退款、退货、换货）
        /// </summary>
        public SpuAssEnums ass { get; set; }

        /// <summary>
        /// 热销商品
        /// </summary>
        public ScmBoolEnum hot { get; set; }

        /// <summary>
        /// 会员商品
        /// </summary>
        public ScmBoolEnum vip { get; set; }

        /// <summary>
        /// 推荐商品
        /// </summary>
        public ScmBoolEnum sss { get; set; }

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