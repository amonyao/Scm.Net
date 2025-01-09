using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.BEST.KY.OrderCancel;
using Com.Scm.Express.My.Shipper.BEST.KY.OrderCreate;
using Com.Scm.Express.My.Shipper.BEST.KY.OrderFilter;
using Com.Scm.Express.My.Shipper.BEST.KY.OrderUpdate;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Http;
using Com.Scm.Utils;

namespace Com.Scm.Express.My.Shipper.BEST.KY
{
    public class BsKyExpress : BsExpress
    {

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        public override bool Init(MyConfig config)
        {
            return true;
        }

        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var bsRequest = new BsKyOrderCancelRequest();
            bsRequest.logisticID = order.code;
            bsRequest.logisticCompanyID = "BESTQJT";
            bsRequest.gmtCancel = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var bsResponse = Request<BsKyOrderCreateResponse>(bsRequest);
            if (bsResponse == null)
            {
                return response;
            }

            response.SetSuccess();
            return response;
        }

        public override OrderCreateResponse OrderCreateB2C(OrderCreateRequest request)
        {
            var response = new OrderCreateResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var bsRequest = new BsKyOrderCreateRequest();
            bsRequest.logisticID = order.code;
            bsRequest.logisticCompanyID = "BESTQJT";
            bsRequest.gmtCommit = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bsRequest.cargo = order.goods_desc;
            bsRequest.amount = "1.0";
            bsRequest.paymentTypeId = "30";
            bsRequest.serviceModeId = "57";

            var consigner = order.consigner;
            bsRequest.sendCompany = consigner.unit;
            bsRequest.sendPerson = consigner.contact;
            bsRequest.sendTelPhone = consigner.telephone;
            bsRequest.sendPhone = consigner.cellphone;
            var address = consigner.address;
            bsRequest.sendProvince = address.prov;
            bsRequest.sendCity = address.prov;
            bsRequest.sendCounty = address.city;
            bsRequest.sendAddress = address.area;

            var consignee = order.consignee;
            bsRequest.acceptCompany = consignee.unit;
            bsRequest.acceptPerson = consignee.contact;
            bsRequest.acceptTelPhone = consignee.telephone;
            bsRequest.acceptPhone = consignee.cellphone;
            address = consignee.address;
            bsRequest.acceptProvince = address.prov;
            bsRequest.acceptCity = address.city;
            bsRequest.acceptArea = address.area;
            bsRequest.acceptAddress = address.street;

            var bsResponse = Request<BsKyOrderCreateResponse>(bsRequest);
            if (bsResponse == null)
            {
                return response;
            }

            var result = new OrderCreateResult();
            result.code = order.code;
            result.mail_no = bsResponse.waybillcode;
            response.Append(result);

            response.SetSuccess();
            return response;
        }

        public override OrderCreateResponse OrderCreateC2C(OrderCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderFilterResponse OrderFilter(OrderFilterRequest request)
        {
            var response = new OrderFilterResponse();

            var order = request.Order;

            var address = order.consignee.address;

            var bsRequest = new BsKyOrderFilterRequest();
            bsRequest.address = address.prov + address.city + address.area + address.town + address.street;

            var bsResponse = Request<BsKyOrderFilterResponse>(bsRequest);
            if (bsResponse == null)
            {
                return response;
            }

            response.SetSuccess();
            return response;
        }

        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
        {
            var response = new OrderUpdateResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var bsRequest = new BsKyOrderUpdateRequest();
            bsRequest.logisticID = order.code;
            bsRequest.logisticCompanyID = "BESTQJT";
            bsRequest.updatedCommit = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bsRequest.cargo = order.goods_desc;
            bsRequest.amount = "1.0";
            bsRequest.paymentTypeId = "30";
            bsRequest.serviceModeId = "57";

            var consigner = order.consigner;
            bsRequest.sendCompany = consigner.unit;
            bsRequest.sendPerson = consigner.contact;
            bsRequest.sendTelPhone = consigner.telephone;
            bsRequest.sendPhone = consigner.cellphone;
            var address = consigner.address;
            bsRequest.sendProvince = address.prov;
            bsRequest.sendCity = address.prov;
            bsRequest.sendCounty = address.city;
            bsRequest.sendAddress = address.area;

            var consignee = order.consignee;
            bsRequest.acceptCompany = consignee.unit;
            bsRequest.acceptPerson = consignee.contact;
            bsRequest.acceptTelPhone = consignee.telephone;
            bsRequest.acceptPhone = consignee.cellphone;
            address = consignee.address;
            bsRequest.acceptProvince = address.prov;
            bsRequest.acceptCity = address.city;
            bsRequest.acceptArea = address.area;
            bsRequest.acceptAddress = address.street;

            var bsResponse = Request<BsKyOrderUpdateResponse>(bsRequest);
            if (bsResponse == null)
            {
                return response;
            }

            var result = new OrderUpdateResult();
            result.code = order.code;
            result.mail_no = bsResponse.logisticID;
            response.Append(result);

            response.SetSuccess();
            return response;
        }

        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public override bool IsCollect(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override bool IsReceipt(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override string GetServicePath()
        {
            return "http://openapi.800best.com/api-server/ky/api/process";
        }

        public override string GetPartnerId()
        {
            return "6091";
        }

        public override string GetPartnerKey()
        {
            return "ZX80NXNC";
        }

        protected T Request<T>(BsKyRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var request = new ScmHttpRequest();
            request.AddParameter("partnerID", GetPartnerId());
            request.AddParameter("serviceType", req.GetServiceType());
            request.AddParameter("bizData", json);
            request.AddParameter("sign", Digest(json));

            var client = new ScmHttpClient(GetServicePath());
            client.AddHeadParam("Content-type", "application/x-www-form-urlencoded;charset=UTF-8");

            var text = client.PostText(request);
            return text.AsJsonObject<T>();
        }
    }
}
