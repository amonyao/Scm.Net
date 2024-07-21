namespace Com.Scm.Express.My.Shipper.DEPPON
{
    public class DbContact
    {
        /// <summary>
        /// 公司
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string county { get; set; }
        public string town { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
    }

    public class DbContactSender : DbContact
    {
        /// <summary>
        /// 发货部门编码
        /// 德邦门店编码(由德邦门店经理提供)，适用于需要指定接货门店的场景,如果不需要指定可以不填
        /// </summary>
        public string businessNetworkNo { get; set; }
    }

    public class DbContactReceiver : DbContact
    {
        /// <summary>
        /// 到达部门编码
        /// 德邦门店编码(由德邦门店经理提供)，适用于需要指定到达门店的场景,如果不需要指定可以不填
        /// </summary>
        public string toNetworkNo { get; set; }
    }
}
