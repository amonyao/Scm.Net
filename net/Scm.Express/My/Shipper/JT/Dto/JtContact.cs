namespace Com.Scm.Express.My.Shipper.JT.Dto
{
    public class JtContact
    {
        ///<summary>
        ///  寄件人姓名
        ///</summary>
        public string name { get; set; }
        ///<summary>
        ///  寄件公司
        ///</summary>
        public string company { get; set; }
        ///<summary>
        ///  寄件邮编
        ///</summary>
        public string postCode { get; set; }
        ///<summary>
        ///  寄件邮箱
        ///</summary>
        public string mailBox { get; set; }
        ///<summary>
        ///  寄件手机（手机和电话二选一必填）
        ///</summary>
        public string mobile { get; set; }
        ///<summary>
        ///  寄件电话（手机和电话二选一必填）
        ///</summary>
        public string phone { get; set; }
        ///<summary>
        ///  寄件国家三字码（如：中国=CHN、印尼=IDN）
        ///</summary>
        public string countryCode { get; set; }
        ///<summary>
        ///  寄件省份
        ///</summary>
        public string prov { get; set; }
        ///<summary>
        ///  寄件城市
        ///</summary>
        public string city { get; set; }
        ///<summary>
        ///  寄件区域
        ///</summary>
        public string area { get; set; }
        ///<summary>
        ///  寄件乡镇
        ///</summary>
        public string town { get; set; }
        ///<summary>
        ///  寄件街道
        ///</summary>
        public string street { get; set; }
        ///<summary>
        ///  寄件详细地址
        ///</summary>
        public string address { get; set; }
    }
}
