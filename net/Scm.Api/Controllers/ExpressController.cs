using Com.Scm.Config;
using Com.Scm.Controllers;
using Com.Scm.Express.Dto;
using Com.Scm.Express.My;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Api.Controllers
{
    [Route("express")]
    public class ExpressController : ApiController
    {
        private EnvConfig _Config;

        public ExpressController(EnvConfig config)
        {
            _Config = config;
        }

        [HttpGet("query")]
        public ScmResponse QueryAsync(string shipper, string code, string phone)
        {
            var response = new ScmResponse();

            MyExpress express = MyExpress.GetInstance(shipper);
            if (express == null)
            {
                response.SetFailure($"无效的物流公司：{shipper}");
                return response;
            }

            var config = new MyConfig();
            if (!express.Init(config))
            {
                response.SetFailure($"物流公司 {shipper} 初始化失败！");
                return response;
            }

            var order = new OrderHeader();
            order.mail_no = code;
            order.consignee = new Contact();
            order.consignee.telephone = phone;
            return DoTest(express, "RouteSearch", order);
        }

        private ScmResponse DoTest(MyExpress express, string key, OrderHeader order)
        {
            if (key == "OrderFilter")
            {
                var request = new OrderFilterRequest();
                request.Order = order;
                return express.OrderFilter(request);
            }

            if (key == "OrderCreate")
            {
                var request = new OrderCreateRequest();
                request.Append(order);
                return express.OrderCreateB2C(request);
            }

            if (key == "OrderUpdate")
            {
                var request = new OrderUpdateRequest();
                request.Append(order);
                return express.OrderUpdateB2C(request);
            }

            if (key == "OrderSearch")
            {
                var request = new OrderSearchRequest();
                request.Append(order);
                return express.OrderSearch(request);
            }

            if (key == "RouteSearch")
            {
                var request = new RouteSearchRequest();
                request.Append(order);
                return express.RouteSearch(request);
            }

            if (key == "OrderCancel")
            {
                var request = new OrderCancelRequest();
                request.Order = order;
                return express.OrderCancel(request);
            }

            return null;
        }
    }
}
