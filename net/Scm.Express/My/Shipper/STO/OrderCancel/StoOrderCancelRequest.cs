namespace Com.Scm.Express.My.Shipper.STO.OrderCancel
{
    public class StoOrderCancelRequest : StoRequest
    {
        /// <summary>
        /// 客户订单号（与运单号二者必传其一）ps：最好使用运单号进行取消
        /// </summary>
        public string sourceOrderId { get; set; }

        /// <summary>
        /// 运单号（与客户订单号必传其一） ps：最好使用运单号进行取消
        /// </summary>
        public string billCode { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string orderSource { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 取消人姓名
        /// </summary>
        public string creater { get; set; }

        /// <summary>
        /// 02:调度订单（默认值），01：普通订单
        /// </summary>
        public string orderType { get; set; }

        public override string GetApiName()
        {
            return "EDI_MODIFY_ORDER_CANCEL";
        }

        public override string GetToAppKey()
        {
            return "edi_modify_order";
        }

        public override string GetToCode()
        {
            return "edi_modify_order";
        }
    }
}
