using System.ComponentModel;

namespace Com.Scm.Express.My.Shipper.SF.Exp.Model
{
    public class ContactInfo
    {
        /// <summary>
        /// 地址类型： 1，寄件方信息 2，到件方信息
        /// </summary>
        public ContactType contactType { get; set; }
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
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 城市代码或国家代码,参考附录 《城市代码表》，如果是跨境件，则此字段为必填
        /// </summary>
        public string zoneCode { get; set; }
        /// <summary>
        /// 国家或地区2位代码 参照附录《城市代码表》
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 所在省级行政区名称，必须是标准的省级行政区名称如：北 京、广东省、广西壮族自治区等；此字段影响原寄地代码识 别，建议尽可能传该字段的值
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 所在地级行政区名称，必须是标准的城市称谓 如：北京市、 深圳市、大理白族自治州等； 此字段影响原寄地代码识别， 建议尽可能传该字段的值
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 所在县/区级行政区名称，必须 是标准的县/区称谓，如：福田区，南涧彝族自治县、准格尔旗等
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 详细地址，若有四级行政区划，如镇/街道等信息可拼接至此字段，格式样例：镇/街道+详细地址。若province/city 字段的值不传，此字段必须包含省市信息，避免影响原寄地代码识别，如：广东省深圳市福田区新洲十一街万基商务大厦10楼；此字段地址必须详细，否则会影响目的地中转识别；
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 邮编，跨境件必填（中国内地， 港澳台互寄除外）
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string taxNo { get; set; }
    }

    public enum ContactType
    {
        [Description("寄件方信息")]
        Consigner = 1,
        [Description("到件方信息")]
        Consignee = 2
    }
}
