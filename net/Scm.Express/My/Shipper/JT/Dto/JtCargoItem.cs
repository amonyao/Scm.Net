namespace Com.Scm.Express.My.Shipper.JT.Dto
{

    public class JtCargoItem
    {
        /// <summary>
        /// 物品类型:
        /// bm000001 文件
        /// bm000002 数码产品
        /// bm000003 生活用品
        /// bm000004 食品
        /// bm000005 服饰
        /// bm000006 其他
        /// bm000007 生鲜类
        /// bm000008 易碎品
        /// bm000009 液体
        /// </summary>
        public string itemType { get; set; }
        ///<summary>
        ///  物品名称
        ///</summary>
        public string itemName { get; set; }
        ///<summary>
        ///  物品中文名称
        ///</summary>
        public string chineseName { get; set; }
        ///<summary>
        ///  物品英文名称
        ///</summary>
        public string englishName { get; set; }
        ///<summary>
        ///  件数，≤1
        ///</summary>
        public int number { get; set; }
        ///<summary>
        ///  申报价值(数值型)
        ///</summary>
        public string itemValue { get; set; }
        ///<summary>
        ///  申报货款币别（默认本国币别，如：RMB）
        ///</summary>
        public string priceCurrency { get; set; }
        ///<summary>
        ///  物品描述
        ///</summary>
        public string desc { get; set; }
        ///<summary>
        ///  商品URL
        ///</summary>
        public string itemUrl { get; set; }
    }
}
