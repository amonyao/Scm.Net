using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.YUNDA.Model;
using Com.Scm.Express.My.Shipper.YUNDA.OrderCancel;
using Com.Scm.Express.My.Shipper.YUNDA.OrderCreateB2C;
using Com.Scm.Express.My.Shipper.YUNDA.OrderFilter;
using Com.Scm.Express.My.Shipper.YUNDA.OrderUpdate;
using Com.Scm.Express.My.Shipper.YUNDA.RouteSearch;
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

namespace Com.Scm.Express.My.Shipper.YUNDA
{
    public class YdExpress : MyExpress
    {
        public const string APP_KEY = "000304";
        public const string APP_SECRET = "ca39d98bbd0b11eab6fe27157fa860a7";

        public const string APP_KEY_DEV = "999999";
        public const string APP_SECRET_DEV = "04d4ad40eeec11e9bad2d962f53dda9d";

        public const string SERVICE_ROOT = "https://openapi.yundaex.com/";
        public const string SERVICE_ROOT_DEV = "https://devkyweixin.yundasys.com/";

        private YdConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is YdConfig))
            {
                _Config = new YdConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (YdConfig)config;
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

            var ydRequest = new YdOrderCancelRequest();
            ydRequest.appid = GetAppKey();
            ydRequest.partner_id = GetPartnerId();
            ydRequest.secret = GetPartnerSecret();

            var orders = new List<OrderCancelInfo>();
            ydRequest.orders = orders;

            var orderInfo = new OrderCancelInfo();
            orderInfo.order_serial_no = order.code;
            orderInfo.mailno = order.mail_no;
            orders.Add(orderInfo);

            var ydResponse = Request<YdOrderCancelResponse>(ydRequest);
            if (ydResponse == null || !ydResponse.IsSuccess())
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

            var ydRequest = new YdOrderCreateB2CRequest();
            ydRequest.appid = GetAppKey();
            ydRequest.partner_id = GetPartnerId();
            ydRequest.secret = GetPartnerSecret();

            var orders = new List<OrderCreateInfo>();
            ydRequest.orders = orders;

            var orderInfo = new OrderCreateInfo();
            orders.Add(orderInfo);
            orderInfo.order_serial_no = order.code;
            orderInfo.khddh = order.code;
            orderInfo.order_type = "common";
            orderInfo.node_id = "350";
            orderInfo.weight = order.weight.ToString();
            orderInfo.size = order.volume.ToString();
            //orderInfo.special = order.goods_desc;

            var consigner = order.consigner;
            var sender = new ContactInfo();
            orderInfo.sender = sender;
            sender.name = consigner.contact;
            sender.phone = consigner.telephone;
            sender.mobile = consigner.cellphone;
            var address = consigner.address;
            sender.province = address.prov;
            sender.city = address.city;
            sender.county = address.area;
            sender.address = address.street;
            sender.postcode = address.postcode;

            var consignee = order.consignee;
            var receiver = new ContactInfo();
            orderInfo.receiver = receiver;
            receiver.name = consignee.contact;
            receiver.phone = consignee.telephone;
            receiver.mobile = consignee.cellphone;
            address = consignee.address;
            receiver.province = address.prov;
            receiver.city = address.city;
            receiver.county = address.area;
            receiver.address = address.street;
            receiver.postcode = address.postcode;

            var ydResponse = Request<YdOrderCreateB2CResponse>(ydRequest);
            if (ydResponse == null)
            {
                response.SetFailure("韵达响应异常！");
                return response;
            }
            if (!ydResponse.IsSuccess())
            {
                response.SetFailure(ydResponse.GetMessage());
                return response;
            }

            ydResponse.ToResponse(response);
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

            var ydRequest = new YdOrderFilterRequest();
            ydRequest.id = DateTime.Now.Ticks;

            var address = order.consigner.address;
            ydRequest.sender_address = address.prov + address.city + address.area + address.town + address.street;
            address = order.consignee.address;
            ydRequest.receiver_address = address.prov + address.city + address.area + address.town + address.street;

            var ydResponse = Request<YdOrderFilterResponse>(ydRequest);
            if (ydResponse == null || !ydResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
        {
            var response = new OrderUpdateResponse();

            var order = request.Orders[0];

            var ydRequest = new YdOrderUpdateRequest();
            ydRequest.appid = GetAppKey();
            ydRequest.partner_id = GetPartnerId();
            ydRequest.secret = GetPartnerSecret();

            var orders = new List<OrderCreateInfo>();
            ydRequest.orders = orders;

            var orderInfo = new OrderCreateInfo();
            orders.Add(orderInfo);
            orderInfo.order_serial_no = order.code;
            orderInfo.khddh = order.code;
            orderInfo.order_type = "common";
            orderInfo.node_id = "350";
            orderInfo.weight = order.weight.ToString();
            orderInfo.size = order.volume.ToString();
            //orderInfo.special = order.goods_desc;

            var consigner = order.consigner;
            var sender = new ContactInfo();
            orderInfo.sender = sender;
            sender.name = consigner.contact;
            sender.phone = consigner.telephone;
            sender.mobile = consigner.cellphone;
            var address = consigner.address;
            sender.province = address.prov;
            sender.city = address.city;
            sender.county = address.area;
            sender.address = address.street;
            sender.postcode = address.postcode;

            var consignee = order.consignee;
            var receiver = new ContactInfo();
            orderInfo.receiver = receiver;
            receiver.name = consignee.contact;
            receiver.phone = consignee.telephone;
            receiver.mobile = consignee.cellphone;
            address = consignee.address;
            receiver.province = address.prov;
            receiver.city = address.city;
            receiver.county = address.area;
            receiver.address = address.street;
            receiver.postcode = address.postcode;

            var ydResponse = Request<YdOrderUpdateResponse>(ydRequest);
            if (ydResponse == null || !ydResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            var response = new RouteSearchResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var ydRequest = new YdRouteSearchRequest();
            ydRequest.mailno = order.mail_no;

            var ydResponse = Request<YdRouteSearchResponse>(ydRequest);
            if (ydResponse == null)
            {
                response.SetFailure("韵达响应异常！");
                return response;
            }
            if (!ydResponse.IsSuccess())
            {
                response.SetFailure(ydResponse.GetMessage());
                return response;
            }

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

        #region 公共方法
        private T Request<T>(YdRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var client = new ScmHttpClient(req.GetServicePath());
            client.AddHeadParam("Content-type", "application/json");
            client.AddHeadParam("app-key", GetAppKey());
            client.AddHeadParam("sign", Digest(json));
            client.AddHeadParam("req-time", Digest(json));

            var request = new ScmHttpRequest();
            request.AddBody(json);

            var text = client.PostText(request);
            return text.AsJsonObject<T>();
        }

        private static HashAlgorithm _Md5;
        public static string Digest(string json)
        {
            string text = json + "_" + GetAppSecret();

            byte[] buytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            buytes = _Md5.ComputeHash(buytes);
            return Encode(buytes);
        }

        public static string Encode(byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public long Timestamp(DateTime time)
        {
            return (long)(time - _UnixTime).TotalMilliseconds;
        }

        public static string GetAppKey()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_KEY;
            }
            return APP_KEY_DEV;
        }

        public static string GetAppSecret()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_SECRET;
            }
            return APP_SECRET_DEV;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public string GetPartnerId()
        {
            return "201700101001";
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public string GetPartnerSecret()
        {
            return "123456789";
        }
        #endregion
    }
}
