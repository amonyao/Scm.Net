using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.ZTO.OrderCancel;
using Com.Scm.Express.My.Shipper.ZTO.OrderCreate;
using Com.Scm.Express.My.Shipper.ZTO.OrderSearch;
using Com.Scm.Express.My.Shipper.ZTO.RouteSearch.V2;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Http;
using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.My.Shipper.ZTO
{
    public class ZtoExpress : MyExpress
    {
        public const string APP_KEY = "064a61a37a3eb6bc8aa99";
        public const string APP_SECRET = "1f5e9edf174b8cb7bed5757b486e65ac";

        public const string APP_KEY_DEV = "d577e7b5024ad20446e10";
        public const string APP_SECRET_DEV = "0e8e9457d493666ee2f5adb783e69abb";

        private ZtoConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is ZtoConfig))
            {
                _Config = new ZtoConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (ZtoConfig)config;
            }

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

            var ztoRequest = new ZtoOrderCancelRequest();
            ztoRequest.billCode = order.mail_no;

            var ztoResponse = Request<ZtoOrderCancelResponse>(ztoRequest);
            if (ztoResponse == null || !ztoResponse.IsSuccess())
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

            var ztoRequest = new ZtoOrderCreateRequest();
            ztoRequest.partnerType = "2";
            ztoRequest.orderType = "1";
            ztoRequest.partnerOrderCode = order.code;
            ztoRequest.billCode = order.mail_no;

            var consigner = order.consigner;
            var sender = new SenderInfoInput();
            ztoRequest.senderInfo = sender;
            sender.senderName = consigner.contact;
            sender.senderMobile = consigner.cellphone;
            sender.senderPhone = consigner.telephone;

            var address = consigner.address;
            sender.senderProvince = address.prov;
            sender.senderCity = address.city;
            sender.senderDistrict = address.area;
            sender.senderAddress = address.street;

            var consignee = order.consignee;
            var receive = new ReceiveInfoInput();
            ztoRequest.receiveInfo = receive;
            receive.receiverName = consignee.contact;
            receive.receiverMobile = consignee.cellphone;
            receive.receiverPhone = consignee.telephone;

            address = consignee.address;
            receive.receiverProvince = address.prov;
            receive.receiverCity = address.city;
            receive.receiverDistrict = address.area;
            receive.receiverAddress = address.street;

            // OrderItem;

            var ztoResponse = Request<ZtoOrderCreateResponse>(ztoRequest);
            if (ztoResponse == null)
            {
                response.SetFailure("中通响应异常！");
                return response;
            }
            if (!ztoResponse.IsSuccess())
            {
                response.SetFailure(ztoResponse.GetMessage());
                return response;
            }

            ztoResponse.ToResponse(response);
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
            var response = new OrderSearchResponse();

            var order = request.Orders[0];

            var ztoRequest = new ZtoOrderSearchRequest();
            ztoRequest.billCode = order.mail_no;

            var ztoResponse = Request<ZtoOrderSearchResponse>(ztoRequest);
            if (ztoResponse == null)
            {
                response.SetFailure("中通响应异常！");
                return response;
            }
            if (!ztoResponse.IsSuccess())
            {
                response.SetFailure(ztoResponse.GetMessage());
                return response;
            }

            response.SetSuccess();
            return response;
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
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

            var ztoRequest = new ZtoRouteSearchRequest();
            ztoRequest.billCode = order.mail_no;
            var phone = order.consigner?.cellphone;
            if (phone == null)
            {
                phone = order.consignee?.cellphone;
            }
            ztoRequest.mobilePhone = phone;

            var ztoResponse = Request<ZtoRouteSearchResponse>(ztoRequest);
            if (ztoResponse == null)
            {
                response.SetFailure("中通响应异常！");
                return response;
            }
            if (!ztoResponse.IsSuccess())
            {
                response.SetFailure(ztoResponse.GetMessage());
                return response;
            }

            ztoResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override bool IsCollect(OrderHeader order)
        {
            var ztoRequest = new ZtoRouteSearchRequest();
            ztoRequest.billCode = order.mail_no;

            var ztoResponse = Request<ZtoRouteSearchResponse>(ztoRequest);
            if (ztoResponse == null)
            {
                return false;
            }
            if (!ztoResponse.IsSuccess())
            {
                return false;
            }

            return true;
        }

        public override bool IsReceipt(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        #region 公共方法
        private T Request<T>(ZtoRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var client = new RestClient(req.GetServiceUrl());
            client.AddDefaultHeader("Content-type", "application/json");
            client.AddDefaultHeader("x-appKey", GetAppKey());
            client.AddDefaultHeader("x-datadigest", Digest(json));

            var request = new HttpRequest();
            request.AddBody(json);

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }

        private static HashAlgorithm _Md5;
        public static string Digest(string json)
        {
            string text = json + GetAppSecret();

            byte[] buytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            buytes = _Md5.ComputeHash(buytes);
            return Convert.ToBase64String(buytes);
        }

        protected static string GetAppKey()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_KEY;
            }

            return APP_KEY_DEV;
        }

        protected static string GetAppSecret()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_SECRET;
            }

            return APP_SECRET_DEV;
        }

        public long Timestamp(DateTime time)
        {
            return (long)(time - _UnixTime).TotalMilliseconds;
        }
        #endregion
    }
}
