using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.STO.OrderCreate
{
    public class StoOrderCreateRequest : StoRequest
    {
        /// <summary>
        /// 订单号（客户系统自己生成，唯一）	
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 订单来源（订阅服务时填写的来源编码）	
        /// </summary>
        public string orderSource { get; set; }
        /// <summary>
        /// 获取面单的类型（00-普通、03-国际、01-代收、02-到付、04-生鲜），默认普通业务，如果有其他业务先与业务方沟通清楚	
        /// </summary>
        public string billType { get; set; }
        /// <summary>
        /// 订单类型（01-普通订单、02-调度订单）默认01-普通订单，如果有散单业务需先业务方沟通清楚	
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 寄件人信息	
        /// </summary>
        public SenderDO sender { get; set; }
        /// <summary>
        /// 收件人信息	
        /// </summary>
        public ReceiverDO receiver { get; set; }
        /// <summary>
        /// 包裹信息	
        /// </summary>
        public CargoDO cargo { get; set; }
        /// <summary>
        /// 客户信息，在线下单取运单号必填，代单号下单不需要填写，测试账号传值如下，生产账号联系合作业务方提供	
        /// </summary>
        public CustomerDO customer { get; set; }
        /// <summary>
        /// 国际订单附属信息（国际业务订单必填，其他业务不要填写）	
        /// </summary>
        public InternationalAnnexDO internationalAnnex { get; set; }
        /// <summary>
        /// 运单号（下单前已获取运单号时必传，否则不传或传NULL）	
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// 指定网点揽收（调度散单业务订单需要传）其他业务不需要	
        /// </summary>
        public AssignAnnex assignAnnex { get; set; }
        /// <summary>
        /// 代收货款金额，单位：元（代收货款业务时必填）	
        /// </summary>
        public string codValue { get; set; }
        /// <summary>
        /// 到付运费金额，单位：元（到付业务时必填）	
        /// </summary>
        public string freightCollectValue { get; set; }
        /// <summary>
        /// 时效类型（01-普通）	
        /// </summary>
        public string timelessType { get; set; }
        /// <summary>
        /// 产品类型 （01-普通、02-冷链、03-生鲜）	
        /// </summary>
        public string productType { get; set; }
        /// <summary>
        /// 增值服务（DELIVER_CONTACT-派前电联,TRACE_PUSH-轨迹回传;PRIVACY_SURFACE_SINGLE-隐私面单标）
        /// <summary>
        public List<string> serviceTypeList { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public Dictionary<string, string> extendFieldMap { get; set; }

        /// <summary>
        /// 备注		
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 快递流向（01-正向订单)默认01		
        /// </summary>
        public string expressDirection { get; set; }
        /// <summary>
        /// 创建原因（01-客户创建）默认01		
        /// </summary>
        public string createChannel { get; set; }
        /// <summary>
        /// 区域类型（01-国内）默认01		
        /// </summary>
        public string regionType { get; set; }
        /// <summary>
        /// 保价模型（保价服务必填）		
        /// </summary>
        public InsuredAnnexDo insuredAnnex { get; set; }
        /// <summary>
        /// 预估费用（散单业务使用）		
        /// </summary>
        public string expectValue { get; set; }
        /// <summary>
        /// 支付方式（1-现付；2-到付；3-月结）		
        /// </summary>
        public string payModel { get; set; }

        public override string GetApiName()
        {
            return "OMS_EXPRESS_ORDER_CREATE";
        }

        public override string GetToAppKey()
        {
            return "sto_oms";
        }

        public override string GetToCode()
        {
            return "sto_oms";
        }
    }

    public class SenderDO
    {
        /// <summary>
        /// 寄件人名称	
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 寄件人固定电话	
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 寄件人手机号码	
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 邮编	
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 国家	
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 省	
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市	
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区	
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 镇	
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 详细地址	
        /// </summary>
        public string address { get; set; }
    }

    public class ReceiverDO
    {
        /// <summary>
        /// 收件人名称	
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 收件人固定电话	
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 收件人手机号码	
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 邮编	
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 国家	
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 省	
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市	
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区	
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 镇	
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 详细地址	
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 安全号码	
        /// </summary>
        public string safeNo { get; set; }
    }

    public class CargoDO
    {
        /// <summary>
        /// 带电标识 （10/未知 20/带电 30/不带电）	
        /// </summary>
        public string battery { get; set; }
        /// <summary>
        /// 物品类型（大件、小件、扁平件\文件）	
        /// </summary>
        public string goodsType { get; set; }
        /// <summary>
        /// 物品名称	
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 物品数量	
        /// </summary>
        public string goodsCount { get; set; }
        /// <summary>
        /// 长（cm）	
        /// </summary>
        public string spaceX { get; set; }
        /// <summary>
        /// 宽(cm)	
        /// </summary>
        public string spaceY { get; set; }
        /// <summary>
        /// 高(cm)	
        /// </summary>
        public string spaceZ { get; set; }
        /// <summary>
        /// 重量(kg)	
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 商品金额	
        /// </summary>
        public string goodsAmount { get; set; }
        /// <summary>
        /// 小包信息（国际业务专用，其他业务不需要填写）	
        /// </summary>
        public List<CargoItemDO> cargoItemList { get; set; }
    }

    public class CargoItemDO
    {
        /// <summary>
        /// 小包号	
        /// </summary>
        public string serialNumber { get; set; }
        /// <summary>
        /// 关联单号	
        /// </summary>
        public string referenceNumber { get; set; }
        /// <summary>
        /// 商品ID	
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 名称	
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 数量	
        /// </summary>
        public string qty { get; set; }
        /// <summary>
        /// 单价	
        /// </summary>
        public string unitPrice { get; set; }
        /// <summary>
        /// 总价	
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 币种	
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 重量(kg)	
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 备注	
        /// </summary>
        public string remark { get; set; }
    }

    public class CustomerDO
    {
        /// <summary>
        /// 网点编码必传，测试传值"siteCode":"666666",	
        /// </summary>
        public string siteCode { get; set; }
        /// <summary>
        /// 客户编码，测试传值"customerName":"666666000001"	
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// 电子面单密码，测试传值"sitePwd":"abc123"	
        /// </summary>
        public string sitePwd { get; set; }
        /// <summary>
        /// 月结客户编码（不传单号需调度才传月结编号）如果填写一般和客户编号值相同	
        /// </summary>
        public string monthCustomerCode { get; set; }
    }

    public class InternationalAnnexDO
    {
        /// <summary>
        /// 国际业务类型（01-国际进口，02-国际保税，03-国际直邮)	
        /// </summary>
        public string internationalProductType { get; set; }
        /// <summary>
        /// 是否报关，默认为否	
        /// </summary>
        public string customsDeclaration { get; set; }
        /// <summary>
        /// 发件人所在国家，国际件为必填字段	
        /// </summary>
        public string senderCountry { get; set; }
        /// <summary>
        /// 收件人所在国家，国际件为必填字段	
        /// </summary>
        public string receiverCountry { get; set; }
    }

    public class AssignAnnex
    {
        /// <summary>
        /// 指定取件的网点编号	
        /// </summary>
        public string takeCompanyCode { get; set; }
        /// <summary>
        /// 指定取件的业务员编号（指定业务员时takeCompanyCode可传可不传，若传必须传正确，举例：寄件地址是上海，只能是指定上海业务员取件）	
        /// </summary>
        public string takeUserCode { get; set; }
    }

    public class InsuredAnnexDo
    {
        /// <summary>
        /// 保价金额，单位：元（保价服务时必填，精确到小数点后两位）		
        /// </summary>
        public string insuredValue { get; set; }
        /// <summary>
        /// 物品价值，单位：元（保价服务时必填，精确到小数点后两位）		
        /// </summary>
        public string goodsValue { get; set; }
    }
}
