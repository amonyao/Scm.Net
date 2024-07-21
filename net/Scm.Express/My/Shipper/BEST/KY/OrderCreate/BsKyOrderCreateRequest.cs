using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.BEST.KY.OrderCreate
{
    public class BsKyOrderCreateRequest : BsKyRequest
    {
        public string bizType { get; set; }
        public string checkOrderId { get; set; }
        public string podFee { get; set; }
        public string pack { get; set; }
        public string packageServicePrice { get; set; }
        public bool notifyDelivery { get; set; }
        public string notifyDeliveryFee { get; set; }
        public bool smsNotify { get; set; }
        public string smsNotifyPrice { get; set; }
        public bool fuelSurcharge { get; set; }
        public string fuelSurchargePrice { get; set; }
        public string otherFees { get; set; }
        public string reMark { get; set; }
        public int? memberType { get; set; }
        public bool waybillFlag { get; set; }
        public string logisticID { get; set; }
        public string code { get; set; }
        public string sendSiteCode { get; set; }
        public string productID { get; set; }
        public string insuranceTypeID { get; set; }
        public string largeService { get; set; }
        public string largeServicePrice { get; set; }
        public string entryService { get; set; }
        public string entryServicePrice { get; set; }
        public string upstairsService { get; set; }
        public string upstairsServicePrice { get; set; }
        public string acceptSiteCode { get; set; }
        public PackageInfoList packageInfoList { get; set; }
        public string acceptCompany { get; set; }
        public string sendTelPhone { get; set; }
        public string sendProvince { get; set; }
        public string sendCity { get; set; }
        public string sendCounty { get; set; }
        public string sendAddress { get; set; }
        public string acceptProvince { get; set; }
        public string acceptCity { get; set; }
        public string acceptArea { get; set; }
        public string acceptAddress { get; set; }
        public string sendPhone { get; set; }
        public string sendPerson { get; set; }
        public string sendCompany { get; set; }
        public string logisticCompanyID { get; set; }
        public string insurance { get; set; }
        public string acceptPerson { get; set; }
        public string acceptPhone { get; set; }
        public string acceptTelPhone { get; set; }
        public string gmtCommit { get; set; }
        public string cargo { get; set; }
        public string amount { get; set; }
        public string actualWeight { get; set; }
        public string cubic { get; set; }
        public string totalPrice { get; set; }
        public string paymentTypeId { get; set; }
        public string serviceModeId { get; set; }
        public string deliveryPrice { get; set; }
        public string insureAmount { get; set; }

        public override string GetServiceType()
        {
            return "KY_CREATE_ORDER_NOTIFY";
        }
    }

    public class PackageInfoList
    {
        public List<PackageInfo> packageInfo { get; set; }
    }

    public class PackageInfo
    {
        public string packageID { get; set; }
        public string packageItem { get; set; }
    }
}
