using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.ZTO.OrderSearch
{
    public class ZtoOrderSearchRequest : ZtoRequest
    {
        /// <summary>
        /// 0，预约件 1，全网件
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 运单编号
        /// </summary>
        public string billCode { get; set; }

        public override string GetServiceUrl()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://japi.zto.com/zto.open.getOrderInfo";
            }
            return "https://japi-test.zto.com/zto.open.getOrderInfo";
        }
    }
}
