using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.ZTO.OrderSearch
{
    public class ZtoOrderSearchResponse : ZtoResponse<List<PreOrderInfoDto>>
    {
    }

    public class PreOrderInfoDto
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 发件市
        /// </summary>
        public string sendCity { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string receivName { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string orderRemark { get; set; }
        /// <summary>
        /// 订单状态（0：下单成功； 1：分配网点； 2：分配业务员； 3：业务员上门取件； -2订单取消； 99：订单完成）
        /// </summary>
        public int orderStatus { get; set; }
        /// <summary>
        /// 分配网点
        /// </summary>
        public string assignSite { get; set; }
        /// <summary>
        /// 发件省
        /// </summary>
        public string sendProv { get; set; }
        /// <summary>
        /// 收件人电话号
        /// </summary>
        public string receivPhone { get; set; }
        /// <summary>
        /// 收件人公司名
        /// </summary>
        public string receivCompany { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string orderCreateDate { get; set; }
        /// <summary>
        /// 收件人邮编
        /// </summary>
        public string receivZipCode { get; set; }
        /// <summary>
        /// 分配业务员
        /// </summary>
        public string assignEmp { get; set; }
        /// <summary>
        /// 收件人详细地址
        /// </summary>
        public string receivAddress { get; set; }
        /// <summary>
        /// 包裹包装费(单位:分)
        /// </summary>
        public string parcelPackingFee { get; set; }
        /// <summary>
        /// 发件人手机号码
        /// </summary>
        public string sendMobile { get; set; }
        /// <summary>
        /// 分配业务员Code
        /// </summary>
        public string assignEmpCode { get; set; }
        /// <summary>
        /// 包裹总重量 （克）
        /// </summary>
        public string parcelWeight { get; set; }
        /// <summary>
        /// 包裹内货物总数量
        /// </summary>
        public string parcelQuantity { get; set; }
        /// <summary>
        /// 包裹其他费用(单位:分)
        /// </summary>
        public string parcelOtherFee { get; set; }
        /// <summary>
        /// 收件人手机号码
        /// </summary>
        public string receivMobile { get; set; }
        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string sendName { get; set; }
        /// <summary>
        /// 收件人省
        /// </summary>
        public string receivProv { get; set; }
        /// <summary>
        /// 收件人市
        /// </summary>
        public string receivCity { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string partnerName { get; set; }
        /// <summary>
        /// 分配网点code 
        /// </summary>
        public string assignSiteCode { get; set; }
        /// <summary>
        /// 包裹运输费(单位:分) 
        /// </summary>
        public string parcelFreight { get; set; }
        /// <summary>
        /// 发件区 
        /// </summary>
        public string sendCounty { get; set; }
        /// <summary>
        /// 发件人公司名 
        /// </summary>
        public string sendCompany { get; set; }
        /// <summary>
        /// 到达收取币种 
        /// </summary>
        public string vasCollectCurrency { get; set; }
        /// <summary>
        /// 运单号 
        /// </summary>
        public string billCode { get; set; }
        /// <summary>
        /// 预约取件结束时间 
        /// </summary>
        public string parcelTakingEndTime { get; set; }
        /// <summary>
        /// 包裹中商品总价值(分) 
        /// </summary>
        public string parcelPrice { get; set; }
        /// <summary>
        /// 发件人详细地址 
        /// </summary>
        public string sendAddress { get; set; }
        /// <summary>
        /// 到达收取金额 
        /// </summary>
        public string vasCollectSum { get; set; }
        /// <summary>
        /// 收件人区 
        /// </summary>
        public string receivCounty { get; set; }
        /// <summary>
        /// 预约取件开始时间 
        /// </summary>
        public string parcelTakingStartTime { get; set; }
        /// <summary>
        /// 发件人电话号 
        /// </summary>
        public string sendPhone { get; set; }
        /// <summary>
        /// 订单总金额(单位:分) 
        /// </summary>
        public string parcelOrderSum { get; set; }
        /// <summary>
        /// 订单号 
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// Y渠道Id 
        /// </summary>
        public string partnerId { get; set; }
        /// <summary>
        /// 订单包裹大小（厘米）, 用半角的逗号来分隔长宽高 
        /// </summary>
        public string parcelSize { get; set; }
        /// <summary>
        /// 发件人邮编 
        /// </summary>
        public string sendZipCode { get; set; }
        /// <summary>
        /// 交易号 
        /// </summary>
        public string tradeId { get; set; }
        /// <summary>
        /// 取消类型 
        /// </summary>
        public string secStatus { get; set; }
        /// <summary>
        /// 取消原因 
        /// </summary>
        public string cancelReason { get; set; }
        /// <summary>
        /// 分配业务员手机号 
        /// </summary>
        public string assignEmpMobile { get; set; }
    }
}
