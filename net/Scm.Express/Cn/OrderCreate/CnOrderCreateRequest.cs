namespace Com.Scm.Express.CnExpress.OrderCreate
{
    /// <summary>
    /// 
    /// </summary>
    public class CnOrderCreateRequest : CnRequest
    {
        /// <summary>
        /// 运单类型：1-普通运单；2-生鲜；3-航空	
        /// </summary>
        public int waybillType { get; set; }

        /// <summary>
        /// 所需运单的数量，顺丰只能传1，非顺丰快递公司最多只能传99	
        /// </summary>
        public int waybillCount { get; set; }

        /// <summary>
        /// 承运商ID （与providerCode必填一个）	
        /// </summary>
        public int providerId { get; set; }

        /// <summary>
        /// 承运商编码（与providerId必填一个）	
        /// </summary>
        public string providerCode { get; set; }

        /// <summary>
        /// 承运商发货网点编码（网点编码不要超过32位），承运商发货网点编码 ，加盟型快递公司必传	
        /// </summary>
        public string branchCode { get; set; }

        /// <summary>
        /// 平台订单号，即pop订单号，如果多订单合并发货，每个订单号之间用“,”逗号分隔，每个订单号最多32位	
        /// </summary>
        public string platformOrderNo { get; set; }

        /// <summary>
        /// 商家编码，是用POP商家ID	
        /// </summary>
        public string vendorCode { get; set; }

        /// <summary>
        /// 商家名称	
        /// </summary>
        public string vendorName { get; set; }

        /// <summary>
        /// 商家自有订单号	
        /// </summary>
        public string vendorOrderCode { get; set; }

        /// <summary>
        /// 销售平台；0010001代表京东平台下的订单	
        /// </summary>
        public string salePlatform { get; set; }

        /// <summary>
        /// 是否子母单	
        /// </summary>
        public bool childMotherOrder { get; set; }

        /// <summary>
        /// 发货四级地址	
        /// </summary>
        public WaybillAddress fromAddress { get; set; }

        /// <summary>
        /// 收货四级地址	
        /// </summary>
        public WaybillAddress toAddress { get; set; }

        /// <summary>
        /// 重量，单位为kg，两位小数 （0-10000 ）不允许超出范围，没有传0	
        /// </summary>
        public double weight { get; set; }

        /// <summary>
        /// 体积，单位为统一为立方厘米,两位小数 体积超出范围（0-10000000）cm3，没有传0	
        /// </summary>
        public double volume { get; set; }

        /// <summary>
        /// 商品名称（不下传承运商）	
        /// </summary>
        public string goodsName { get; set; }

        /// <summary>
        /// 承诺时效类型；无时效默认传0	
        /// </summary>
        public int promiseTimeType { get; set; }

        /// <summary>
        /// 承诺完成时间，若未承诺时效，则不考虑此字段	
        /// </summary>
        public string promiseCompleteTime { get; set; }

        /// <summary>
        /// 商品金额，单位：元，两位小数	
        /// </summary>
        public double goodsMoney { get; set; }

        /// <summary>
        /// 付款方式0-在线支付，目前暂时不支持货到付款业务	
        /// </summary>
        public int payType { get; set; }

        /// <summary>
        /// 代收金额，单位：元，两位小数 代收货款金额超出范围（0-20000）,付款方式为在线支付时，代收金额必须为0	
        /// </summary>
        public double shouldPayMoney { get; set; }

        /// <summary>
        /// 是否要保价（系统暂不开放报价业务）	
        /// </summary>
        public bool needGuarantee { get; set; }

        /// <summary>
        /// 保价金额，单位：元，两位小数，needGuarantee为false时，保价金额必须为0	
        /// </summary>
        public double guaranteeMoney { get; set; }

        /// <summary>
        /// 收货时间类型 0-任何时间，1-工作日，2-节假日	
        /// </summary>
        public int receiveTimeType { get; set; }

        /// <summary>
        /// 发货仓（京配使用）	
        /// </summary>
        public string warehouseCode { get; set; }

        /// <summary>
        /// 二段码（如：京配为京配路区）	
        /// </summary>
        public string secondSectionCode { get; set; }

        /// <summary>
        /// 三段码（如：京配为京配站点ID）	
        /// </summary>
        public string thirdSectionCode { get; set; }

        /// <summary>
        /// 备注	
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 扩展字段1	
        /// </summary>
        public string extendField1 { get; set; }

        /// <summary>
        /// 扩展字段2	
        /// </summary>
        public string extendField2 { get; set; }

        /// <summary>
        /// 扩展字段3	
        /// </summary>
        public string extendField3 { get; set; }

        /// <summary>
        /// 扩展字段4	
        /// </summary>
        public int extendField4 { get; set; }

        /// <summary>
        /// 扩展字段5	
        /// </summary>
        public int extendField5 { get; set; }

        /// <summary>
        /// 授权获取access_token的京东账号，属性要有，值可以为空	
        /// </summary>
        public string pin { get; set; }

        /// <summary>
        /// 应用的app_key	
        /// </summary>
        public string appKey { get; set; }

        /// <summary>
        /// 快递费付款方式(顺丰必填)，1:寄方付 2:收方付 3:第三方付	
        /// </summary>
        public string expressPayMethod { get; set; }

        /// <summary>
        /// 快件产品类别，目前顺丰使用，后续可根据承运商自己定义 。1-顺丰标快；2-顺丰特惠	
        /// </summary>
        public string expressType { get; set; }

        /// <summary>
        /// 商家结算编码，直营承运商必填	
        /// </summary>
        public string settlementCode { get; set; }
    }

    public class WaybillAddress
    {
        /// <summary>
        /// 省/直辖市id	
        /// </summary>
        public int provinceId { get; set; }

        /// <summary>
        /// 省/直辖市名称	
        /// </summary>
        public string provinceName { get; set; }

        /// <summary>
        /// 市id	
        /// </summary>
        public int cityId { get; set; }

        /// <summary>
        /// 市名称	
        /// </summary>
        public string cityName { get; set; }

        /// <summary>
        /// 区/县id	
        /// </summary>
        public int countryId { get; set; }

        /// <summary>
        /// 区/县名称	
        /// </summary>
        public string countryName { get; set; }

        /// <summary>
        /// 乡镇/街道id	
        /// </summary>
        public int countrysideId { get; set; }

        /// <summary>
        /// 县镇/街道名称	
        /// </summary>
        public string countrysideName { get; set; }

        /// <summary>
        /// 详细地址	
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 发货联系人	
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        /// 联系人电话	
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 联系人手机	
        /// </summary>
        public string mobile { get; set; }
    }
}
