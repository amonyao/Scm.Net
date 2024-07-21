using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.JT.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JT.OrderCreateB2C
{
    public class JtOrderCreateB2CRequest : JtRequest
    {
        /// <summary>
        /// 客户编码（联系出货网点提供）
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 签名，Base64(Md5(客户编号+密文+privateKey))，其中密文：MD5(明文密码+jadada236t2) 后大写
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 合作网点编码（没有则不传）
        /// </summary>
        public string network { get; set; }
        /// <summary>
        /// 客户订单号（传客户自己系统的订单号）
        /// </summary>
        public string txlogisticId { get; set; }
        /// <summary>
        /// 快件类型：EZ(标准快递)
        /// </summary>
        public string expressType { get; set; }
        /// <summary>
        /// 订单类型（有客户编号为月结）1、 散客；2、月结；
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 服务类型 ：02 门店寄件 ； 01 上门取件
        /// </summary>
        public string serviceType { get; set; }
        /// <summary>
        /// 派送类型： 06 代收点自提 05 快递柜自提 04 站点自提 03 派送上门
        /// </summary>
        public string deliveryType { get; set; }
        /// <summary>
        /// 支付方式：PP_PM("寄付月结"), CC_CASH("到付现结");
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 寄件信息对象
        /// </summary>
        public JtContact sender { get; set; }
        /// <summary>
        /// 收件信息对象
        /// </summary>
        public JtContact receiver { get; set; }
        /// <summary>
        /// 物流公司上门取货开始时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string sendStartTime { get; set; }
        /// <summary>
        /// 客户物流公司上门取货结束时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string sendEndTime { get; set; }
        /// <summary>
        /// 物品类型（对应订单主表物品类型）:
        /// bm000001 文件
        /// bm000002 数码产品
        /// bm000003 生活用品
        /// bm000004 食品
        /// bm000005 服饰
        /// bm000006 其他
        /// bm000007 生鲜类
        /// bm000008 易碎品
        /// bm000009 液体
        /// </summary>
        public string goodsType { get; set; }
        /// <summary>
        /// 是否实名
        /// </summary>
        public bool isRealName { get; set; }
        /// <summary>
        /// 长，cm编号
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 宽，cm
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高，cm
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 重量，单位kg，范围0.01-30
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 包裹总票数(必须为1)
        /// </summary>
        public int totalQuantity { get; set; }
        /// <summary>
        /// 代收货款金额 (数值型)
        /// </summary>
        public string itemsValue { get; set; }
        /// <summary>
        /// 代收货款币别（默认本国币别，如：RMB）
        /// </summary>
        public string priceCurrency { get; set; }
        /// <summary>
        /// 保价金额(数值型)，单位：元
        /// </summary>
        public string offerFee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 商品信息列表
        /// </summary>
        public List<JtCargoItem> items { get; set; }
        /// <summary>
        /// 客户信息
        /// </summary>
        public JtCustomsInfo customsInfo { get; set; }
        /// <summary>
        /// 驿站编码
        /// </summary>
        public string postSiteCode { get; set; }
        /// <summary>
        /// 驿站名称
        /// </summary>
        public string postSiteName { get; set; }
        /// <summary>
        /// 驿站地址
        /// </summary>
        public string postSiteAddress { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/order/addOrder";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/order/addOrder?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }
    }

    public class JtCustomsInfo
    {
        /// <summary>
        /// 货物数量 跨境件报关需要填写
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 货物单位，如：个、台、本， 跨境件报关需要填写
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 原产地国别， 跨境件报关需要填写
        /// </summary>
        public string sourceArea { get; set; }
        /// <summary>
        /// 货物产品国检备案编号 跨境件报关需要填写
        /// </summary>
        public string productRecordNo { get; set; }
        /// <summary>
        /// 商品海关备案号 跨境件报关需要填写
        /// </summary>
        public string goodPrepardNo { get; set; }
        /// <summary>
        /// 商品行邮税号 跨境件报关需要填写
        /// </summary>
        public string taxNo { get; set; }
        /// <summary>
        /// 海关编码 跨境件报关需要填写
        /// </summary>
        public string hsCode { get; set; }
        /// <summary>
        /// 商品编号 跨境件报关需要填写
        /// </summary>
        public string goodsCode { get; set; }
        /// <summary>
        /// 货物品牌 跨境件报关需要填写
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 货物规格型号 跨境件报关需要填写
        /// </summary>
        public string specifications { get; set; }
        /// <summary>
        /// 生产厂家 跨境件报关需要填写
        /// </summary>
        public string manufacturer { get; set; }
        /// <summary>
        /// 托寄物声明价值 跨境件报关需要填写
        /// </summary>
        public string cargoDeclaredValue { get; set; }
        /// <summary>
        /// 托寄物声明价值币别 跨境件报关需要填写
        /// </summary>
        public string declaredValueDeclaredCurrency { get; set; }
        /// <summary>
        /// 客户支付的运费 跨境件报关需要填写
        /// </summary>
        public string customerFreight { get; set; }
        /// <summary>
        /// 申报价值(数值型)
        /// </summary>
        public string itemValue { get; set; }
        /// <summary>
        /// 申报货款币别（默认本国币别，如：RMB）
        /// </summary>
        public string priceCurrency { get; set; }
        /// <summary>
        /// 物品描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 商品URL
        /// </summary>
        public string itemUrl { get; set; }
    }
}
