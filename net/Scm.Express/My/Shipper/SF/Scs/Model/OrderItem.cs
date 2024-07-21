using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class OrderItem
    {
        /// <summary>
        /// 商品类别，参照附录4.5或自定义类别
        /// </summary>
        public string skuCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuName { get; set; }
        /// <summary>
        /// 长(cm)
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 宽(cm)
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 高(cm)
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 件数，数量
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 毛重(kg)
        /// </summary>
        public string grossWeight { get; set; }
        /// <summary>
        /// 净重(kg)
        /// </summary>
        public string netHeight { get; set; }
        /// <summary>
        /// 体积(m3) 立方米 可以只填体积,不填长宽高
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 是否进口(1-不进口，2-进口)
        /// </summary>
        public string importedFlag { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string carrierName { get; set; }
        /// <summary>
        /// 商品编码SKU
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string skuNumber { get; set; }
        /// <summary>
        /// 数量计量单位，类似kg
        /// </summary>
        public string skuUnit { get; set; }
        /// <summary>
        /// 商品单价(元)
        /// </summary>
        public string price { get; set; }
    }
}
