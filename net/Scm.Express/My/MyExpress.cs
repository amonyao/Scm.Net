using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.BEST.KY;
using Com.Scm.Express.My.Shipper.DEPPON;
using Com.Scm.Express.My.Shipper.JDL.Ecap;
using Com.Scm.Express.My.Shipper.JT;
using Com.Scm.Express.My.Shipper.SF.Exp;
using Com.Scm.Express.My.Shipper.SF.Scs;
using Com.Scm.Express.My.Shipper.STO;
using Com.Scm.Express.My.Shipper.YTO;
using Com.Scm.Express.My.Shipper.YUNDA;
using Com.Scm.Express.My.Shipper.ZTO;
using Com.Scm.Express.OrderCancel;
using Com.Scm.Express.OrderCreate;
using Com.Scm.Express.OrderFilter;
using Com.Scm.Express.OrderSearch;
using Com.Scm.Express.OrderUpdate;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Express.ShipperSearch;
using System;
using System.ComponentModel;
using System.Text;
using System.Web;

namespace Com.Scm.Express.My
{
    public abstract class MyExpress
    {
        public static EnvironmentEnum Environment { get; protected set; } = EnvironmentEnum.Release;

        public const string SHIPPER_SF = "SF";
        public const string SHIPPER_STO = "STO";
        public const string SHIPPER_YTO = "YTO";
        public const string SHIPPER_ZTO = "ZTO";
        public const string SHIPPER_YUNDA = "YUNDA";
        public const string SHIPPER_JDL = "JDL";
        public const string SHIPPER_BSKY = "BSKY";
        public const string SHIPPER_JT = "JT";
        public const string SHIPPER_DEPPON = "DEPPON";

        public abstract bool Init(MyConfig config);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ShipperSearchResponse ShipperSearch(ShipperSearchRequest request)
        {
            var response = new ShipperSearchResponse();

            response.Append(new ShipperSearchResult { code = SHIPPER_SF, name = "顺丰速运" });
            response.Append(new ShipperSearchResult { code = SHIPPER_STO, name = "申通快递" });
            response.Append(new ShipperSearchResult { code = SHIPPER_YTO, name = "圆通速递" });
            response.Append(new ShipperSearchResult { code = SHIPPER_ZTO, name = "中通快递" });
            response.Append(new ShipperSearchResult { code = SHIPPER_YUNDA, name = "韵达速递" });
            response.Append(new ShipperSearchResult { code = SHIPPER_JDL, name = "京东物流" });
            response.Append(new ShipperSearchResult { code = SHIPPER_BSKY, name = "百世快运" });
            response.Append(new ShipperSearchResult { code = SHIPPER_JT, name = "极兔速递" });
            response.Append(new ShipperSearchResult { code = SHIPPER_DEPPON, name = "德帮快递" });

            response.SetSuccess();
            return response;
        }

        /// <summary>
        /// 运单查询
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract OrderSearchResponse OrderSearch(OrderSearchRequest request);
        /// <summary>
        /// 路由查询
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract RouteSearchResponse RouteSearch(RouteSearchRequest request);
        /// <summary>
        /// 运单创建（企业）
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract OrderCreateResponse OrderCreateB2C(OrderCreateRequest request);
        /// <summary>
        /// 运单创建（个人）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract OrderCreateResponse OrderCreateC2C(OrderCreateRequest request);
        /// <summary>
        /// 运单更新（企业）
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract OrderUpdateResponse OrderUpdateB2C(OrderUpdateRequest order);
        /// <summary>
        /// 运单更新（个人）
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        //public abstract OrderUpdateResponse OrderUpdateC2C(OrderUpdateRequest order);
        /// <summary>
        /// 运单取消
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract OrderCancelResponse OrderCancel(OrderCancelRequest request);

        /// <summary>
        /// 订单过滤
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract OrderFilterResponse OrderFilter(OrderFilterRequest request);

        /// <summary>
        /// 是否已揽件
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract bool IsCollect(OrderHeader order);

        /// <summary>
        /// 是否已签收
        /// </summary>
        /// <returns></returns>
        public abstract bool IsReceipt(OrderHeader order);

        public abstract bool DcQuery();

        public static string UrlEncode(string text, Encoding encod, bool cap = true)
        {
            if (cap)
            {
                var builder = new StringBuilder();
                foreach (char c in text)
                {
                    var t = HttpUtility.UrlEncode(c.ToString(), encod);
                    if (t.Length > 1)
                    {
                        builder.Append(t.ToUpper());
                    }
                    else
                    {
                        builder.Append(c);
                    }
                }
                return builder.ToString();
            }
            else
            {
                return HttpUtility.UrlEncode(text, encod);
            }
        }

        protected static DateTime _UnixTime = new DateTime(1970, 1, 1);

        public static MyExpress GetInstance(string shipper)
        {
            switch (shipper)
            {
                case SHIPPER_SF:
                    return new SfExpExpress();
                case SHIPPER_STO:
                    return new StoExpress();
                case SHIPPER_YTO:
                    return new YtoExpress();
                case SHIPPER_ZTO:
                    return new ZtoExpress();
                case SHIPPER_YUNDA:
                    return new YdExpress();
                case SHIPPER_JDL:
                    return new JdlEcapExpress();
                case SHIPPER_BSKY:
                    return new BsKyExpress();
                case SHIPPER_JT:
                    return new JtExpress();
                case SHIPPER_DEPPON:
                    return new DbExpress();
            }
            return new DefExpress();
        }

        public static MyExpress GetInstance(ShipperEnum shipper)
        {
            switch (shipper)
            {
                case ShipperEnum.Sf:
                case ShipperEnum.Sf_Exp:
                    return new SfExpExpress();
                case ShipperEnum.Sf_Scs:
                    return new SfScsExpress();
                //case ShipperEnum.Sf_Fop:
                //    return new SfFopExpress();
                case ShipperEnum.Sto:
                    return new StoExpress();
                case ShipperEnum.Yto:
                    return new YtoExpress();
                case ShipperEnum.Zto:
                    return new ZtoExpress();
                case ShipperEnum.Yunda:
                    return new YdExpress();
                case ShipperEnum.Jdl:
                case ShipperEnum.Jdl_Ecap:
                    return new JdlEcapExpress();
                case ShipperEnum.Bsky:
                    return new BsKyExpress();
                case ShipperEnum.Jt:
                    return new JtExpress();
                case ShipperEnum.Deppon:
                    return new DbExpress();
                default:
                    return new DefExpress();
            }
        }
    }

    public enum ShipperEnum
    {
        [Description("顺丰速运")]
        Sf = 120,
        [Description("顺丰速运")]
        Sf_Exp = 121,
        [Description("顺丰冷运")]
        Sf_Scs = 122,
        [Description("顺丰快运")]
        Sf_Fop = 123,
        [Description("申通快递")]
        Sto = 130,
        [Description("圆通速递")]
        Yto = 140,
        [Description("中通快递")]
        Zto = 150,
        [Description("韵达速运")]
        Yunda = 160,
        [Description("京东物流")]
        Jdl = 170,
        [Description("京东物流")]
        Jdl_Ecap = 171,
        [Description("京东物流")]
        Jdl_Eclp = 172,
        [Description("百世快运")]
        Bsky = 180,
        [Description("极兔速递")]
        Jt = 190,
        [Description("德邦快递")]
        Deppon = 200
    }
}
