using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.STO.OrderCancel;
using Com.Scm.Express.My.Shipper.STO.OrderCreate;
using Com.Scm.Express.My.Shipper.STO.OrderFilter;
using Com.Scm.Express.My.Shipper.STO.RouteSearch;
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

namespace Com.Scm.Express.My.Shipper.STO
{
    public class StoExpress : MyExpress
    {
        public const string APP_KEY = "CAKADzXrvJtIitk";
        public const string SECRET_KEY = "i3lqZlQOnMxxRxTBgRTXoJFMoIU80woT";
        public const string APP_CODE = "CAKADzXrvJtIitk";
        /// <summary>
        /// 联派
        /// </summary>
        public const string ORDER_SOURCE = "LPGP";

        private StoConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is StoConfig))
            {
                _Config = new StoConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (StoConfig)config;
            }

            return true;
        }

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        public static string GetAppKey()
        {
            return APP_KEY;
        }

        public static string GetSecret()
        {
            return SECRET_KEY;
        }

        public static string GetAppCode()
        {
            return APP_CODE;
        }

        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var stoRequest = new StoOrderCancelRequest();
            stoRequest.billCode = order.mail_no;
            stoRequest.orderSource = ORDER_SOURCE;
            stoRequest.orderType = "02";

            var stoResponse = Request<StoOrderCancelResponse>(stoRequest);
            if (stoResponse == null)
            {
                return response;
            }
            if (!stoResponse.IsSuccess())
            {
                response.SetFailure(stoResponse.GetMessage());
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

            var stoRequest = new StoOrderCreateRequest();
            stoRequest.orderNo = order.code;
            stoRequest.orderSource = ORDER_SOURCE;
            stoRequest.billType = "00";
            stoRequest.orderType = "01";
            stoRequest.waybillNo = order.mail_no;

            var consigner = order.consigner;
            var sender = new SenderDO();
            stoRequest.sender = sender;
            sender.name = consigner.contact;
            sender.tel = consigner.telephone;
            sender.mobile = consigner.cellphone;
            var address = consigner.address;
            sender.country = address.country;
            sender.province = address.prov;
            sender.city = address.city;
            sender.area = address.area;
            sender.town = address.town;
            sender.address = address.street;
            sender.postCode = address.postcode;

            var consignee = order.consignee;
            var receiver = new ReceiverDO();
            stoRequest.receiver = receiver;
            receiver.name = consignee.contact;
            receiver.tel = consignee.telephone;
            receiver.mobile = consignee.cellphone;
            address = consignee.address;
            receiver.country = address.country;
            receiver.province = address.prov;
            receiver.city = address.city;
            receiver.area = address.area;
            receiver.town = address.town;
            receiver.address = address.street;
            receiver.postCode = address.postcode;

            var customer = new CustomerDO();
            stoRequest.customer = customer;
            customer.siteCode = "666666";
            customer.customerName = "666666000001";
            customer.sitePwd = "abc123";
            customer.siteCode = "666666";

            var cargo = new CargoDO();
            stoRequest.cargo = cargo;
            cargo.battery = "30";
            cargo.goodsType = "文件";
            cargo.goodsName = order.goods_desc;

            var stoResponse = Request<StoOrderCreateResponse>(stoRequest);
            if (stoResponse == null)
            {
                response.SetFailure("申通响应异常！");
                return response;
            }
            if (!stoResponse.IsSuccess())
            {
                response.SetFailure(stoResponse.GetMessage());
                return response;
            }

            stoResponse.ToResponse(response);
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

            var stoRequest = new StoOrderFilterRequest();
            stoRequest.requestUUID = DateTime.Now.Ticks.ToString();
            stoRequest.customerName = ORDER_SOURCE;

            var sender = new SendAddress();
            stoRequest.sendAddress = sender;
            var address = order.consigner.address;
            sender.provinceName = address.prov;
            sender.cityName = address.city;
            sender.areaName = address.area;
            sender.townName = address.town;
            sender.detailAddress = address.street;

            var consignee = new ConsigneeAddress();
            stoRequest.consigneeAddress = consignee;
            address = order.consignee.address;
            consignee.provinceName = address.prov;
            consignee.cityName = address.city;
            consignee.areaName = address.area;
            consignee.townName = address.town;
            consignee.detailAddress = address.street;

            var stoResponse = Request<StoOrderFilterResponse>(stoRequest);
            if (stoResponse == null || !stoResponse.IsSuccess())
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

            var list = new List<string>();
            foreach (var order in request.Orders)
            {
                list.Add(order.mail_no);
            }

            var stoRequest = new StoRouteSearchRequest();
            stoRequest.waybillNoList = list;

            var stoResponse = Request<StoRouteSearchResponse>(stoRequest);
            if (stoResponse == null)
            {
                response.SetFailure("申通响应异常！");
                return response;
            }
            if (!stoResponse.IsSuccess())
            {
                response.SetFailure(stoResponse.GetMessage());
                return response;
            }

            stoResponse.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        public override bool IsCollect(OrderHeader order)
        {
            var list = new List<string>
            {
                order.mail_no
            };

            var stoRequest = new StoRouteSearchRequest();
            stoRequest.waybillNoList = list;

            var stoResponse = Request<StoRouteSearchResponse>(stoRequest);
            if (stoResponse == null)
            {
                return false;
            }
            if (!stoResponse.IsSuccess())
            {
                return false;
            }

            return stoResponse.IsCollect();
        }

        public override bool IsReceipt(OrderHeader order)
        {
            var list = new List<string>
            {
                order.mail_no
            };

            var stoRequest = new StoRouteSearchRequest();
            stoRequest.waybillNoList = list;

            var stoResponse = Request<StoRouteSearchResponse>(stoRequest);
            if (stoResponse == null)
            {
                return false;
            }
            if (!stoResponse.IsSuccess())
            {
                return false;
            }

            return stoResponse.IsReceipt();
        }

        #region 公共方法
        private T Request<T>(StoRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var request = new HttpRequest();
            request.AddParameter("api_name", req.GetApiName());
            request.AddParameter("from_appkey", GetAppKey());
            request.AddParameter("from_code", GetAppCode());
            request.AddParameter("to_appkey", req.GetToAppKey());
            request.AddParameter("to_code", req.GetToCode());
            request.AddParameter("content", json);
            request.AddParameter("data_digest", Digest(json));

            var client = new RestClient(req.GetServicePath());
            client.AddDefaultHeader("Content-type", "application/x-www-form-urlencoded");

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }

        private static HashAlgorithm _Md5;
        public static string Digest(string json)
        {
            string text = json + GetSecret();

            byte[] buytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            buytes = _Md5.ComputeHash(buytes);
            return Convert.ToBase64String(buytes);
        }
        #endregion
    }
}
