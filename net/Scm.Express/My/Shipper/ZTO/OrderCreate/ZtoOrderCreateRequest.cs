using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.ZTO.OrderCreate
{
    public class ZtoOrderCreateRequest : ZtoRequest
    {
        /// <summary>
        /// 合作模式 ，1：集团客户；2：非集团客户	
        /// </summary>
        public string partnerType { get; set; }
        /// <summary>
        /// partnerType为1时，orderType：1：全网件 2：预约件。 partnerType为2时，orderType：1：全网件 2：预约件（返回运单号） 3：预约件（不返回运单号） 4：星联全网件	
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 合作商订单号	
        /// </summary>
        public string partnerOrderCode { get; set; }
        /// <summary>
        /// 账号信息	
        /// </summary>
        public AccountDto accountInfo { get; set; }
        /// <summary>
        /// 运单号	
        /// </summary>
        public string billCode { get; set; }
        /// <summary>
        /// 发件人信息	
        /// </summary>
        public SenderInfoInput senderInfo { get; set; }
        /// <summary>
        /// 收件人信息	
        /// </summary>
        public ReceiveInfoInput receiveInfo { get; set; }
        /// <summary>
        /// 增值服务信息	
        /// </summary>
        public List<OrderVasDto> orderVasList { get; set; }
        /// <summary>
        /// 门店/仓库编码（partnerType为1时可使用）	
        /// </summary>
        public string hallCode { get; set; }
        /// <summary>
        /// 汇总信息	
        /// </summary>
        public SummaryDto summaryInfo { get; set; }
        /// <summary>
        /// 网点code（orderVasList.vasType为receiveReturnService必填）	
        /// </summary>
        public string siteCode { get; set; }
        /// <summary>
        /// 网点名称（orderVasList.vasType为receiveReturnService必填）	
        /// </summary>
        public string siteName { get; set; }
        /// <summary>
        /// 备注	
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 物品信息	
        /// </summary>
        public List<OrderItem> orderItems { get; set; }
        /// <summary>
        /// 机柜信息	
        /// </summary>
        public CabinetInfo cabinet { get; set; }

        public override string GetServiceUrl()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://japi.zto.com/zto.open.createOrder";
            }
            return "https://japi-test.zto.com/zto.open.createOrder";
        }
    }

    public class AccountDto
    {
        /// <summary>
        /// 电子面单账号（partnerType为2，orderType传1,2,4时必传）	
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 客户编码（partnerType传1时必传）	
        /// </summary>
        public string customerId { get; set; }
        /// <summary>
        /// 单号类型:1.普通电子面单；74.星联电子面单；默认是1	
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 电子面单密码（测试环境传ZTO123）	
        /// </summary>
        public string accountPassword { get; set; }
    }

    public class SenderInfoInput
    {
        /// <summary>
        /// 发件人座机（与senderMobile二者不能同时为空）	
        /// </summary>
        public string senderPhone { get; set; }
        /// <summary>
        /// 发件人姓名	
        /// </summary>
        public string senderName { get; set; }
        /// <summary>
        /// 发件人详细地址	
        /// </summary>
        public string senderAddress { get; set; }
        /// <summary>
        /// 发件人区	
        /// </summary>
        public string senderDistrict { get; set; }
        /// <summary>
        /// 发件人手机号（与senderPhone二者不能同时为空）	
        /// </summary>
        public string senderMobile { get; set; }
        /// <summary>
        /// 发件人省	
        /// </summary>
        public string senderProvince { get; set; }
        /// <summary>
        /// 发件人市	
        /// </summary>
        public string senderCity { get; set; }
    }

    public class ReceiveInfoInput
    {
        /// <summary>
        /// 收件人市	
        /// </summary>
        public string receiverCity { get; set; }
        /// <summary>
        /// 收件人详细地址	
        /// </summary>
        public string receiverAddress { get; set; }
        /// <summary>
        /// 收件人座机（与receiverMobile二者不能同时为空）	
        /// </summary>
        public string receiverPhone { get; set; }
        /// <summary>
        /// 收件人姓名	
        /// </summary>
        public string receiverName { get; set; }
        /// <summary>
        /// 收件人区	
        /// </summary>
        public string receiverDistrict { get; set; }
        /// <summary>
        /// 收件人手机号（与 receiverPhone二者不能同时为空）	
        /// </summary>
        public string receiverMobile { get; set; }
        /// <summary>
        /// 收件人省	
        /// </summary>
        public string receiverProvince { get; set; }
    }

    public class OrderVasDto
    {
        /// <summary>
        /// 代收账号（有代收货款增值时必填）	
        /// </summary>
        public string accountNo { get; set; }
        /// <summary>
        /// 增值类型 （COD：代收； vip：尊享； insured：保价； receiveReturnService：签单返回； twoHour：两小时；standardExpress：标快）	
        /// </summary>
        public string vasType { get; set; }
        /// <summary>
        /// 增值价格，如果增值类型涉及金额会校验，vasType为COD、insured时不能为空，单位：分	
        /// </summary>
        public string vasAmount { get; set; }
        /// <summary>
        /// 增值价格（暂时不用）	
        /// </summary>
        public string vasPrice { get; set; }
        /// <summary>
        /// 增值详情	
        /// </summary>
        public string vasDetail { get; set; }
    }

    public class SummaryDto
    {
        /// <summary>
        /// 到达收取币种	
        /// </summary>
        public string collectMoneyType { get; set; }
        /// <summary>
        /// 订单包裹内货物总数量	
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 险费（单位：元）	
        /// </summary>
        public string premium { get; set; }
        /// <summary>
        /// 订单包裹大小（单位：厘米、格式：”长，宽，高”，用半角的逗号来分隔）	
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 商品总价值（单位：元）	
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 其他费用(单位:元)	
        /// </summary>
        public string otherCharges { get; set; }
        /// <summary>
        /// 运输费（单位：元）	
        /// </summary>
        public string freight { get; set; }
        /// <summary>
        /// 包装费(单位:元)	
        /// </summary>
        public string packCharges { get; set; }
        /// <summary>
        /// 取件开始时间	
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 取件截止时间	
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 保险费	
        /// </summary>
        public string orderSum { get; set; }
    }

    public class OrderItem
    {
        /// <summary>
        /// 货品名称	
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品分类	
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 商品材质	
        /// </summary>
        public string material { get; set; }
        /// <summary>
        /// 大小（长,宽,高）(单位：厘米), 用半角的逗号来分隔长宽高	
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 重量（单位：克)	
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 单价(单位:元)	
        /// </summary>
        public string unitprice { get; set; }
        /// <summary>
        /// 货品数量	
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 货品备注	
        /// </summary>
        public string remark { get; set; }
    }

    public class CabinetInfo
    {
        /// <summary>
        /// 地址	
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 格口规格 格口大小( 1 大 2 中 3 小）	
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 开箱码	
        /// </summary>
        public string code { get; set; }
    }
}
