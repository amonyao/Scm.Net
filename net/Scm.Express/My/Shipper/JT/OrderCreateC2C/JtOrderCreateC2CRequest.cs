using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.JT.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JT.OrderCreateC2C
{
    public class JtOrderCreateC2CRequest : JtRequest
    {
        ///<summary>
        /// 业务员编码（联系出货网点提供，且采用网点编码签名）
        ///</summary>
        public string salesmanCode { get; set; }
        ///<summary>
        /// 签名，Base64(Md5(业务员编码+密文+privateKey))，其中密文：(网点编码+jadada236t2) 大写
        ///</summary>
        public string digest { get; set; }
        ///<summary>
        /// 运单编号
        ///</summary>
        public string billCode { get; set; }
        ///<summary>
        /// 客户订单号（传客户自己系统的订单号）
        ///</summary>
        public string txlogisticId { get; set; }
        ///<summary>
        /// 快件类型：EZ(标准快递)
        ///</summary>
        public string expressType { get; set; }
        ///<summary>
        /// 订单类型 1、散客；
        ///</summary>
        public string orderType { get; set; }
        ///<summary>
        /// 服务类型 02 门店寄件 01 上门取件
        ///</summary>
        public string serviceType { get; set; }
        ///<summary>
        /// 派送类型 06 代收点自提 05 快递柜自提 04 站点自提 03 派送上门
        ///</summary>
        public string deliveryType { get; set; }
        ///<summary>
        /// 支付方式CC_CASH(“到付现结”);PP_CASH（“寄付现结”）
        ///</summary>
        public string payType { get; set; }
        ///<summary>
        /// 寄件信息对象
        ///</summary>
        public JtContact sender { get; set; }
        ///<summary>
        /// 收件信息对象
        ///</summary>
        public JtContact receiver { get; set; }
        ///<summary>
        /// 物流公司上门取货开始时间 yyyy-MM-dd HH:mm:ss
        ///</summary>
        public string sendStartTime { get; set; }
        ///<summary>
        /// 物流公司上门取货结束时间 yyyy-MM-dd HH:mm:ss
        ///</summary>
        public string sendEndTime { get; set; }
        ///<summary>
        /// 物品类型（对应订单主表物品类型）:
        ///</summary>
        public string goodsType { get; set; }
        ///<summary>
        /// 长，cm
        ///</summary>
        public int length { get; set; }
        ///<summary>
        /// 宽，cm
        ///</summary>
        public int width { get; set; }
        ///<summary>
        /// 高，cm
        ///</summary>
        public int height { get; set; }
        ///<summary>
        /// 重量，单位kg，范围0.01-30
        ///</summary>
        public string weight { get; set; }
        ///<summary>
        /// 包裹总票数(必须为1)
        ///</summary>
        public int totalQuantity { get; set; }
        ///<summary>
        /// 代收货款金额 (数值型)
        ///</summary>
        public string itemsValue { get; set; }
        ///<summary>
        /// 代收货款币别（默认本国币别，如：RMB
        ///</summary>
        public string priceCurrency { get; set; }
        ///<summary>
        /// 保价金额(数值型)，单位：元
        ///</summary>
        public string offerFee { get; set; }
        ///<summary>
        /// 备注
        ///</summary>
        public string remark { get; set; }
        ///<summary>
        /// 商品信息列表
        ///</summary>
        public List<JtCargoItem> items { get; set; }
        ///<summary>
        /// 驿站编码
        ///</summary>
        public string postSiteCode { get; set; }
        ///<summary>
        /// 驿站名称
        ///</summary>
        public string postSiteName { get; set; }
        ///<summary>
        /// 驿站地址
        ///</summary>
        public string postSiteAddress { get; set; }
        ///<summary>
        /// 是否实名
        ///</summary>
        public bool? isRealName { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/order/addLooseOrder";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/order/addLooseOrder?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }
    }
}
