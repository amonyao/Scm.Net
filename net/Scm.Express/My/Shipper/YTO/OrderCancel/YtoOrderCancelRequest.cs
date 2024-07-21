using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YTO.OrderCancel
{
    public class YtoOrderCancelRequest : YtoRequest
    {
        /// <summary>
        /// 物流单号，打印拉取运单号前，物流单号和渠道唯一确定一笔快递物流订单。
        /// </summary>
        public string logisticsNo { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string cancelDesc { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "https://openuat.yto56.com.cn:6443/open/korder_cancel_adapter/v1/PFUYji/K21000119";
        }

        public override string GetServiceCode()
        {
            return "inreach_adapter";
        }

        public override string GetServiceVer()
        {
            return "v1";
        }

        public override string GetPartnerCode()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "TEST";
        }

        public override string GetPartnerPass()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "123456";
        }
    }
}
