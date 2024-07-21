using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.ZTO.OrderCancel
{
    public class ZtoOrderCancelRequest : ZtoRequest
    {
        /// <summary>
        /// 取消类型 1不想寄了,2下错单,3重复下单,4运费太贵,5无人联系,6取件太慢,7态度差
        /// </summary>
        public int cancelType { get; set; }
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
                return "https://japi.zto.com/zto.open.cancelPreOrder";
            }
            return "https://japi-test.zto.com/zto.open.cancelPreOrder";
        }
    }
}
