namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class OrderReturn
    {
        /// <summary>
        /// 签回单号
        /// </summary>
        public string returnWaybillNo { get; set; }
        /// <summary>
        /// 发货公司
        /// </summary>
        public string senderCompany { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string senderName { get; set; }
        /// <summary>
        /// 发货人手机号
        /// </summary>
        public string senderMobile { get; set; }
        /// <summary>
        /// 发货人座机号码
        /// </summary>
        public string senderPhone { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-国家名称
        /// </summary>
        public string senderCountryName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-省名称
        /// </summary>
        public string senderProvinceName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-市名称
        /// </summary>
        public string senderCityName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-区名称
        /// </summary>
        public string senderAreaName { get; set; }
        /// <summary>
        /// 客户发货地址（小哥收件地址）-详细地址
        /// </summary>
        public string senderDetailAddress { get; set; }
        /// <summary>
        /// 收货公司
        /// </summary>
        public string receiverCompany { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receiverName { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string receiverMobile { get; set; }
        /// <summary>
        /// 收货人座机号码
        /// </summary>
        public string receiverPhone { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-国家名称
        /// </summary>
        public string receiverCountryName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-省名称
        /// </summary>
        public string receiverProvinceName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-市名称
        /// </summary>
        public string receiverCityName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-区名称
        /// </summary>
        public string receiverAreaName { get; set; }
        /// <summary>
        /// 客户收货地址（小哥派件地址）-详细地址
        /// </summary>
        public string receiverDetailAddress { get; set; }
    }
}
