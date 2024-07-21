namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class OrderServices
    {
        /// <summary>
        /// 增值服务编码 参考附录4.1说明
        /// </summary>
        public string serviceCode { get; set; }
        /// <summary>
        /// 增值服务内容 如有金额的增值服务(保价,代收货款)
        /// </summary>
        public string serviceValue { get; set; }
        /// <summary>
        /// 预留字段(用于扩展,更多增值服务更多属性)
        /// </summary>
        public string userDef1 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef2 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef3 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef4 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef5 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef6 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef7 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string userDef8 { get; set; }
    }
}
