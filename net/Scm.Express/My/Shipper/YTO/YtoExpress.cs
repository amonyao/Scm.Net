using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.YTO.OrderCancel;
using Com.Scm.Express.My.Shipper.YTO.OrderCreate;
using Com.Scm.Express.My.Shipper.YTO.OrderFilter;
using Com.Scm.Express.My.Shipper.YTO.RouteSearch;
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

namespace Com.Scm.Express.My.Shipper.YTO
{
    public class YtoExpress : MyExpress
    {
        public const string APP_KEY = "064a61a37a3eb6bc8aa99";
        public const string APP_SECRET = "1f5e9edf174b8cb7bed5757b486e65ac";

        public const string APP_KEY_DEV = "d577e7b5024ad20446e10";
        public const string APP_SECRET_DEV = "0e8e9457d493666ee2f5adb783e69abb";

        private YtoConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is YtoConfig))
            {
                _Config = new YtoConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (YtoConfig)config;
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

            var ytoRequest = new YtoOrderCancelRequest();
            ytoRequest.logisticsNo = order.code;
            ytoRequest.cancelDesc = request.CancelReason ?? "客户取消";

            var text = Request(ytoRequest);
            if (string.IsNullOrEmpty(text))
            {
                return response;
            }

            if (text.IndexOf("\"reason\"") < 0)
            {
                text = "{\"data\":" + text + ",\"success\": \"true\"}";
            }
            var ytoResponse = text.AsJsonObject<YtoOrderCancelResponse>();
            if (ytoResponse == null || !ytoResponse.IsSuccess())
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

            var ytoRequest = new YtoOrderCreateRequest();
            ytoRequest.logisticsNo = order.code;

            var consigner = order.consigner;
            ytoRequest.senderName = consigner.contact;
            ytoRequest.senderMobile = consigner.cellphone;
            var address = consigner.address;
            ytoRequest.senderProvinceName = address.prov;
            ytoRequest.senderCityName = address.city;
            ytoRequest.senderCountyName = address.area;
            ytoRequest.senderTownName = address.town;
            ytoRequest.senderAddress = address.street;

            var consignee = order.consignee;
            ytoRequest.recipientName = consignee.contact;
            ytoRequest.recipientMobile = consignee.cellphone;
            address = consignee.address;
            ytoRequest.recipientProvinceName = address.prov;
            ytoRequest.recipientCityName = address.city;
            ytoRequest.recipientCountyName = address.area;
            ytoRequest.recipientTownName = address.town;
            ytoRequest.recipientAddress = address.street;

            var text = Request(ytoRequest);
            if (string.IsNullOrEmpty(text))
            {
                return response;
            }

            if (text.IndexOf("\"reason\"") < 0)
            {
                text = "{\"data\":" + text + ",\"success\": \"true\"}";
            }
            var ytoResponse = text.AsJsonObject<YtoOrderCreateResponse>();
            if (ytoResponse == null)
            {
                response.SetFailure("圆通响应异常！");
                return response;
            }
            if (!ytoResponse.IsSuccess())
            {
                response.SetFailure(ytoResponse.GetMessage());
                return response;
            }

            ytoResponse.ToResponse(response);
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

            var ytoRequest = new YtoOrderFilterRequest();
            ytoRequest.fromCountry = "CN";
            ytoRequest.country = "CN";

            var address = order.consigner.address;
            ytoRequest.startProv = address.prov;
            ytoRequest.startCity = address.city;
            ytoRequest.startCountry = address.area;
            ytoRequest.startTown = address.town;
            ytoRequest.startAddress = address.street;

            address = order.consignee.address;
            ytoRequest.endProv = address.prov;
            ytoRequest.endCity = address.city;
            ytoRequest.endCountry = address.area;
            ytoRequest.endTown = address.town;
            ytoRequest.endAddress = address.street;

            var text = Request(ytoRequest);
            if (string.IsNullOrEmpty(text))
            {
                return response;
            }

            if (text.IndexOf("\"reason\"") < 0)
            {
                text = "{\"data\":" + text + ",\"success\": \"true\"}";
            }
            var ytoResponse = text.AsJsonObject<YtoOrderCancelResponse>();
            if (ytoResponse == null || !ytoResponse.IsSuccess())
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

            var ytoRequest = new YtoRouteSearchRequest();
            ytoRequest.number = order.mail_no;

            var text = Request(ytoRequest);
            if (string.IsNullOrEmpty(text))
            {
                return response;
            }

            //if (text[0] == '{')
            //{
            //    text = "{\"map\":{\"" + order.mail_no + "\": " + text + "},\"success\": \"true\"}";
            //}

            var ytoResponse = text.AsJsonObject<YtoRouteSearchResponse>();
            if (ytoResponse == null)
            {
                response.SetFailure("圆通响应异常！");
                return response;
            }
            if (!ytoResponse.IsSuccess())
            {
                response.SetFailure(ytoResponse.GetMessage());
                return response;
            }

            ytoResponse.ToResponse(response);
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

        #region 公共方法
        private string Request(YtoRequest req)
        {
            var json = req.ToJsonString();

            var now = DateTime.Now;

            var req2 = new ApiRequest();
            req2.timestamp = Timestamp(now).ToString();
            req2.param = json;
            req2.format = "JSON";
            req2.sign = Digest(json, req);
            json = req2.ToJsonString();

            var request = new ScmHttpRequest();
            request.AddBody(json);

            var client = new ScmHttpClient(req.GetServicePath());

            return client.PostText(request);
        }

        private static HashAlgorithm _Md5;
        public static string Digest(string json, YtoRequest request)
        {
            string text = json + request.GetServiceCode() + request.GetServiceVer() + request.GetPartnerPass();

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
