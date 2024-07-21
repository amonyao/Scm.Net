using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Scs.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace Com.Scm.Express.My.Shipper.SF.Scs.OrderCreate
{
    public class SfScsOrderCreateRequest : SfScsRequest
    {
        /// <summary>
        /// ERP单号(客户订单号)不可以重复，取消再下发，重新生成一个新的ERP单号
        /// </summary>
        public string erpOrder { get; set; }
        /// <summary>
        /// 产品类型代码 详细请见附录4.3 传承运商产品编码（例如：SE0022）
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string focusCode { get; set; }
        /// <summary>
        /// 月结账号(到付,寄付现结不填) SF提供
        /// </summary>
        public string monthlyAccount { get; set; }
        /// <summary>
        /// 客户下单时间 格式为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string orderTime { get; set; }
        /// <summary>
        /// 付款方式 见附4.4 （例如：PR_ACCOUNT）大写半角
        /// </summary>
        public string paymentTypeCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 发货公司
        /// </summary>
        public string shipperName { get; set; }
        /// <summary>
        /// 发货联系人姓名
        /// </summary>
        public string shipperContactName { get; set; }
        /// <summary>
        /// 发货联系人电话(手机) 支持分机号 只支持一个传值
        /// </summary>
        public string shipperContactTel { get; set; }
        /// <summary>
        /// 提货省份名称 要求精准(广东省)
        /// </summary>
        public string shipperProvinceName { get; set; }
        /// <summary>
        /// 提货城市名称 要求精准(深圳市)
        /// </summary>
        public string shipperCityName { get; set; }
        /// <summary>
        /// 提货县区名称 要求精准(南山区)
        /// </summary>
        public string shipperDistrictName { get; set; }
        /// <summary>
        /// 提货地点名称 不用包含省市区(海天一路软件产业基地)
        /// </summary>
        public string shipperLocationName { get; set; }
        /// <summary>
        /// 提货开始时间(有提货增值服务时候填写) 格式为 yyyy-MM-dd HH:mm:ss字符串
        /// </summary>
        public string requirePickupTimeFm { get; set; }
        /// <summary>
        /// 提货结束时间(有提货增值服务时候填写) 格式为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string requirePickupTimeTo { get; set; }
        /// <summary>
        /// 收货公司
        /// </summary>
        public string consigneeName { get; set; }
        /// <summary>
        /// 收货联系人姓名
        /// </summary>
        public string consigneeContactName { get; set; }
        /// <summary>
        /// 收货联系人电话 支持分机号 只支持一个传值
        /// </summary>
        public string consigneeContactTel { get; set; }
        /// <summary>
        /// 送货省份名称 要求精准(广东省)
        /// </summary>
        public string consigneeProvinceName { get; set; }
        /// <summary>
        /// 送达城市名称 要求精准(深圳市)
        /// </summary>
        public string consigneeCityName { get; set; }
        /// <summary>
        /// 送达县区名称 要求精准(南山区)
        /// </summary>
        public string consigneeDistrictName { get; set; }
        /// <summary>
        /// 送达地点名称 不用包含省市区(海天一路软件产业基地)
        /// </summary>
        public string consigneeLocationName { get; set; }
        /// <summary>
        /// 送达开始时间 格式为 yyyy-MM-dd HH:mm:ss字符串
        /// </summary>
        public string requireDeliveryTimeFm { get; set; }
        /// <summary>
        /// 送达结束时间 格式为 yyyy-MM-dd HH:mm:ss 字符串
        /// </summary>
        public string requireDeliveryTimeTo { get; set; }
        /// <summary>
        /// 运输类型代码 默认LAND
        /// </summary>
        public string transportType { get; set; }
        /// <summary>
        /// 温层代码 参照附录4.2 例如：温层代码 5
        /// </summary>
        public string temperatureLevelCode { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public string totalWeight { get; set; }
        /// <summary>
        /// 总体积
        /// </summary>
        public string totalVolume { get; set; }
        /// <summary>
        /// 车辆类型(专车下单必填) 参照附录4.6
        /// </summary>
        public string carType { get; set; }
        /// <summary>
        /// 商品明细
        /// </summary>
        public List<OrderItem> orderItems { get; set; }
        /// <summary>
        /// 增值服务明细
        /// </summary>
        public string orderServices { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://sfapi.sf-express.com/std/service";
            }
            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "SCS_RECE_CREATE_ORDER";
        }
    }

    public enum ProductCode
    {
        [Description("冷运零担")]
        SE0030,
        [Description("冷运小票零担")]
        SE003001,
        [Description("冷运专车")]
        SE0031,
        [Description("冷运到店")]
        SE0059
    }
}
