using Com.Scm.Express.OrderSearch;

namespace Com.Scm.Express.My.Shipper.DEPPON.OrderSearch
{
    public class DbOrderSearchResponse : DbResponse
    {
        public DbOrderSearchResult responseParam { get; set; }

        public void ToResponse(OrderSearchResponse response)
        {
            if (responseParam != null)
            {
                var result = new OrderSearchResult();
                result.code = responseParam.logisticID;
                result.mail_no = responseParam.mailNo;
                response.Append(result);
            }
        }
    }

    public class DbOrderSearchResult
    {
        /// <summary>
        /// 物流公司ID
        /// </summary>
        public string logisticCompanyID { get; set; }

        /// <summary>
        /// 渠道单号
        /// </summary>
        public string logisticID { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailNo { get; set; }
        /// <summary>
        /// 出发部门编码
        /// </summary>
        public string businessNetworkNo { get; set; }
        /// <summary>
        /// 到达部门编码
        /// </summary>
        public string toNetworkNo { get; set; }
        /// <summary>
        /// 发货人信息
        /// </summary>
        public DbContact sender { get; set; }
        /// <summary>
        /// receiver
        /// </summary>
        public DbContact receiver { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string cargoName { get; set; }
        /// <summary>
        /// 总件数
        /// </summary>
        public int totalNumber { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public double totalWeight { get; set; }
        /// <summary>
        /// 总体积
        /// </summary>
        public double totalVolume { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 保价金额
        /// </summary>
        public double insuranceValue { get; set; }
        /// <summary>
        /// 代收货款类型
        /// </summary>
        public string codType { get; set; }
        /// <summary>
        /// 代收货款
        /// </summary>
        public double codValue { get; set; }
        /// <summary>
        /// 代收货款费
        /// </summary>
        public double codPrice { get; set; }
        /// <summary>
        /// 上门接货
        /// </summary>
        public string vistReceive { get; set; }
        /// <summary>
        /// 送货方式
        /// </summary>
        public string deliveryType { get; set; }
        /// <summary>
        /// 签收回单
        /// </summary>
        public string backSignBill { get; set; }
        /// <summary>
        /// 包装
        /// </summary>
        public string packageService { get; set; }
        /// <summary>
        /// 短信通知
        /// </summary>
        public string smsNotify { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 运输费用
        /// </summary>
        public string transportPrice { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public string totalPrice { get; set; }
        /// <summary>
        /// 上门接货费
        /// </summary>
        public string vistReceivePrice { get; set; }
        /// <summary>
        /// 送货费用
        /// </summary>
        public string deliveryPrice { get; set; }
        /// <summary>
        /// 签收回单费
        /// </summary>
        public string backSignBillPrice { get; set; }
        /// <summary>
        /// 包装服务费
        /// </summary>
        public string packageServicePrice { get; set; }
        /// <summary>
        /// 短信通知费用
        /// </summary>
        public string smsNotifyPrice { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public string otherPrice { get; set; }
        /// <summary>
        /// 燃油附加
        /// </summary>
        public string fuelSurcharge { get; set; }
        /// <summary>
        /// 燃油附加费
        /// </summary>
        public string fuelSurchargePrice { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string statusType { get; set; }
        /// <summary>
        /// 等通知发货费用
        /// </summary>
        public string waitNotifySendPrice { get; set; }
        /// <summary>
        /// 是否等通知发货
        /// </summary>
        public string waitNotifySend { get; set; }
        /// <summary>
        /// 反馈原因
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public string memberType { get; set; }
        /// <summary>
        /// 保价费
        /// </summary>
        public double insurancePrice { get; set; }
    }
}
