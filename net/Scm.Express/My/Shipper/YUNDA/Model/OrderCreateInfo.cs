using Com.Scm.Express.My.Shipper.YUNDA.OrderCreateB2C;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.Model
{
    public class OrderCreateInfo
    {
        /// <summary>
        /// 订单唯一序列号 由字母、数字、下划线组成，必须保证唯一，请对特殊符号进行过滤
        /// </summary>
        public string order_serial_no { get; set; }
        /// <summary>
        /// 大客户系统订单的订单号可与订单唯一序列号相同
        /// </summary>
        public string khddh { get; set; }
        /// <summary>
        /// 寄件人信息
        /// </summary>
        public ContactInfo sender { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public ContactInfo receiver { get; set; }
        /// <summary>
        /// 物品重量
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 货物大小（米），用半角的逗号来分隔长宽高
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 货物金额
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 揽件网点编码
        /// </summary>
        public string collect_branch { get; set; }
        /// <summary>
        /// 代收货款金额 目前仅用于cod订单，cod订单必填
        /// </summary>
        public string collection_value { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public string other_charges { get; set; }
        /// <summary>
        /// 商品信息集合
        /// </summary>
        public List<CargoInfo> items { get; set; }
        /// <summary>
        /// 商品类型(见商品类型字段)
        /// </summary>
        public string special { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 可以自定义显示信息1，打印在客户自定义区域1，换行请用\n
        /// </summary>
        public string cus_area1 { get; set; }
        /// <summary>
        /// 可以自定义显示信息2，打印在客户自定义区域2，换行请用\n
        /// </summary>
        public string cus_area2 { get; set; }
        /// <summary>
        /// 客户波次号，按照此号进行批量打印校验，批量下单时，此波次号需要相同
        /// </summary>
        public string wave_no { get; set; }
        /// <summary>
        /// 运单类型参数值对照order_type字典表可固定为common
        /// </summary>
        public string order_type { get; set; }
        /// <summary>
        /// 判断订单是否是隐私订单，如果是隐私订单把收件人姓名和电话进行脱敏处理（1表示是，0表示否）隐私面单业务对接需与韵达方确认业务需求
        /// </summary>
        public string isProtectPrivacy { get; set; }
        /// <summary>
        /// 平台来源
        /// </summary>
        public string platform_source { get; set; }
        /// <summary>
        /// 350（默认）
        /// </summary>
        public string node_id { get; set; }
        /// <summary>
        /// 一票多件
        /// </summary>
        public MultiPackInfo multi_pack { get; set; }
        /// <summary>
        /// 增值服务标签增值服务必填，json字符串
        /// </summary>
        public string markingInfos { get; set; }
    }
}
