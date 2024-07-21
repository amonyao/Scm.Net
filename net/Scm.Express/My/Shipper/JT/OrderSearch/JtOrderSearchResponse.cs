using Com.Scm.Express.My.Shipper.JT.Dto;
using Com.Scm.Express.OrderSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JT.OrderSearch
{
    public class JtOrderSearchResponse : JtResponse<List<JtOrderSearchResult>>
    {
        public void ToResponse(OrderSearchResponse response)
        {

        }
    }

    public class JtOrderSearchResult
    {
        ///<summary>
        ///  集包地
        ///</summary>
        public string lastCenterName { get; set; }
        ///<summary>
        ///  系统订单号
        ///</summary>
        public string orderNumber { get; set; }
        ///<summary>
        ///  客户订单号    客户自己系统的订单号
        ///</summary>
        public string txlogisticId { get; set; }
        ///<summary>
        ///  运单号
        ///</summary>
        public string billCode { get; set; }
        ///<summary>
        ///  快件类型 标准快递EZ
        ///</summary>
        public string expressType { get; set; }
        ///<summary>
        ///  订单状态 ：已取消 104 ；已取件 103 ；已调派业务员 102 ；已调派网点 101 ；未调派 100
        ///</summary>
        public string orderStatus { get; set; }
        ///<summary>
        ///  订单类型  ：01散客 02月结
        ///</summary>
        public string orderType { get; set; }
        ///<summary>
        ///  服务类型  02 门店寄件 01 上门取件
        ///</summary>
        public string serviceType { get; set; }
        ///<summary>
        ///  派送类型 : 06 代收点自提; 05 快递柜自提; 04 站点自提; 03 派送上门
        ///</summary>
        public string deliveryType { get; set; }
        ///<summary>
        ///  三段码
        ///</summary>
        public string sortingCode { get; set; }
        ///<summary>
        ///  参考总运费（数值型）
        ///</summary>
        public string sumFreight { get; set; }
        ///<summary>
        ///  寄件信息对象
        ///</summary>
        public JtContact sender { get; set; }
        ///<summary>
        ///  收件信息对象
        ///</summary>
        public JtContact receiver { get; set; }
        ///<summary>
        ///  物流公司上门取货开始时间 yyyy-MM-dd HH:mm:ss
        ///</summary>
        public string sendStartTime { get; set; }
        ///<summary>
        ///  客户物流公司上门取货结束时间 yyyy-MM-dd HH:mm:ss编号
        ///</summary>
        public string sendEndTime { get; set; }
        ///<summary>
        ///  长，cm编号
        ///</summary>
        public int length { get; set; }
        ///<summary>
        ///  宽，cm
        ///</summary>
        public int width { get; set; }
        ///<summary>
        ///  高，cm
        ///</summary>
        public int height { get; set; }
        ///<summary>
        ///  体积
        ///</summary>
        public int volume { get; set; }
        ///<summary>
        ///  重量（不能为负数）
        ///</summary>
        public int weight { get; set; }
        ///<summary>
        ///  包裹总件数
        ///</summary>
        public int totalQuantity { get; set; }
        ///<summary>
        ///  代收货款金额 (数值型)
        ///</summary>
        public string itemsValue { get; set; }
        ///<summary>
        ///  代收货款币别（默认本国币别，如：RMB）
        ///</summary>
        public string priceCurrency { get; set; }
        ///<summary>
        ///  保价金额(数值型)，单位：元
        ///</summary>
        public string offerFee { get; set; }
        ///<summary>
        ///  备注
        ///</summary>
        public string remark { get; set; }
        ///<summary>
        ///  商品信息列表
        ///</summary>
        public List<JtCargoItem> items { get; set; }
        ///<summary>
        ///  订单创建时间 yyyy-MM-dd HH:mm:ss
        ///</summary>
        public string createOrderTime { get; set; }
    }
}
