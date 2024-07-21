using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.BEST.KD.OrderCreate
{
    public class BsKdOrderCreateRequesst : BsKdRequest
    {
        public double? itemWeight { get; set; }
        public string cusSendCode { get; set; }
        public BsKdOrderCreateItems items { get; set; }
        public BsKdOrderCreateReceiver receiver { get; set; }
        public BsKdOrderCreateSender sender { get; set; }
        public double? codSplitFee { get; set; }
        public double? buyServiceFee { get; set; }
        public double? totalServiceFee { get; set; }
        public string remark { get; set; }
        public int? special { get; set; }
        public double? insuranceValue { get; set; }
        public double? itemsValue { get; set; }
        public string recSite { get; set; }
        public double? goodsValue { get; set; }
        public string customerName { get; set; }
        public string customerId { get; set; }
        public string txLogisticID { get; set; }
        public string tradeNo { get; set; }
        public string mailNo { get; set; }
        public string orderType { get; set; }
        public string serviceType { get; set; }
        public string orderFlag { get; set; }
        public string sendStartTime { get; set; }
        public string sendEndTime { get; set; }

        public override string GetServiceType()
        {
            return "KD_CREATE_ORDER_NOTIFY";
        }
    }

    public class BsKdOrderCreateItems
    {
        public List<BsKdOrderCreateItem> item { get; set; }
    }

    public class BsKdOrderCreateItem
    {
        public string itemName { get; set; }
        public string number { get; set; }
        public double? itemValue { get; set; }
        public string remark { get; set; }
    }

    public class BsKdOrderCreateSender
    {
        public string city { get; set; }
        public string county { get; set; }
        public string prov { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string country { get; set; }
        public string postCode { get; set; }
        public string name { get; set; }
    }

    public class BsKdOrderCreateReceiver
    {
        public string address { get; set; }
        public string county { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string prov { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string postCode { get; set; }
        public string name { get; set; }
    }
}
