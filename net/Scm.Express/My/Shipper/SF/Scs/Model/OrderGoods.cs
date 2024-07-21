using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class OrderGoods
    {
        /// <summary>
        /// 商品类别
        /// </summary>
        public string skuCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuName { get; set; }
        /// <summary>
        /// 箱数,件
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 实重(kg)
        /// </summary>
        public string grossWeight { get; set; }
        /// <summary>
        /// 体积(m3)
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 商品编码SKU
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string skuNumber { get; set; }
        /// <summary>
        /// 计量单位，类似个、只
        /// </summary>
        public string skuUnit { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
