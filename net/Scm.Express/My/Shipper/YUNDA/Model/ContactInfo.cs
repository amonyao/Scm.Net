namespace Com.Scm.Express.My.Shipper.YUNDA.Model
{
    public class ContactInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区/县
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// 固定电话(手机号固话必填一项)
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 手机号(手机号固话必填一项)
        /// </summary>
        public string mobile { get; set; }
    }
}
