namespace Com.Scm.Express.My.Shipper.SF.Exp.Model
{
    public class WaybillNoInfo
    {
        /// <summary>
        /// 运单号类型1：母单 2 :子单 3 : 签回单
        /// </summary>
        public WaybillType waybillType { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string boxNo { get; set; }
        /// <summary>
        /// 长(cm)
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 宽(cm)
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 高(cm)
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 重量(kg)
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 体积（立方厘米）
        /// </summary>
        public string volume { get; set; }
    }
}
