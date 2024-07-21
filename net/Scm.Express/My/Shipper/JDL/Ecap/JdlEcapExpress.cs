using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCancel;
using Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCreate;
using Com.Scm.Express.My.Shipper.JDL.Ecap.RouteSearch;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.OAuth.Jd;
using Com.Scm.Utils;

namespace Com.Scm.Express.My.Shipper.JDL.Ecap
{
    public class JdlEcapExpress : JdlExpress
    {
        private JdlEcapConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is JdlEcapConfig))
            {
                _Config = new JdlEcapConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (JdlEcapConfig)config;
            }

            _JdClient = new JdClient2();
            _JdClient.GetAuth();

            return true;
        }

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var jdlRequest = new JdlEcapOrderCancelRequest();
            jdlRequest.orderCode = order.code;
            jdlRequest.waybillCode = order.mail_no;
            jdlRequest.orderOrigin = _Config.orderOrigin;
            jdlRequest.customerCode = "";
            jdlRequest.cancelReason = request.CancelReason ?? "用户取消";
            jdlRequest.cancelReasonCode = "1";
            jdlRequest.cancelType = _Config.orderOrigin == 0 ? 0 : 1;

            var json = jdlRequest.ToJsonString();
            json = "[" + json + "]";
            var jdlResponse = Request<JdlEcapOrderCancelResponse>(jdlRequest, json);
            if (jdlResponse == null)
            {
                return response;
            }
            if (!jdlResponse.IsSuccess())
            {
                response.SetFailure(jdlResponse.GetMessage());
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

            var jdlRequest = new JdlEcapOrderCreateRequest();
            jdlRequest.orderId = order.code;
            jdlRequest.orderOrigin = _Config.orderOrigin;
            jdlRequest.settleType = _Config.settleType;
            jdlRequest.pickupType = _Config.pickupType;

            var sender = new JdlEcapContact();
            jdlRequest.senderContact = sender;
            sender.name = order.consigner.contact;
            sender.mobile = order.consigner.cellphone;
            var address = order.consigner.address;
            sender.fullAddress = address.prov + address.city + address.area + address.town + address.street;

            var receiver = new JdlEcapContact();
            jdlRequest.receiverContact = receiver;
            receiver.name = order.consignee.contact;
            receiver.mobile = order.consignee.cellphone;
            address = order.consignee.address;
            receiver.fullAddress = address.prov + address.city + address.area + address.town + address.street;

            var product = new JdlEcapProductInfo();
            product.productCode = _Config.productCode;
            jdlRequest.productsReq = product;

            var cargos = new List<JdlEcapCargoInfo>();
            var cargo = new JdlEcapCargoInfo();
            cargo.name = "文件";
            cargo.quantity = 1;
            cargo.weight = 1;
            cargo.volume = 1;
            cargos.Add(cargo);
            jdlRequest.cargoes = cargos;

            var json = jdlRequest.ToJsonString();
            json = "[" + json + "]";

            var jdlResponse = Request<JdlEcapOrderCreateResponse>(jdlRequest, json);
            if (jdlResponse == null)
            {
                return response;
            }
            if (!jdlResponse.IsSuccess())
            {
                response.SetFailure(jdlResponse.GetMessage());
                return response;
            }

            var data = jdlResponse.data;
            if (data != null)
            {
                var result = new OrderCreateResult();
                result.code = order.code;
                result.bill_no = data.orderCode;
                result.mail_no = data.waybillCode;
                response.Append(result);
            }

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

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest order)
        {
            throw new NotImplementedException();
        }

        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            var response = new RouteSearchResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];
            var jdlRequest = new JdlEcapRouteSearchRequest();
            jdlRequest.waybillCode = order.mail_no;
            jdlRequest.orderCode = order.code;
            jdlRequest.orderOrigin = 0;

            var json = jdlRequest.ToJsonString();
            json = "[" + json + "]";
            var jdlResponse = Request<JdlEcapRouteSearchResponse>(jdlRequest, json);
            if (jdlResponse == null)
            {
                return response;
            }
            if (!jdlResponse.IsSuccess())
            {
                response.SetFailure(jdlResponse.GetMessage());
                return response;
            }

            response.SetSuccess();
            return response;
        }

        public override bool IsCollect(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override bool IsReceipt(OrderHeader order)
        {
            throw new NotImplementedException();
        }
    }
}
