using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.DEPPON.OrderCreate;
using Com.Scm.Express.My.Shipper.DEPPON.OrderSearch;
using Com.Scm.Express.My.Shipper.DEPPON.RouteSearch;
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

namespace Com.Scm.Express.My.Shipper.DEPPON
{
    public class DbExpress : MyExpress
    {
        public const string CUSTOMER_CODE = "DbConfig";

        public const string COMPANY_CODE = "EWBYCWLWKJSHYXGS";
        public const string COMPANY_SIGN = "TQXG";

        public const string APP_KEY = "e2d5b2ce524c81ebca041ef0733fdc4b";

        private DbConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is DbConfig))
            {
                _Config = new DbConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (DbConfig)config;
            }

            return true;
        }

        public override bool DcQuery()
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

            var dbRequest = new DbOrderCreateRequest();
            dbRequest.companyCode = GetCompanyCode();
            dbRequest.custOrderNo = order.code;
            dbRequest.customerCode = _Config.customerCode;
            dbRequest.logisticID = GetCompanySign() + order.code;
            dbRequest.needTraceInfo = 2;
            dbRequest.orderType = _Config.orderType;
            dbRequest.transportType = _Config.transportType;
            dbRequest.gmtCommit = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dbRequest.payType = _Config.payType;
            dbRequest.isOut = _Config.isOut;

            var consigner = order.consigner;
            var sender = new DbContactSender();
            dbRequest.sender = sender;
            sender.name = consigner.contact;
            //sender.phone = consigner.telephone;
            sender.mobile = consigner.cellphone;
            var address = consigner.address;
            sender.province = address.prov;
            sender.city = address.city;
            sender.county = address.area;
            sender.town = address.town;
            sender.address = address.street;

            var consignee = order.consignee;
            var receiver = new DbContactReceiver();
            dbRequest.receiver = receiver;
            receiver.name = consignee.contact;
            //receiver.phone = consignee.telephone;
            receiver.mobile = consignee.cellphone;
            address = consignee.address;
            receiver.province = address.prov;
            receiver.city = address.city;
            receiver.county = address.area;
            receiver.town = address.town;
            receiver.address = address.street;

            var package = new DbPackageInfo();
            package.cargoName = order.goods_desc;
            package.deliveryType = _Config.deliveryType;
            package.totalNumber = order.unit_qty;
            package.totalWeight = ToWeight(order.weight);
            package.totalVolume = ToVolume(order.volume);
            dbRequest.packageInfo = package;

            var dbResponse = Request<DbOrderCreateResponse>(dbRequest);
            if (dbResponse == null)
            {
                response.SetFailure("韵达响应异常！");
                return response;
            }
            if (!dbResponse.IsSuccess())
            {
                response.SetFailure(dbResponse.GetMessage());
                return response;
            }

            dbResponse.ToResponse(response);
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

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var dbRequest = new DbOrderSearchRequest();
            dbRequest.logisticCompanyID = "DEPPON";
            dbRequest.logisticID = GetCompanySign() + order.code;

            var dbResponse = Request<DbOrderSearchResponse>(dbRequest);
            if (dbResponse == null)
            {
                response.SetFailure("韵达响应异常！");
                return response;
            }
            if (!dbResponse.IsSuccess())
            {
                response.SetFailure(dbResponse.GetMessage());
                return response;
            }

            dbResponse.ToResponse(response);
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

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var dbRequest = new DbRouteSearchRequest();
            dbRequest.mailNo = order.mail_no;

            var dbResponse = Request<DbRouteSearchResponse>(dbRequest);
            if (dbResponse == null)
            {
                response.SetFailure("韵达响应异常！");
                return response;
            }
            if (!dbResponse.IsSuccess())
            {
                response.SetFailure(dbResponse.GetMessage());
                return response;
            }

            dbResponse.ToRespnose(response);
            response.SetSuccess();
            return response;
        }

        #region 公共方法
        private T Request<T>(DbRequest req) where T : class, new()
        {
            var now = DateTime.UtcNow;
            var time = Timestamp(now).ToString();

            var json = req.ToJsonString();

            var client = new ScmHttpClient(req.GetServicePath());
            client.AddHeadParam("Content-type", "application/x-www-form-urlencoded;charset=UTF-8");

            var request = new ScmHttpRequest();
            request.AddParameter("params", json);
            request.AddParameter("digest", Digest(json, time));
            request.AddParameter("timestamp", time);
            request.AddParameter("companyCode", GetCompanyCode());

            var text = client.PostText(request);
            return text.AsJsonObject<T>();
        }

        private static HashAlgorithm _Md5;
        public static string Digest(string json, string timestamp)
        {
            string text = json + GetAppKey() + timestamp;

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            bytes = _Md5.ComputeHash(bytes);
            bytes = Encoding.UTF8.GetBytes(Encode(bytes));
            return Convert.ToBase64String(bytes);
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
            return APP_KEY;
        }

        public string GetCompanyCode()
        {
            return COMPANY_CODE;
        }

        public string GetCompanySign()
        {
            return COMPANY_SIGN;
        }

        public double ToWeight(int weight)
        {
            if (weight < 10)
            {
                weight = 10;
            }
            return weight / 1000.0;
        }

        public double ToVolume(long volume)
        {
            return volume / 1000000000.0;
        }
        #endregion
    }
}
