using Com.Scm.Express.My.Shipper.SF.Exp.Model;
using Com.Scm.Express.OrderCreate;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderCreate
{
    public class SfOrderCreateResponse
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 原寄地区域代码，可用于顺丰 电子运单标签打印
        /// </summary>
        public string originCode { get; set; }
        /// <summary>
        /// 目的地区域代码，可用于顺丰 电子运单标签打印
        /// </summary>
        public string destCode { get; set; }
        /// <summary>
        /// 筛单结果： 1：人工确认 2：可收派 3：不可以收派
        /// </summary>
        public string filterResult { get; set; }
        /// <summary>
        /// 如果filter_result=3时为必填， 不可以收派的原因代码： 1：收方超范围 2：派方超范围 3：其它原因 高峰管控提示信息 【数字】：【高峰管控提示信息】 (如 4：温馨提示 ，1：春运延时)
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 二维码URL （用于CX退货操作的URL）
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 用于第三方支付运费的URL
        /// </summary>
        public string paymentLink { get; set; }
        /// <summary>
        /// 是否送货上楼 1:是
        /// </summary>
        public string isUpstairs { get; set; }
        /// <summary>
        /// true 包含特殊仓库增值服务
        /// </summary>
        public string isSpecialWarehouseService { get; set; }
        /// <summary>
        /// 下单补充的增值服务信息
        /// </summary>
        public List<ServiceInfo> serviceList { get; set; }
        /// <summary>
        /// 返回信息扩展属性
        /// </summary>
        public List<ExtraInfo> returnExtraInfoList { get; set; }
        /// <summary>
        /// 顺丰运单号
        /// </summary>  
        public List<WaybillNoInfo> waybillNoInfoList { get; set; }
        /// <summary>
        /// 路由标签，除少量特殊场景用户外，其余均会返回
        /// </summary>
        public List<RouteLabelInfo> routeLabelInfo { get; set; }

        public void ToResponse(OrderCreateResponse response)
        {
            if (waybillNoInfoList != null)
            {
                foreach (var info in waybillNoInfoList)
                {
                    var result = new OrderCreateResult();
                    result.code = orderId;
                    result.mail_no = info.waybillNo;
                    response.Append(result);
                }
            }
        }
    }
}
