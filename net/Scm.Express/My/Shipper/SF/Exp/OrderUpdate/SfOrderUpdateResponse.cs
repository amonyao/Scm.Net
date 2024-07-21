using Com.Scm.Express.My.Shipper.SF.Exp.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderUpdate
{
    public class SfOrderUpdateResponse
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 顺丰运单号
        /// </summary>
        public List<WaybillNoInfo> waybillNoInfoList { get; set; }

        /// <summary>
        /// 备注 1:客户订单号与顺丰运单不匹配 2 :操作成功
        /// </summary>
        public ResStatus resStatus { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string extraInfoList { get; set; }
    }

    public enum ResStatus
    {
        [Description("客户订单号与顺丰运单不匹配")]
        Failure = 1,
        [Description("操作成功")]
        Success = 2
    }
}
