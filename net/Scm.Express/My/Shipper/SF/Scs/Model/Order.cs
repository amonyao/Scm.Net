using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class Order
    {
        /// <summary>
        /// 订单来源
        /// </summary>
        public string sourceCode { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// ERP单号（客户系统订单号）
        /// </summary>
        public string erpNo { get; set; }
        /// <summary>
        /// 月结卡号
        /// </summary>
        public string monthlyAccount { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 产品类型代码
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 付款方式代码
        /// </summary>
        public string paymentTypeCode { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 收货确认时间
        /// </summary>
        public string confirmTime { get; set; }
        /// <summary>
        /// 签收时间
        /// </summary>
        public string signTime { get; set; }
        /// <summary>
        /// 温层名称
        /// </summary>
        public string temperatureLevelName { get; set; }
        /// <summary>
        /// 温层代码
        /// </summary>
        public string temperatureLevelCode { get; set; }
        /// <summary>
        /// 实际重量
        /// </summary>
        public string grossWeight { get; set; }
        /// <summary>
        /// 计费重量
        /// </summary>
        public string meterageWeight { get; set; }
        /// <summary>
        /// 主运单号
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// 运单数量（子母件数量）
        /// </summary>
        public int waybillCount { get; set; }
        /// <summary>
        /// 签回单号
        /// </summary>
        public string returnWaybillNo { get; set; }
        /// <summary>
        /// 提货方式
        /// </summary>
        public string pickupType { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string distributionType { get; set; }
        /// <summary>
        /// 发货公司
        /// </summary>
        public string senderCompany { get; set; }
        /// <summary>
        /// 发货人姓名
        /// </summary>
        public string senderName { get; set; }
        /// <summary>
        /// 发货人手机号
        /// </summary>
        public string senderMobile { get; set; }
        /// <summary>
        /// 发货人座机号
        /// </summary>
        public string senderPhone { get; set; }
        /// <summary>
        /// 发货国家名称
        /// </summary>
        public string senderCountryName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-省名称
        /// </summary>
        public string senderProvinceName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-市名称
        /// </summary>
        public string senderCityName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-区名称
        /// </summary>
        public string senderAreaName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-详细地址
        /// </summary>
        public string senderDetailAddress { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-网点名称
        /// </summary>
        public string senderWebsiteName { get; set; }
        /// <summary>
        /// 收货公司
        /// </summary>
        public string receiverCompany { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receiverName { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string receiverMobile { get; set; }
        /// <summary>
        /// 收货国家名称
        /// </summary>
        public string receiverCountryName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-省份名称
        /// </summary>
        public string receiverProvinceName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-城市名称
        /// </summary>
        public string receiverCityName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-区名称
        /// </summary>
        public string receiverAreaName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-详细地址
        /// </summary>
        public string receiverDetailAddress { get; set; }
        /// <summary>
        /// 收货网点名称
        /// </summary>
        public string receiverWebsiteName { get; set; }
        /// <summary>
        /// 派件员姓名
        /// </summary>
        public string delivererName { get; set; }
        /// <summary>
        /// 派件员手机号
        /// </summary>
        public string delivererMobile { get; set; }
        /// <summary>
        /// 司机姓名
        /// </summary>
        public string driverName { get; set; }
        /// <summary>
        /// 司机手机号
        /// </summary>
        public string driverMobile { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string carNo { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string carType { get; set; }

        public List<string> childWaybillList { get; set; }
    }
}
