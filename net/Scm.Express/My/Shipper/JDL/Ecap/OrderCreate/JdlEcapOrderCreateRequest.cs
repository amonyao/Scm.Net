using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCreate
{
    public class JdlEcapOrderCreateRequest : JdlRequest
    {
        /// <summary>
        /// 商家订单号；字段长度1-50。orderOrigin=1或者orderOrigin=2时请保证客户编码下唯一，若使用相同的订单号下单则会报错	
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 寄件人信息	
        /// </summary>
        public JdlEcapContact senderContact { get; set; }

        /// <summary>
        /// 收件人信息	
        /// </summary>
        public JdlEcapContact receiverContact { get; set; }

        /// <summary>
        /// 下单来源：0-c2c；1-b2c；2-c2b 详细说明：https://cloud.jdl.com/#/open-business-document/access-guide/267/54152	
        /// </summary>
        public int orderOrigin { get; set; }

        /// <summary>
        /// 客户编码；orderOrigin为 1 或2 时必填，与京东物流签约后生成；orderOrigin为 0时不传参；字段长度1-32	
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// 产品信息，主产品必填且只能填一种；增值产品选填，可选择多个	
        /// </summary>
        public JdlEcapProductInfo productsReq { get; set; }

        /// <summary>
        /// 付款方式；orderOrigin= 0时，枚举值：1-寄付；2-到付；orderOrigin= 1 时，枚举值：1-寄付；2-到付；3-月结；orderOrigin为2时，枚举值：5-多方收费，多方收费指下单费用由多个模块或主体来支付，逆向取件时运费、包装费以及保险费用可能由商家以及C消费者分别承担，具体参考c2bAddedSettleTypeInfo枚举传入	
        /// </summary>
        public int settleType { get; set; }

        /// <summary>
        /// 货品信息，寄递货物的基础信息，包括重量体积以及包裹的长宽高等。仅支持一组货品信息，不支持传多个货品对象	
        /// </summary>
        public List<JdlEcapCargoInfo> cargoes { get; set; }

        /// <summary>
        /// 商品信息；商品的基础信息；orderOrigin为 2 时必填，用于个人到商家的退货场景，快递小哥可核实退货的商品是否准确；orderOrigin不为 2 时不传此参数	
        /// </summary>
        public List<JdlEcapGoodsInfo> goods { get; set; }

        /// <summary>
        /// C2B详细费用收费方式；orderOrigin为2时，必须传参；当settleType是其他值时，此参数不传	
        /// </summary>
        public C2BAddedSettleTypeInfo c2bAddedSettleTypeInfo { get; set; }

        /// <summary>
        /// 期望揽收开始时间；orderOrigin为 2 时必填；时间戳，毫秒值；需大于下单时间小于预约揽收结束时间，揽收时会参考此时间。若预约揽收开始时间早于系统计算出的时间，则以系统时间为准，若晚于系统计算时间，则以预约揽收开始时间为准。 备注：以实际运营操作时间为准	
        /// </summary>
        public long? pickupStartTime { get; set; }

        /// <summary>
        /// 期望揽收结束时间；orderOrigin为 2 时必填；时间戳，毫秒值，需大于预约揽收开始时间（大于即可） 备注：以实际运营操作时间为准	
        /// </summary>
        public long? pickupEndTime { get; set; }

        /// <summary>
        /// 期望配送开始时间；时间戳，毫秒值，不可早于下单时间 备注：以实际运营操作时间为准	
        /// </summary>
        public long? expectDeliveryStartTime { get; set; }

        /// <summary>
        /// 期望配送结束时间，时间戳，毫秒值，不可早于下单时间 备注：以实际运营操作时间为准	
        /// </summary>
        public long? expectDeliveryEndTime { get; set; }

        /// <summary>
        /// 揽收方式：1-上门取件(默认) ；2-自送，默认1-上门取件	
        /// </summary>
        public int pickupType { get; set; }

        /// <summary>
        /// 下单备注；此信息显示在面单上，长度1-500；面单上截取前50个字符显示	
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 渠道信息	
        /// </summary>
        public JdlEcapChannelInfo CommonChannelInfo { get; set; }

        /// <summary>
        /// 京东物流运单号，预制条码下单时将提前获取的运单号赋予此字段	
        /// </summary>
        public string waybillCode { get; set; }

        /// <summary>
        /// 扩展信息，详细传参参考：https://cloud.jdl.com/#/open-business-document/access-guide/267/54309	
        /// </summary>
        public Dictionary<string, string> extendProps { get; set; }

        public override string GetPath()
        {
            return "/ecap/v1/orders/create";
        }

        public override string GetDomain()
        {
            return "ECAP";
        }
    }

    public class JdlEcapContact
    {
        /// <summary>
        /// 寄件人姓名；长度1-50，超出报错；不支持生僻字及emjio
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 寄件人手机号码，与寄件人电话不能同时为空。长度11位，做基础正则校验，最多仅支持传入1个手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 寄件全量四级地址；长度1-200，超出报错；必须包含省市
        /// </summary>
        public string fullAddress { get; set; }
        /// <summary>
        /// 发货仓编码；长度1-50，用于匹配揽收站点，入参传入此信息时，优先以此获取揽收站点，本信息可通过销售或销售支持获取
        /// </summary>
        public string warehouseCode { get; set; }
    }

    public class JdlEcapProductInfo
    {
        /// <summary>
        /// 主产品编码，只能填一种，枚举值参照「主产品信息」https://cloud.jdl.com/#/open-business-document/access-guide/267/54153。仅可使用与京东快递销售约定的快递产品
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 主产品属性，当主产品是：「LL-HD-M」生鲜特惠、「LL-SD-M」生鲜特快时必填；主产品是「ed-m-0017」函速达时选填；其他产品不填；赋值方法参照「主产品属性」https://cloud.jdl.com/#/open-business-document/access-guide/267/54154
        /// </summary>
        public Dictionary<string, string> productAttrs { get; set; }
        /// <summary>
        /// 增值服务产品信息，可根据实际填写多个
        /// </summary>
        public List<JdlEcapProductInfo> addedProducts { get; set; }
    }

    public class JdlEcapCargoInfo
    {
        /// <summary>
        /// 拖寄物描述；长度1-200；不可含有生僻字或Emoji表情	
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 件数（包裹数），长度1-8	
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// 重量；单位：kg；保留小数点后两位；必须大于0，不同的产品类型，重量上限不同，最大不能超过10000KG	
        /// </summary>
        public double weight { get; set; }

        /// <summary>
        /// 体积：单位：cm³；保留小数点后两位；必须大于0，如果length、width、 hight均不是空，此处应填写三者之积	
        /// </summary>
        public double volume { get; set; }

        /// <summary>
        /// 包裹长度，长度1-8，未填默认补0	
        /// </summary>
        public double length { get; set; }

        /// <summary>
        /// 包裹宽度，长度1-8，未填默认补0	
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// 包裹高度，长度1-8，未填默认补0	
        /// </summary>
        public double hight { get; set; }

        /// <summary>
        /// 备注 ，长度1-200，小哥手持终端上可查看此信息，最长支持展示前50个字符，截取前50个字符	
        /// </summary>
        public string remark { get; set; }

    }

    public class JdlEcapGoodsInfo
    {
        /// <summary>
        /// 商品名称；商家自定义；orderOrigin为 2 时必填；长度1-200，超出字段最大长度报错
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品编码；商家自定义，长度1-50，超出字段最大长度报错
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 商品金额，单位：元，需大于0
        /// </summary>
        public double amount { get; set; }
        /// <summary>
        /// 商品件数，单位：件；orderOrigin为2时必填；需大于0
        /// </summary>
        public int quantity { get; set; }
    }

    public class C2BAddedSettleTypeInfo
    {
        /// <summary>
        /// orderOrigin为2时必填；基础运费付款方式；枚举值：1-寄付现结 ；3-月结
        /// </summary>
        public int basicFreigthSettleType { get; set; }
        /// <summary>
        /// orderOrigin为2时必填；揽收包装付款方式；枚举值：1-寄付现结 ；3-月结
        /// </summary>
        public int packageServiceSettleType { get; set; }
        /// <summary>
        /// orderOrigin为2时必填；保价服务费付款方式；枚举值：1-寄付现结；3-月结；当增值服务选择「保价服务」时，只支持3-月结
        /// </summary>
        public int guaranteeMoneyServiceSettleType { get; set; }
    }

    public class JdlEcapChannelInfo
    {
        /// <summary>
        /// 销售平台编码；orderOrigin为 1 时必填；枚举值：0010001-京东商城；0030001-其他平台
        /// </summary>
        public string channelCode { get; set; }
        /// <summary>
        /// 销售平台订单号，销售平台为0010001京东商城时必填
        /// </summary>
        public string channelOrderCode { get; set; }
    }
}
