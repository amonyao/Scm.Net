namespace Com.Scm.Express.My.Shipper.SF.Scs.OrderCreate
{
    public class SfScsOrderCreateResponse
    {
        /// <summary>
        /// SF生成订单号 (失败不产生)
        /// </summary>
        public string sfOrderNo { get; set; }
        /// <summary>
        /// 外部系统订单号(客户方系统订单号)
        /// </summary>
        public string erpOrder { get; set; }
    }
}
