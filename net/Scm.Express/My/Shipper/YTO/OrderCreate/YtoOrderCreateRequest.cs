using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YTO.OrderCreate
{
    public class YtoOrderCreateRequest : YtoRequest
    {
        /// <summary>
        /// 物流单号，打印拉取运单号前，物流单号和渠道唯一确定一笔快递物流订单。注：最低长度为7
        /// </summary>
        public string logisticsNo { get; set; }
        /// <summary>
        /// 寄件人姓名
        /// </summary>
        public string senderName { get; set; }
        /// <summary>
        /// 寄件人省名称
        /// </summary>
        public string senderProvinceName { get; set; }
        /// <summary>
        /// 寄件人市名称
        /// </summary>
        public string senderCityName { get; set; }
        /// <summary>
        /// 寄件人区县名称
        /// </summary>
        public string senderCountyName { get; set; }
        /// <summary>
        /// 寄件人乡镇名称
        /// </summary>
        public string senderTownName { get; set; }
        /// <summary>
        /// 寄件人详细地址
        /// </summary>
        public string senderAddress { get; set; }
        /// <summary>
        /// 寄件人联系电话
        /// </summary>
        public string senderMobile { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string recipientName { get; set; }
        /// <summary>
        /// 收件人省名称
        /// </summary>
        public string recipientProvinceName { get; set; }
        /// <summary>
        /// 收件人市名称
        /// </summary>
        public string recipientCityName { get; set; }
        /// <summary>
        /// 收件人区县名称
        /// </summary>
        public string recipientCountyName { get; set; }
        /// <summary>
        /// 收件人乡镇名称
        /// </summary>
        public string recipientTownName { get; set; }
        /// <summary>
        /// 收件人详细地址
        /// </summary>
        public string recipientAddress { get; set; }
        /// <summary>
        /// 收件人联系电话
        /// </summary>
        public string recipientMobile { get; set; }
        /// <summary>
        /// 备注，打印在面单上的备注内容
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 寄件码或取件码
        /// </summary>
        public string gotCode { get; set; }
        /// <summary>
        /// 增值服务列表
        /// </summary>
        public List<OrderIncrementDto> increments { get; set; }
        /// <summary>
        /// 物品列表，注：支持20个以内
        /// </summary>
        public List<OrderGoodsDto> goods { get; set; }
        /// <summary>
        /// 预约上门取件开始时间，”yyyy-MM-dd HH:mm:ss”格式化，注：预约取件时间规则为下单当天的00:00:00至下单当天+6天的23:59:59。
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 预约上门取件结束时间，”yyyy-MM-dd HH:mm:ss”格式化，注：预约取件时间规则为下单当天的00:00:00至下单当天+6天的23:59:59。
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 客户业务类型，可以对客户订单进行业务或渠道区分。
        public string cstBusinessType { get; set; }
        /// <summary>
        /// 客户的订单号
        /// </summary>
        public string cstOrderNo { get; set; }
        /// <summary>
        /// 下单总重量，单位：千克，小数后三位	
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 实名制id
        /// </summary>
        public string realNameId { get; set; }
        /// <summary>
        /// 产品编号：YZD-圆准达；PK-普通普快，默认PK
        /// </summary>
        public string productCode { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "https://openuat.yto56.com.cn:6443/open/privacy_create_adapter/v1/PFUYji/K21000119";
        }

        public override string GetServiceCode()
        {
            return "privacy_create_adapter";
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
            return "K21000119";
        }

        public override string GetPartnerPass()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "u2Z1F7Fh";
        }
    }

    public class OrderIncrementDto
    {
        /// <summary>
        /// 增值类型：1-代收货款；2-到付；4-保价，注：增值服务不能同时选择代收和到付。
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 金额，单位：元，代收货款金额：[3,20000]；到付金额：[1,5000]；保价金额：[100,30000]。
        /// </summary>
        public string amount { get; set; }
    }

    public class OrderGoodsDto
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 	重量，单位：千克
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 长，单位：米
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 宽，单位：米
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 高，单位：米
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 单价，单位：元
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int quantity { get; set; }
    }
}
