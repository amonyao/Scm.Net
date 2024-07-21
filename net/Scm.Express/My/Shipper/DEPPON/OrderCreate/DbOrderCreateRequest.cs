using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.DEPPON.OrderCreate
{
    public class DbOrderCreateRequest : DbRequest
    {
        public override string GetServicePath()
        {
            if (MyExpress.Environment == Dto.EnvironmentEnum.Release)
            {
                return "http://gwapi.deppon.com/dop-interface-async/standard-order/createOrderNotify.action";
            }

            return "http://dpsanbox.deppon.com/sandbox-web/dop-standard-ewborder/createOrderNotify.action";
        }

        public override string GetServiceCode()
        {
            return "CREATE_ORDER_NOTIFY";
        }

        /// <summary>
        /// 渠道单号
        /// 由第三方接入商产生的订单号（生成规则为sign+客户自己订单号，sign值由德邦开放平台自动生成），注意：同一票子母件（一票多货），logisticid必须要保持一致
        /// </summary>
        public string logisticID { get; set; }
        /// <summary>
        /// 客户订单号/商户订单号
        /// 客户的订单号
        /// </summary>
        public string custOrderNo { get; set; }
        /// <summary>
        /// 运单号
        /// 预埋单号时传运单号，不传时会返回运单号给客户,仅支持单个单号
        /// </summary>
        public string mailNo { get; set; }
        /// <summary>
        /// 是否需要自动订阅轨迹
        /// 1：是（为是时要对接轨迹推送接口） 2：否 默认否
        /// </summary>
        public int needTraceInfo { get; set; }
        /// <summary>
        /// 第三方接入商的公司编码
        /// 渠道来源,查看途径:登录德邦开放平台-我要接入-基本信息
        /// </summary>
        public string companyCode { get; set; }
        /// <summary>
        /// 下单模式
        /// 1、散单上门取件模式（单量小，发货地址不固定,需系统通知接货员上门取件,由接货员打单,适用门店调拨，退换货等场景;整车订单也选此模式）； 2、大客户热敏电子面单模式（单量大,发货地址固定,不需要系统通知接货员上门取件,由客户打印热敏面单贴在货上，适用电商等固定仓库批量出货场景）; 3、快递筛单下单模式（单量大,发货地址固定,由客户打印热敏面单贴在货上,只支持快递产品,不可达区域会直接下单失败）
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 运输方式/产品类型
        /// （具体传值请与月结合同签订约定的为准，否则可能影响计费） 快递运输方式: ①RCP：大件快递360 ②NZBRH：重包入户 ③ZBTH：重包特惠 ④WXJTH：微小件特惠 ⑤JJDJ：经济大件 ⑥PACKAGE：标准快递 ⑦HKDJC：航空大件次日达 ⑧HKDJG：航空大件隔日达 ⑨TZKJC：特快专递; 零担运输方式：①JZKY：精准空运（仅散客模式支持该运输方式）②JZQY_LONG：精准汽运 ③JZKH：精准卡航 ④JZZH：精准重货； 整车运输方式:①ZCPS:整车配送 ②JZZHC:精准专车 ③JZZHC:精准专车 ④JZPC:精准拼车
        /// </summary>
        public string transportType { get; set; }
        /// <summary>
        /// 客户编码/月结账号
        /// 德邦一线营业部给到客户的月结客户编码，由营业部给出。沙箱环境，下子母件订单必须传值 219401 或者219402
        /// </summary>
        public string customerCode { get; set; }
        public DbContactSender sender { get; set; }
        public DbContactReceiver receiver { get; set; }
        public DbPackageInfo packageInfo { get; set; }
        /// <summary>
        /// 订单提交时间
        /// 系统当前时间,格式为 yyyy-MM-dd HH:mm:ss 如 2020-11-07 18:44:19
        /// </summary>
        public string gmtCommit { get; set; }
        /// <summary>
        /// 支付方式
        /// 0、发货人付款（现付）（大客户模式不支持寄付） 1、收货人付款（到付） 2、发货人付款（月结）
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 增值服务
        /// </summary>
        public DbAddServices addServices { get; set; }
        /// <summary>
        /// 短信通知
        /// Y：需要 N: 不需要
        /// </summary>
        public string smsNotify { get; set; }
        /// <summary>
        /// 上门接货开始时间
        /// 方便上门接货的时间范围(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string sendStartTime { get; set; }
        /// <summary>
        /// 上门接货结束时间
        /// 上门接货结束时间
        /// </summary>
        public string sendEndTime { get; set; }
        /// <summary>
        /// 原运单号
        /// 异地调货退货场景可能用到
        /// </summary>
        public string originalWaybillNumber { get; set; }
        /// <summary>
        /// 备注
        /// 注意事项（备注）
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 是否外发
        /// Y：需要 N: 不需要（仅适用于下单模式为2且是零担运输方式；或者下单模式为3时）
        /// </summary>
        public string isOut { get; set; }
        /// <summary>
        /// 是否口令签收
        /// 仅适用于快递，Y：需要 N: 不需要；若为Y，必须收货人提供验证码给快递员才能签收，该服务是有偿的，具体费用请咨询我司门店
        /// </summary>
        public string passwordSigning { get; set; }
        /// <summary>
        /// 是否可派送
        /// Y：是 N:否
        /// </summary>
        public string isdispatched { get; set; }
        /// <summary>
        /// 是否预售单
        /// Y：是 N: 否
        /// </summary>
        public string ispresaleorder { get; set; }
        /// <summary>
        /// 是否合伙人自提
        /// Y：是 N: 否（只适用于同步筛单下单模式）
        /// </summary>
        public string isPickupSelf { get; set; }
        /// <summary>
        /// 是否接受仅镇中心派送
        /// Y：是 N:否,若空时，默认为Y（只适用于同步筛单下单模式）
        /// </summary>
        public string isCenterDelivery { get; set; }
        /// <summary>
        /// 扩展字段
        /// 1.如需传值货物唯一码，key值（变量名）必须为custewb_number，value值为货物唯一码，以逗号分隔，且唯一码数量与件数一致，每个唯一码长度50； 2.如有快递送装一体需求，key值（变量名）必须为serviceType，value值为家装品类名称（家具送装：FURNITURE_EQUIP；家电送装：HOME_APPLICATION_EQUIP；建材送装：BUILD_MATERIAL_EQUIP；卫浴：WASHROOM_EQUIP 灯具：FITNESS_EQUIP；）
        /// </summary>
        public List<DbOrderExtendFields> orderExtendFields { get; set; }
    }

    public class DbPackageInfo
    {
        /// <summary>
        /// 货物名称
        /// </summary>
        public string cargoName { get; set; }
        /// <summary>
        /// 总件数（包裹数）
        /// 包裹数(指实际交接给我司已打包后的总包裹数)，子母件场景下,一个包裹对应一个运单号；若包裹数大于1，则返回一个母运单号和N-1个子运单号，（单次下单总件数上限为30件，如果大于30请分多次下单）
        /// </summary>
        public int totalNumber { get; set; }
        /// <summary>
        /// 总重量
        /// 单个包裹重量，如果是多件累计的情况下，请传总重量/总件数后得出的均值，单位：kg,可以精确到小数点后两位
        /// </summary>
        public double totalWeight { get; set; }
        /// <summary>
        /// 总体积
        /// 单个包裹体积，如果是多件累计的情况下，请传总体积/总件数后得出的均值，单位：m3,可以精确到小数点后三位
        /// </summary>
        public double totalVolume { get; set; }
        /// <summary>
        /// 包装
        /// 包装（直接用中文） ： 纸、纤、木箱、木架、托膜、托木（大客户模式下运输方式为零担时必填）
        /// </summary>
        public string packageService { get; set; }
        /// <summary>
        /// 送货方式
        /// 1、自提； 2、送货进仓； 3、送货（不含上楼）； 4、送货上楼； 5、大件上楼 ;7、送货安装；8、送货入户
        /// </summary>
        public string deliveryType { get; set; }
    }

    public class DbAddServices
    {
        /// <summary>
        /// 保价金额
        /// 如为空，则开单时默认为0，单位：元
        /// </summary>
        public string insuranceValue { get; set; }
        /// <summary>
        /// 代收货款类型
        /// 0：无代收 1：即日退 3：三日退 代收货款金额不为0时，此项客户必填，代收货款金额为0或为空，则代收类型默认为无
        /// </summary>
        public string codType { get; set; }
        /// <summary>
        /// 代收货款客户账号
        /// 代收货款金额不为0时，此项客户必填
        /// </summary>
        public string reciveLoanAccount { get; set; }
        /// <summary>
        /// 代收货款客户开户名
        /// 代收货款金额不为0时，此项客户必填
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 代收货款金额
        /// 如为空，则开单时默认为0，单位：元
        /// </summary>
        public string codValue { get; set; }
        /// <summary>
        /// 签收回单
        /// 0:无需返单 1：签收单原件返回 2:电子签收单（电子签名图片需要另外对接查询接口）
        /// </summary>
        public string backSignBill { get; set; }
    }

    public class DbOrderExtendFields
    {
        /// <summary>
        /// 订单扩展属性键
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 订单扩展属性值
        /// </summary>
        public string value { get; set; }
    }
}
