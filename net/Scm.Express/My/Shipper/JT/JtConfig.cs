namespace Com.Scm.Express.My.Shipper.JT
{
    public class JtConfig : MyConfig
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 合作网点编码
        /// </summary>
        public string network { get; set; }
        public string expressType { get; set; }
        /// <summary>
        /// 物品类型
        /// </summary>
        public string goodsType { get; set; }
        /// <summary>
        /// 寄件国家三字码
        /// </summary>
        public string countryCode { get; set; }

        public override void LoadDef()
        {
            customerCode = "J0086474299";
            expressType = "EZ";
            goodsType = "bm000006";
        }
    }
}
