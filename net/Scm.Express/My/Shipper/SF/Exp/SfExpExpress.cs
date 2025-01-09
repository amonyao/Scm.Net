using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Exp.DcQuery;
using Com.Scm.Express.My.Shipper.SF.Exp.Model;
using Com.Scm.Express.My.Shipper.SF.Exp.OrderCreate;
using Com.Scm.Express.My.Shipper.SF.Exp.OrderFilter;
using Com.Scm.Express.My.Shipper.SF.Exp.OrderSearch;
using Com.Scm.Express.My.Shipper.SF.Exp.OrderUpdate;
using Com.Scm.Express.My.Shipper.SF.Exp.RouteSearch;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Http;
using Com.Scm.Utils;
using System.Text.RegularExpressions;

namespace Com.Scm.Express.My.Shipper.SF.Exp
{
    /// <summary>
    /// 顺丰速运
    /// </summary>
    public class SfExpExpress : SfExpress
    {
        private SfExpConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is SfExpConfig))
            {
                _Config = new SfExpConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (SfExpConfig)config;
            }

            return GetAuth();
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            var response = new OrderSearchResponse();

            var order = request.Orders[0];

            var sfRequest = new SfOrderSearchRequest();
            sfRequest.searchType = "1";
            sfRequest.orderId = order.code;
            sfRequest.language = "zh-cn";

            var sfResponse = Request<SfExpResponse<SfOrderSearchResponse>>(sfRequest);
            if (sfResponse == null || !sfResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        /// <summary>
        /// 路由查询
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            var response = new RouteSearchResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var list = new List<string>();
            var order = request.Orders[0];
            list.Add(order.mail_no);

            var phone = GetPhone(order);
            if (phone == null)
            {
                response.SetFailure("无效的收寄人电话！");
                return response;
            }

            var sfRequest = new SfRouteSearchRequest();
            sfRequest.language = "0";
            sfRequest.trackingType = "1";
            sfRequest.trackingNumber = list;
            sfRequest.methodType = "1";
            sfRequest.checkPhoneNo = phone;

            var sfResponse = Request<SfExpResponse<SfRouteSearchResponse>>(sfRequest);
            if (sfResponse == null)
            {
                response.SetFailure("顺丰响应异常！");
                return response;
            }
            if (!sfResponse.IsSuccess())
            {
                response.SetFailure(sfResponse.GetMessage());
                return response;
            }

            sfResponse.msgData.ToResponse(response);
            response.SetSuccess();
            return response;
        }

        private string GetPhone(OrderHeader order)
        {
            string phone = null;
            var consignee = order.consignee;
            if (consignee != null)
            {
                phone = consignee.telephone ?? consignee.cellphone;
            }
            var consigner = order.consigner;
            if (consigner != null)
            {
                phone = consigner.telephone ?? consigner.cellphone;
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 4)
            {
                return null;
            }

            phone = phone.Substring(phone.Length - 4, 4);
            if (!Regex.IsMatch(phone, "^\\d{4}$"))
            {
                return null;
            }

            return phone;
        }

        /// <summary>
        /// 订单新增
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public override OrderCreateResponse OrderCreateB2C(OrderCreateRequest request)
        {
            var response = new OrderCreateResponse();

            if (!request.HasItems())
            {
                response.SetFailure("下单列表为空！");
                return response;
            }

            var order = request.Orders[0];

            var sfRequest = new SfOrderCreateRequest();
            sfRequest.language = _Config.language;
            sfRequest.orderId = order.code;
            sfRequest.cargoDesc = order.goods_desc;

            var contactInfoList = new List<ContactInfo>();
            sfRequest.contactInfoList = contactInfoList;

            var contactInfo = CreateContact(order.consigner);
            contactInfo.contactType = ContactType.Consigner;
            contactInfoList.Add(contactInfo);

            contactInfo = CreateContact(order.consignee);
            contactInfo.contactType = ContactType.Consignee;
            contactInfoList.Add(contactInfo);

            sfRequest.totalLength = order.length.ToString();
            sfRequest.totalWidth = order.width.ToString();
            sfRequest.totalHeight = order.height.ToString();
            sfRequest.totalWeight = order.weight.ToString();
            sfRequest.volume = order.volume.ToString();

            if (_Config.AutoCreate)
            {
                sfRequest.isGenWaybillNo = "1";
            }

            var sfResponse = Request<SfExpResponse<SfOrderCreateResponse>>(sfRequest);
            if (sfResponse == null || !sfResponse.IsSuccess())
            {
                response.SetFailure("");
                return response;
            }
            if (!sfResponse.IsSuccess())
            {
                response.SetFailure(sfResponse.GetMessage());
                return response;
            }

            sfResponse.msgData.ToResponse(response);

            response.SetSuccess();
            return response;
        }

        public override OrderCreateResponse OrderCreateC2C(OrderCreateRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 订单更新
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
        {
            var response = new OrderUpdateResponse();

            var order = request.Orders[0];

            var sfRequest = new SfOrderUpdateRequest();
            sfRequest.dealType = "1";
            sfRequest.orderId = order.code;

            var list = new List<OrderUpdateWaybillNoInfo>();
            sfRequest.waybillNoInfoList = list;

            var bill = new OrderUpdateWaybillNoInfo();
            bill.waybillNo = "";
            bill.waybillType = "1";
            list.Add(bill);

            var sfResponse = Request<SfExpResponse<SfOrderUpdateResponse>>(sfRequest);
            if (sfResponse == null || !sfResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var sfRequest = new SfOrderUpdateRequest();
            sfRequest.dealType = "2";
            sfRequest.orderId = order.code;

            var sfResponse = Request<SfExpResponse<SfOrderUpdateResponse>>(sfRequest);
            if (sfResponse == null)
            {
                return response;
            }
            if (!sfResponse.IsSuccess())
            {
                response.SetFailure(sfResponse.GetMessage());
                return response;
            }

            response.SetSuccess();
            return response;
        }

        public override OrderFilterResponse OrderFilter(OrderFilterRequest request)
        {
            var response = new OrderFilterResponse();

            var order = request.Order;

            var sfRequest = new SfOrderFilterRequest();
            sfRequest.filterType = FilterType.Type1;
            sfRequest.orderId = order.code;

            var list = new List<ContactInfo>();
            sfRequest.contactInfos = list;

            if (order.consigner != null)
            {
                var contact = CreateContact(order.consigner);
                contact.contactType = ContactType.Consigner;
                list.Add(contact);
            }
            if (order.consignee != null)
            {
                var contact = CreateContact(order.consignee);
                contact.contactType = ContactType.Consignee;
                list.Add(contact);
            }

            var sfResponse = Request<SfExpResponse<SfOrderFilterResponse>>(sfRequest);
            if (sfResponse == null || !sfResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        public override bool IsCollect(OrderHeader order)
        {
            var list = new List<string>();
            list.Add(order.mail_no);

            var phone = GetPhone(order);
            if (phone == null)
            {
                return false;
            }

            var sfRequest = new SfRouteSearchRequest();
            sfRequest.language = "0";
            sfRequest.trackingType = "1";
            sfRequest.trackingNumber = list;
            sfRequest.methodType = "1";
            sfRequest.checkPhoneNo = phone;

            var sfResponse = Request<SfExpResponse<SfRouteSearchResponse>>(sfRequest);
            if (sfResponse == null)
            {
                return false;
            }
            if (!sfResponse.IsSuccess())
            {
                return false;
            }

            return sfResponse.msgData.IsCollect();
        }

        public override bool IsReceipt(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override bool DcQuery()
        {
            var request = new DcQueryRequest();
            request.address = "广东省深圳市宝安区劳动路";
            request.opt = "opt";
            request.deptType = "1|2|5";
            request.servType = "1|2|5";
            request.distance = 1000;

            var response = Request<SfExpResponse<string>>(request);
            if (response == null || !response.IsSuccess())
            {
                return false;
            }

            var data = response.msgData;
            var dto = data.AsJsonObject<DcQueryResponse>();
            foreach (var d in dto.result)
            {
            }

            return true;
        }

        private static ContactInfo CreateContact(Contact contact)
        {
            var contactInfo = new ContactInfo();
            contactInfo.contact = contact.contact;
            contactInfo.mobile = contact.cellphone;

            var address = contact.address;
            if (address != null)
            {
                contactInfo.country = address.country;
                contactInfo.province = address.prov;
                contactInfo.city = address.city;
                contactInfo.address = address.street;
            }

            return contactInfo;
        }

        private T Request<T>(SfExpRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var now = DateTime.Now;
            var timestamp = Timestamp(now);

            var request = new ScmHttpRequest();
            request.AddParameter("partnerID", CUSTOMER_CODE);
            request.AddParameter("requestID", now.Ticks);
            request.AddParameter("serviceCode", req.GetServiceCode());
            request.AddParameter("timestamp", timestamp);
            request.AddParameter("msgDigest", Digest(json, timestamp));
            request.AddParameter("accessToken", _AccessToken);
            request.AddParameter("msgData", json);

            var response = Post<SfResponse>(req.GetServicePath(), request);
            if (response != null && response.IsSuccess())
            {
                var data = response.apiResultData;
                return data.AsJsonObject<T>();
            }
            return null;
        }
    }
}
