using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Exp.Model;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderCreate
{
    public class SfOrderCreateRequest : SfExpRequest
    {
        /// <summary>
        /// 响应报文的语言， 缺省值为zh-CN，目前支持以下值zh-CN 表示中文简体， zh-TW或zh-HK或 zh-MO表示中文繁体， en表示英文
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// 客户订单号，不能重复
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 顺丰运单号
        /// </summary>
        public List<WaybillNoInfo> waybillNoInfoList { get; set; }

        /// <summary>
        /// 报关信息
        /// </summary>
        public CustomsInfo customsInfo { get; set; }

        /// <summary>
        /// 托寄物信息
        /// </summary>
        public List<CargoDetail> cargoDetails { get; set; }

        /// <summary>
        /// 拖寄物类型描述,如： 文件，电子产品，衣服等
        /// </summary>
        public string cargoDesc { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public List<ExtraInfo> extraInfoList { get; set; }

        /// <summary>
        /// 增值服务信息，支持附录： 《增值服务产品表》
        /// </summary>
        public List<ServiceInfo> serviceList { get; set; }

        /// <summary>
        /// 收寄双方信息
        /// </summary>
        public List<ContactInfo> contactInfoList { get; set; }

        /// <summary>
        /// 顺丰月结卡号 月结支付时传值，现结不需传值；沙箱联调可使用测试月结卡号7551234567（非正式，无须绑定，仅支持联调使用）
        /// </summary>
        public string monthlyCard { get; set; }

        /// <summary>
        /// 付款方式，支持以下值： 1:寄方付 2:收方付 3:第三方付
        /// </summary>
        public string payMethod { get; set; }

        /// <summary>
        /// 快件产品类别， 支持附录 《快件产品类别表》 的产品编码值，仅可使用与顺丰销售约定的快件产品
        /// </summary>
        public string expressTypeId { get; set; }

        /// <summary>
        /// 包裹数，一个包裹对应一个运单号；若包裹数大于1，则返回一个母运单号和N-1个子运单号
        /// </summary>
        public string parcelQty { get; set; }

        /// <summary>
        /// 客户订单货物总长，单位厘米， 精确到小数点后3位， 包含子母件
        /// </summary>
        public string totalLength { get; set; }

        /// <summary>
        /// 客户订单货物总宽，单位厘米， 精确到小数点后3位， 包含子母件
        /// </summary>
        public string totalWidth { get; set; }

        /// <summary>
        /// 客户订单货物总高，单位厘米， 精确到小数点后3位， 包含子母件
        /// </summary>
        public string totalHeight { get; set; }

        /// <summary>
        /// 订单货物总体积，单位立方厘米, 精确到小数点后3位，会用于计抛 (是否计抛具体商务沟通中 双方约定)
        /// </summary>
        public string volume { get; set; }

        /// <summary>
        /// 订单货物总重量（郑州空港海关必填）， 若为子母件必填， 单位千克， 精确到小数点后3位，如果提供此值， 必须>0 (子母件需>6)
        /// </summary>
        public string totalWeight { get; set; }

        /// <summary>
        /// 商品总净重
        /// </summary>
        public string totalNetWeight { get; set; }

        /// <summary>
        /// 要求上门取件开始时间， 格式： YYYY-MM-DD HH24:MM:SS， 示例： 2012-7-30 09:30:00 （预约单传预约截止时间，不赋值默认按当前时间下发，1小时内取件）
        /// </summary>
        public string sendStartTm { get; set; }

        /// <summary>
        /// 是否通过手持终端 通知顺丰收派 员上门收件，支持以下值： 1：要求 0：不要求
        /// </summary>
        public string isDocall { get; set; }

        /// <summary>
        /// 是否返回签回单 （签单返还）的运单号， 支持以下值： 1：要求 0：不要求
        /// </summary>
        public string isSignBack { get; set; }

        /// <summary>
        /// 客户参考编码：如客户原始订单号
        /// </summary>
        public string custReferenceNo { get; set; }

        /// <summary>
        /// 温度范围类型，当 express_type为12 医药温控件 时必填，支持以下值： 1:冷藏 3：冷冻
        /// </summary>
        public string temperatureRange { get; set; }

        /// <summary>
        /// 订单平台类型 （对于平台类客户， 如果需要在订单中 区分订单来源， 则可使用此字段） 天猫:tmall， 拼多多：pinduoduo， 京东 : jd 等平台类型编码
        /// </summary>
        public string orderSource { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 快件自取，支持以下值： 1：客户同意快件自取 0：客户不同意快件自取
        /// </summary>
        public string isOneselfPickup { get; set; }

        /// <summary>
        /// 筛单特殊字段用来人工筛单
        /// </summary>
        public string filterField { get; set; }

        /// <summary>
        /// 是否返回用来退货业务的 二维码URL， 支持以下值： 1：返回二维码 0：不返回二维码
        /// </summary>
        public string isReturnQRCode { get; set; }

        /// <summary>
        /// 特殊派送类型代码 1:身份验证
        /// </summary>
        public string specialDeliveryTypeCode { get; set; }

        /// <summary>
        /// 特殊派件具体表述 证件类型: 证件后8位如： 1:09296231（1 表示身份证， 暂不支持其他证件）
        /// </summary>
        public string specialDeliveryValue { get; set; }

        /// <summary>
        /// 商户支付订单号
        /// </summary>
        public string merchantPayOrderNo { get; set; }

        /// <summary>
        /// 是否返回签回单路由标签： 默认0， 1：返回路由标签， 0：不返回
        /// </summary>
        public string isReturnSignBackRoutelabel { get; set; }

        /// <summary>
        /// 是否返回路由标签： 默认1， 1：返回路由标签， 0：不返回；除部分特殊用户外，其余用户都默认返回
        /// </summary>
        public string isReturnRoutelabel { get; set; }

        /// <summary>
        /// 是否使用国家统一面单号 1：是， 0：否（默认）
        /// </summary>
        public string isUnifiedWaybillNo { get; set; }

        /// <summary>
        /// 签单返还范本地址
        /// </summary>
        public string podModelAddress { get; set; }

        /// <summary>
        /// 头程运单号（郑州空港海关必填）
        /// </summary>
        public string inProcessWaybillNo { get; set; }

        /// <summary>
        /// 是否需求分配运单号1：分配 0：不分配（若带单号下单，请传值0）
        /// </summary>
        public string isGenWaybillNo { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://bspgw.sf-express.com/std/service";
            }

            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_CREATE_ORDER";
        }
    }
}
