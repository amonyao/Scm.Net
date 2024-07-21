using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.BEST.KD.OrderCreate;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Http;
using Com.Scm.Utils;

namespace Com.Scm.Express.My.Shipper.BEST.KD
{
    public class BsKdExpress : BsExpress
    {
        public const string URL_DEV = "http://kdtest.800best.com/kd/api/process";

        public override string GetServicePath()
        {
            return "http://openapi.800best.com/api-server/kd/api/process";
        }

        public override string GetPartnerId()
        {
            return "1843";
        }

        public override string GetPartnerKey()
        {
            return "NQZVCA0U";
        }

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
            throw new NotImplementedException();
        }

        public override OrderCreateResponse OrderCreateB2C(OrderCreateRequest request)
        {
            var response = new OrderCreateResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var bsRequest = new BsKdOrderCreateRequesst();
            bsRequest.txLogisticID = order.code;
            bsRequest.serviceType = "";
            bsRequest.orderType = "";
            bsRequest.orderFlag = "";

            var consigner = order.consigner;
            var sender = new BsKdOrderCreateSender();
            var address = consigner.address;
            sender.country = address.country;
            sender.prov = address.prov;
            sender.city = address.city;
            sender.county = address.area;
            sender.address = address.street;
            sender.postCode = address.postcode;
            sender.name = consigner.contact;
            sender.phone = consigner.telephone;
            sender.mobile = consigner.cellphone;
            bsRequest.sender = sender;

            var consignee = order.consignee;
            var receiver = new BsKdOrderCreateReceiver();
            address = consignee.address;
            receiver.country = address.country;
            receiver.prov = address.prov;
            receiver.city = address.city;
            receiver.county = address.area;
            receiver.address = address.street;
            receiver.postCode = address.postcode;
            receiver.name = consignee.contact;
            receiver.phone = consignee.telephone;
            receiver.mobile = consignee.cellphone;
            bsRequest.receiver = receiver;

            var bsResponse = Request<BsKdOrderCreateResponse>(bsRequest);
            if (bsResponse == null)
            {
                return response;
            }

            var result = new OrderCreateResult();
            result.code = order.code;
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
            throw new NotImplementedException();
        }

        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
        {
            throw new NotImplementedException();
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

        protected T Request<T>(BsKdRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var request = new HttpRequest();
            request.AddParameter("partnerID", GetPartnerId());
            request.AddParameter("serviceType", req.GetServiceType());
            request.AddParameter("bizData", json);
            request.AddParameter("sign", Digest(json));

            var client = new RestClient(GetServicePath());
            client.AddDefaultHeader("Content-type", "application/x-www-form-urlencoded;charset=UTF-8");

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }
    }
}
