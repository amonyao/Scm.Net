using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.RouteSearch
{
    public class SfRouteSearchRequest : SfExpRequest
    {
        /// <summary>
        /// 返回描述语语言
        /// 0：中文 1：英文 2：繁体
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// 查询号类别:
        /// 1:根据顺丰运单号查询,trackingNumber将被当作顺丰运单号处理
        /// 2:根据客户订单号查询,trackingNumber将被当作客户订单号处理
        /// </summary>
        public string trackingType { get; set; }

        /// <summary>
        /// 查询号:
        /// trackingType=1,则此值为顺丰运单号
        /// 如果trackingType = 2, 则此值为客户订单号
        /// </summary>
        public List<string> trackingNumber { get; set; }

        /// <summary>
        /// 路由查询类别:
        /// 1:标准路由查询
        /// 2:定制路由查询
        /// </summary>
        public string methodType { get; set; }

        /// <summary>
        /// 参考编码(目前针对亚马逊客户,由客户传)
        /// </summary>
        public string referenceNumber { get; set; }

        /// <summary>
        /// 电话号码验证
        /// </summary>
        public string checkPhoneNo { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://bspgw.sf-express.com/std/service";
            }

            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_SEARCH_ROUTES";
        }
    }
}
