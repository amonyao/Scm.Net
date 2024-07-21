using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JT.OrderSearch
{
    public class JtOrderSearchRequest : JtRequest
    {
        /// <summary>
        /// 客户编码（联系出货网点提供）
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 签名，Base64(Md5(客户编号+密文+privateKey))，其中密文：MD5(明文密码+jadada236t2) 后大写
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 查询命令 1.按客户订单编号查询   2.按运单编号查询（用运单号查订单状态）   3.按下单时间段查询（查订单）（按时间段查询返回不限制条数） 4.订单编号
        /// </summary>
        public int command { get; set; }
        /// <summary>
        /// command=1 客户订单编号查询 ;command=2 运单编号暂时;command=4订单号（公司内部）支持200个; Command=1、2或3时必填
        /// </summary>
        public List<string> serialNumber { get; set; }
        /// <summary>
        /// 开始时间(24小时制: yyyy-MM-dd HH:mm:ss)（command为3时必填）（客户下单时间）command = 3 时必填
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 结束时间(24小时制: yyyy-MM-dd HH:mm:ss)（command为3时必填），时间范围最大7天command = 3 时必填
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 状态（订单状态取下方状态表，筛选数据使用，默认全部）:已取消 104 ;;已取件 103 ;已调派业务员 102 ;已调派网点 101 ;未调派 100
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// command = 3 时必填
        /// </summary>
        public int current { get; set; }
        /// <summary>
        /// command = 3 时必填
        /// </summary>
        public int size { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/order/getOrders";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/order/getOrders?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }
    }
}
