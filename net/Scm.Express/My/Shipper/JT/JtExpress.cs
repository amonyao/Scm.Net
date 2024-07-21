using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.JT.Dto;
using Com.Scm.Express.My.Shipper.JT.OrderCancel;
using Com.Scm.Express.My.Shipper.JT.OrderCreateB2C;
using Com.Scm.Express.My.Shipper.JT.OrderCreateC2C;
using Com.Scm.Express.My.Shipper.JT.OrderFilter;
using Com.Scm.Express.My.Shipper.JT.OrderSearch;
using Com.Scm.Express.My.Shipper.JT.RouteSearch;
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

namespace Com.Scm.Express.My.Shipper.JT
{
    public class JtExpress : MyExpress
    {
        private const string API_ACCOUNT = "462512470972440615";
        private const string PRIVATE_KEY = "045d7ac28fe14e928f7e5633df151b8a";

        private JtConfig _Config;

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        public override bool Init(MyConfig config)
        {
            if (_Config == null || !(config is JtConfig))
            {
                _Config = new JtConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (JtConfig)config;
            }

            return true;
        }

        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var jtRequest = new JtOrderCancelRequest();
            jtRequest.customerCode = _Config.customerCode;
            jtRequest.digest = RequestDigest();
            jtRequest.orderType = "2";
            jtRequest.txlogisticId = order.code;
            jtRequest.reason = request.CancelReason ?? "客户取消";

            var jtResponse = Request<JtOrderCancelResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override OrderCreateResponse OrderCreateB2C(OrderCreateRequest request)
        {
            var response = new OrderCreateResponse();

            var order = request.Orders[0];

            var jtRequest = new JtOrderCreateB2CRequest();
            jtRequest.customerCode = _Config.customerCode;
            jtRequest.digest = RequestDigest();
            jtRequest.network = _Config.network;
            jtRequest.txlogisticId = order.code;
            jtRequest.expressType = _Config.expressType;
            jtRequest.orderType = "2";
            jtRequest.serviceType = "01";
            jtRequest.deliveryType = "03";
            jtRequest.payType = "PP_PM";

            var consigner = order.consigner;
            var sender = new JtContact();
            sender.name = consigner.contact;
            sender.company = consigner.unit;
            sender.countryCode = _Config.countryCode;
            sender.mobile = consigner.cellphone;
            sender.phone = consigner.telephone;
            var address = consigner.address;
            sender.countryCode = "CHN";
            sender.prov = address.prov;
            sender.city = address.city;
            sender.area = address.area;
            sender.town = address.town;
            sender.street = address.street;
            sender.address = address.prov + address.city + address.area + address.town + address.street;
            sender.postCode = address.postcode;
            jtRequest.sender = sender;

            var consignee = order.consignee;
            var receiver = new JtContact();
            receiver.name = consignee.contact;
            receiver.company = consignee.unit;
            receiver.countryCode = _Config.countryCode;
            receiver.mobile = consignee.cellphone;
            receiver.phone = consignee.telephone;
            address = consignee.address;
            receiver.countryCode = "CHN";
            receiver.prov = address.prov;
            receiver.city = address.city;
            receiver.area = address.area;
            receiver.town = address.town;
            receiver.street = address.street;
            receiver.address = address.prov + address.city + address.area + address.town + address.street;
            receiver.postCode = address.postcode;
            jtRequest.receiver = receiver;

            jtRequest.goodsType = _Config.goodsType;
            jtRequest.weight = "1";

            var jtResponse = Request<JtOrderCreateB2CResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override OrderCreateResponse OrderCreateC2C(OrderCreateRequest request)
        {
            var response = new OrderCreateResponse();

            var order = request.Orders[0];

            var jtRequest = new JtOrderCreateC2CRequest();
            jtRequest.digest = RequestDigest();
            jtRequest.txlogisticId = order.code;
            jtRequest.expressType = _Config.expressType;
            jtRequest.orderType = "1";
            jtRequest.serviceType = "01";
            jtRequest.deliveryType = "03";
            jtRequest.payType = "PP_CASH";

            var consigner = order.consigner;
            var sender = new JtContact();
            sender.name = consigner.contact;
            sender.company = consigner.unit;
            sender.countryCode = _Config.countryCode;
            sender.mobile = consigner.cellphone;
            sender.phone = consigner.telephone;
            var address = consigner.address;
            sender.countryCode = "CHN";
            sender.prov = address.prov;
            sender.city = address.city;
            sender.area = address.area;
            sender.town = address.town;
            sender.street = address.street;
            sender.address = address.prov + address.city + address.area + address.town + address.street;
            sender.postCode = address.postcode;
            jtRequest.sender = sender;

            var consignee = order.consignee;
            var receiver = new JtContact();
            receiver.name = consignee.contact;
            receiver.company = consignee.unit;
            receiver.countryCode = _Config.countryCode;
            receiver.mobile = consignee.cellphone;
            receiver.phone = consignee.telephone;
            address = consignee.address;
            receiver.countryCode = "CHN";
            receiver.prov = address.prov;
            receiver.city = address.city;
            receiver.area = address.area;
            receiver.town = address.town;
            receiver.street = address.street;
            receiver.address = address.prov + address.city + address.area + address.town + address.street;
            receiver.postCode = address.postcode;
            jtRequest.receiver = receiver;

            jtRequest.goodsType = _Config.goodsType;
            jtRequest.weight = "1";
            jtRequest.totalQuantity = 1;

            var jtResponse = Request<JtOrderCreateC2CResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override OrderFilterResponse OrderFilter(OrderFilterRequest request)
        {
            var response = new OrderFilterResponse();

            var order = request.Order;
            var jtRequest = new JtOrderFilterRequest();
            var consigner = order.consigner;
            var address = consigner.address;
            jtRequest.senderProvName = address.prov;
            jtRequest.senderCityName = address.city;
            jtRequest.senderAreaName = address.area;
            jtRequest.senderAddress = address.street;

            var consignee = order.consignee;
            address = consigner.address;
            jtRequest.receiverProvName = address.prov;
            jtRequest.receiverCityName = address.city;
            jtRequest.receiverAreaName = address.area;
            jtRequest.receiverAddress = address.street;

            var jtResponse = Request<JtOrderFilterResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            var response = new OrderSearchResponse();

            var list = new List<string>();
            foreach (var order in request.Orders)
            {
                list.Add(order.mail_no);
            }

            var jtRequest = new JtOrderSearchRequest();
            jtRequest.customerCode = _Config.customerCode;
            jtRequest.command = 2;
            jtRequest.serialNumber = list;
            jtRequest.digest = RequestDigest();

            var jtResponse = Request<JtOrderSearchResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest order)
        {
            throw new NotImplementedException();
        }

        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            var response = new RouteSearchResponse();

            var jtRequest = new JtRouteSearchRequest();
            var mailNo = "";
            foreach (var order in request.Orders)
            {
                mailNo += "," + order.mail_no;
            }
            if (mailNo.Length > 1)
            {
                mailNo = mailNo.Substring(1);
            }
            jtRequest.billCodes = mailNo;

            var jtResponse = Request<JtRouteSearchResponse>(jtRequest);
            if (jtResponse == null)
            {
                return response;
            }
            if (!jtResponse.IsSuccess())
            {
                response.SetFailure(jtResponse.GetMessage());
                return response;
            }

            jtResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override bool IsCollect(OrderHeader order)
        {
            var jtRequest = new JtRouteSearchRequest();
            jtRequest.billCodes = order.mail_no;

            var jtResponse = Request<JtRouteSearchResponse>(jtRequest);
            if (jtResponse == null)
            {
                return false;
            }
            if (!jtResponse.IsSuccess())
            {
                return false;
            }

            return true;
        }

        public override bool IsReceipt(OrderHeader order)
        {
            var jtRequest = new JtRouteSearchRequest();
            jtRequest.billCodes = order.mail_no;

            var jtResponse = Request<JtRouteSearchResponse>(jtRequest);
            if (jtResponse == null)
            {
                return false;
            }
            if (!jtResponse.IsSuccess())
            {
                return false;
            }

            return true;
        }

        public string GetApiAccount()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return API_ACCOUNT;
            }
            return "178337126125932605";
        }

        public string GetPrivateKey()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return PRIVATE_KEY;
            }
            return "0258d71b55fc45e3ad7a7f38bf4b201a";
        }

        protected T Request<T>(JtRequest req) where T : class, new()
        {
            var now = DateTime.Now;
            var timestamp = Timestamp(now);
            var json = req.ToJsonString();

            var request = new HttpRequest();
            request.AddParameter("bizContent", json);

            var client = new RestClient(req.GetPath());
            client.AddDefaultHeader("Content-type", "application/x-www-form-urlencoded;charset=utf-8");
            client.AddDefaultHeader("apiAccount", GetApiAccount());
            client.AddDefaultHeader("digest", Digest(json));
            client.AddDefaultHeader("timestamp", timestamp.ToString());

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }

        private HashAlgorithm _Md5;
        private byte[] Md5(string text)
        {
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return _Md5.ComputeHash(bytes);
        }

        private string RequestDigest()
        {
            var secret = TextUtils.ToHexString(Md5("H5CD3zE6" + "jadada236t2"), true);
            var tmp = _Config.customerCode + secret + GetPrivateKey();
            return Convert.ToBase64String(Md5(tmp));
        }

        public string Digest(string json)
        {
            string text = json + GetPrivateKey();
            var bytes = Md5(text);
            return Convert.ToBase64String(bytes);
        }

        public static long Timestamp(DateTime time)
        {
            return (long)(time - _UnixTime).TotalMilliseconds;
        }
    }
}
