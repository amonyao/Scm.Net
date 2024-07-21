using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderUpdate
{
    public class SfOrderUpdateRequest : SfExpRequest
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 客户订单操作标识: 1:确认 (丰桥下订单接口默认自动确认，不需客户重复确认，该操作用在其它非自动确认的场景) 2:取消
        /// </summary>
        public string dealType { get; set; }

        /// <summary>
        /// 顺丰运单号(如dealtype=1， 必填)
        /// </summary>
        public List<OrderUpdateWaybillNoInfo> waybillNoInfoList { get; set; }

        /// <summary>
        /// 报关批次
        /// </summary>
        public string customsBatchs { get; set; }

        /// <summary>
        /// 揽收员工号
        /// </summary>
        public string collectEmpCode { get; set; }

        /// <summary>
        /// 头程运单号
        /// </summary>
        public string inProcessWaybillNo { get; set; }

        /// <summary>
        /// 原寄地网点代码
        /// </summary>
        public string sourceZoneCode { get; set; }

        /// <summary>
        /// 目的地网点代码
        /// </summary>
        public string destZoneCode { get; set; }

        /// <summary>
        /// 订单货物总重量，包含子母 件，单位千克，精确到小数点 后3位，如果提供此值，必 须>0
        /// </summary>
        public string totalWeight { get; set; }

        /// <summary>
        /// 订单货物总体积，单位立方厘 米，精确到小数点后3位，会 用于计抛（是否计抛具体商务 沟通中双方约定）
        /// </summary>
        public string totalVolume { get; set; }

        /// <summary>
        /// 快件产品类别，支持附录《快 件产品类别表》的产品编码 值，仅可使用与顺丰销售约定 的快件产品
        /// </summary>
        public string expressTypeId { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string extraInfoList { get; set; }

        /// <summary>
        /// 客户订单货物总长，单位厘米， 精确到小数点后3位，包含子 母件
        /// </summary>
        public string totalLength { get; set; }

        /// <summary>
        /// 客户订单货物总宽，单位厘米， 精确到小数点后3位，包含子 母件
        /// </summary>
        public string totalWidth { get; set; }

        /// <summary>
        /// 客户订单货物总高，单位厘米， 精确到小数点后3位，包含子 母件
        /// </summary>
        public string totalHeight { get; set; }

        /// <summary>
        /// 增值服务信息
        /// </summary>
        public string serviceList { get; set; }

        /// <summary>
        /// 是否走新通用确认1：支持修改联系人 2：支持改其他客户订单默认0
        /// </summary>
        public string isConfirmNew { get; set; }

        /// <summary>
        /// 收件人信息
        /// </summary>
        public string destContactInfo { get; set; }

        /// <summary>
        /// 是否通过手持终端通知顺丰收派员上门收件， 支持以下值：1：要求其它为不要求
        /// </summary>
        public string isDocall { get; set; }

        /// <summary>
        /// 1. 特殊派送类型代码 身份验证 2. 极效前置单
        /// </summary>
        public string specialDeliveryTypeCode { get; set; }

        /// <summary>
        /// 1> 特殊派件具体表述 证件类型:证件后8位 如：1:09296231（1表示身份证，暂不支持其他证件） 2>.极效前置单时:Y:若不支持则返回普通运单N:若不支持则返回错误码
        /// </summary>
        public string specialDeliveryValue { get; set; }

        /// <summary>
        /// 预约时间(上门揽收时间)
        /// </summary>
        public string sendStartTm { get; set; }

        /// <summary>
        /// 上门揽收截止时间
        /// </summary>
        public string pickupAppointEndtime { get; set; }

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
            return "EXP_RECE_UPDATE_ORDER";
        }
    }

    public class OrderUpdateWaybillNoInfo
    {
        /// <summary>
        /// 运单号类型 1：母单 2 :子单 3 : 签回单
        /// </summary>
        public string waybillType { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillNo { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string boxNo { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public string length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 体积（立方厘米）
        /// </summary>
        public string volume { get; set; }
    }

    public class OrderUpdateExtraInfo
    {
        /// <summary>
        /// 扩展字段 说明： attrName为字段定义， 具体如下表，value存在 attrVal
        /// </summary>
        public string attrName { get; set; }
        /// <summary>
        /// 扩展字段值
        /// </summary>
        public string attrVal { get; set; }
    }

    public class OrderUpdateService
    {
        /// <summary>
        /// 增值服务名，如COD等
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 增值服务扩展属性，参考增值 服务传值说明
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string attrVal { get; set; }
        /// <summary>
        /// 增值服务扩展属性1
        /// </summary>
        public string value1 { get; set; }
        /// <summary>
        /// 增值服务扩展属性2
        /// </summary>
        public string value2 { get; set; }
        /// <summary>
        /// 增值服务扩展属性3
        /// </summary>
        public string value3 { get; set; }
        /// <summary>
        /// 增值服务扩展属性4
        /// </summary>
        public string value4 { get; set; }
    }

    public class OrderContactInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 方手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 国家或地区 2位代码参照附录国家代码附件
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 所在省级行政区名称如：北京、广东省、广西壮族自治区等
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 所在地级行政区名称，必须是标准的城市称谓 如：北京市、深圳市、大理白族自治州等
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 所在县/区级行政区名称，必须是标准的县/区称谓，如：福田区
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 详细地址，若province/city字段的值不传，此字段必须包含省市
        /// </summary>
        public string address { get; set; }
    }
}
