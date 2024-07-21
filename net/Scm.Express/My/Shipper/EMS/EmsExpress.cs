using Com.Scm.Express.Dto;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using System;

namespace Com.Scm.Express.My.Shipper.EMS
{
    public class EmsExpress : MyExpress
    {
        public const string APP_KEY = "ULg9OpPOIt1Bloec";
        public const string SECRET_KEY = "STFtVXdIQVRZUFhSdHViNg==";
        public const string APP_CODE = "YHTOWRPGO";
        /// <summary>
        /// 联派
        /// </summary>
        public const string ORDER_SOURCE = "LPGP";

        private EmsConfig _Config;

        public override bool DcQuery()
        {
            throw new NotImplementedException();
        }

        public override bool Init(MyConfig config)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
