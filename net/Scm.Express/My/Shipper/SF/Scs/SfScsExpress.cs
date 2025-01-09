using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Scs.Model;
using Com.Scm.Express.My.Shipper.SF.Scs.OrderCancel;
using Com.Scm.Express.My.Shipper.SF.Scs.OrderCreate;
using Com.Scm.Express.My.Shipper.SF.Scs.OrderSearch;
using Com.Scm.Express.My.Shipper.SF.Scs.RouteSearch;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Http;
using Com.Scm.Utils;

namespace Com.Scm.Express.My.Shipper.SF.Scs
{
    public class SfScsExpress : SfExpress
    {
        private SfScsConfig _Config;

        public override bool Init(MyConfig config)
        {
            if (config == null || !(config is SfScsConfig))
            {
                _Config = new SfScsConfig();
                _Config.LoadDef();
            }
            else
            {
                _Config = (SfScsConfig)config;
            }

            return GetAuth();
        }

        public override RouteSearchResponse RouteSearch(RouteSearchRequest request)
        {
            var response = new RouteSearchResponse();

            if (!request.HasItems())
            {
                return response;
            }

            var order = request.Orders[0];

            var sfRequest = new SfScsRouteSearchRequest();
            sfRequest.erpOrder = order.code;

            var sfResponse = Request<SfScsResponse<List<SfScsRouteSearchResponse>>>(sfRequest);
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

            response.SetSuccess();
            return response;
        }

        public override OrderSearchResponse OrderSearch(OrderSearchRequest request)
        {
            var response = new OrderSearchResponse();

            var order = request.Orders[0];

            var sfRequest = new SfScsOrderSearchRequest();
            sfRequest.erpOrder = order.code;

            var sfResponse = Request<SfScsResponse<SfScsOrderSearchResponse>>(sfRequest);
            if (sfResponse == null || !sfResponse.IsSuccess())
            {
                return response;
            }

            return response;
        }

        public override OrderFilterResponse OrderFilter(OrderFilterRequest request)
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

            if (string.IsNullOrWhiteSpace(order.code))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(order.order_time))
            {
                return response;
            }

            var consigner = order.consigner;
            if (consigner == null)
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(consigner.contact))
            {
                return response;
            }
            var address = consigner.address;
            if (address == null)
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.prov))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.city))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.area))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.street))
            {
                return response;
            }

            var consignee = order.consignee;
            if (consignee == null)
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(consignee.contact))
            {
                return response;
            }
            address = consignee.address;
            if (address == null)
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.prov))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.city))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.area))
            {
                return response;
            }
            if (string.IsNullOrWhiteSpace(address.street))
            {
                return response;
            }

            var sfRequest = new SfScsOrderCreateRequest();
            sfRequest.erpOrder = order.code;
            sfRequest.productCode = "SE0030";
            sfRequest.orderTime = order.order_time;
            sfRequest.paymentTypeCode = "CC_CASH";
            sfRequest.remark = order.goods_desc;
            sfRequest.transportType = "LAND";
            sfRequest.temperatureLevelCode = "2";

            sfRequest.shipperName = consigner.unit;
            sfRequest.shipperContactName = consigner.contact;
            sfRequest.shipperContactTel = string.IsNullOrWhiteSpace(consigner.telephone) ? consigner.cellphone : consigner.telephone;
            address = consigner.address;
            sfRequest.shipperProvinceName = address.prov;
            sfRequest.shipperCityName = address.city;
            sfRequest.shipperDistrictName = address.area;
            sfRequest.shipperLocationName = address.street;

            sfRequest.consigneeName = consignee.unit;
            sfRequest.consigneeContactName = consignee.contact;
            sfRequest.consigneeContactTel = string.IsNullOrWhiteSpace(consignee.telephone) ? consignee.cellphone : consignee.telephone;
            address = consignee.address;
            sfRequest.consigneeProvinceName = address.prov;
            sfRequest.consigneeCityName = address.city;
            sfRequest.consigneeDistrictName = address.area;
            sfRequest.consigneeLocationName = address.street;

            sfRequest.totalWeight = order.weight.ToString();
            sfRequest.totalVolume = order.volume.ToString();

            if (order.details == null)
            {
                return response;
            }

            var items = new List<OrderItem>();
            sfRequest.orderItems = items;
            foreach (var detail in order.details)
            {
                var item = new OrderItem();
                item.skuCode = "其它";
                item.skuName = detail.goods_name;
                item.volume = detail.volume.ToString();
                items.Add(item);
            }

            var sfResponse = Request<SfScsResponse<SfScsOrderCreateResponse>>(sfRequest);
            if (sfResponse == null)
            {
                return response;
            }
            if (!sfResponse.IsSuccess())
            {
                response.SetFailure(sfResponse.GetMessage());
                return response;
            }

            return response;
        }

        public override OrderCreateResponse OrderCreateC2C(OrderCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public override OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            var response = new OrderCancelResponse();

            var order = request.Order;

            var sfRequest = new SfScsOrderCancelRequest();
            sfRequest.erpOrder = order.code;

            var sfResponse = Request<SfScsResponse<string>>(sfRequest);
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

        public override bool IsCollect(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override bool IsReceipt(OrderHeader order)
        {
            throw new NotImplementedException();
        }

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        private T Request<T>(SfScsRequest req) where T : class, new()
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
