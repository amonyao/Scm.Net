using System.Collections.Generic;
using System.ComponentModel;
using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Exp.Model;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderFilter
{
    public class SfOrderFilterRequest : SfExpRequest
    {
        /// <summary>
        /// 筛单类别: 1:自动筛单(系统根据地址库进行判断,并返回结果,系统无法检索到可派送的将返回不可派送) 2:可人工筛单(系统首先根据地址库判断,如果无法自动判断是否收派,系统将生成需要人工判断的任务,后续由人工处理,处理结束后,顺丰可主动推送给客户系统)
        /// </summary>
        public FilterType filterType { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 月结卡号
        /// </summary>
        public string monthlyCard { get; set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        public List<ContactInfo> contactInfos { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://bspgw.sf-express.com/std/servic";
            }

            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_FILTER_ORDER_BSP";
        }
    }

    public enum FilterType
    {
        [Description("自动筛单")]
        Type1 = 1,
        [Description("可人工筛单")]
        Type2 = 2
    }
}
