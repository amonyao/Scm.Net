using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.ZTO.RouteSearch.V2
{
    public class ZtoRouteSearchResponse : ZtoResponse<List<BillTrackOutput>>
    {
        public void ToResponse(RouteSearchResponse response)
        {
            if (result == null)
            {
                return;
            }

            var list = new List<RouteSearchResult>();
            var mailNo = "";
            foreach (var item in result)
            {
                var tmp = new RouteSearchResult();

                mailNo = item.billCode;

                tmp.opt_time = item.scanDate;
                tmp.opt_user_code = item.operateUserCode;
                tmp.opt_user_name = item.operateUser;
                tmp.opt_user_phone = item.operateUserPhone;

                var site = item.scanSite;
                tmp.opt_unit_code = site.code;
                tmp.opt_unit_name = site.name;
                tmp.opt_unit_phone = site.phone;

                tmp.remark = item.desc;
                list.Add(tmp);
            }
            response.Append(mailNo, list);
        }

        public bool IsCollect()
        {
            if (result == null)
            {
                return false;
            }

            return result.Count > 0;
        }

        public bool IsReceipt()
        {
            if (result == null)
            {
                return false;
            }

            var data = result[0];
            var scanType = data.scanType ?? "";
            return scanType.IndexOf("签收") >= 0;
        }
    }

    public class BillTrackOutput
    {
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string extend { get; set; }
        /// <summary>
        /// 快件所在国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public string signMan { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string operateUser { get; set; }
        /// <summary>
        /// 操作人Code
        /// </summary>
        public string operateUserCode { get; set; }
        /// <summary>
        /// 操作人联系方式
        /// </summary>
        public string operateUserPhone { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string billCode { get; set; }
        /// <summary>
        /// 扫描类型:收件 、发件、 到件、 派件、 签收、退件、问题件、ARRIVAL （派件入三方自提柜等、SIGNED（派件出三方自提柜等）
        /// </summary>
        public string scanType { get; set; }
        /// <summary>
        /// 扫描时间
        /// </summary>
        public string scanDate { get; set; }
        /// <summary>
        /// 轨迹描述
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 扫描网点
        /// </summary>
        public SiteInfo scanSite { get; set; }

        /// <summary>
        /// 上、下一站网点信息（scanType为发件，表示下一站；scanType为到件，表示上一站）
        /// </summary>
        public SiteInfo preOrNextSite { get; set; }
    }

    public class SiteInfo
    {
        /// <summary>
        /// 网点是否中心,T:true,F:false
        /// </summary>
        public string isCenter { get; set; }
        /// <summary>
        /// 网点编码/机柜hallCode;网点扫描时为网点编码，入站出站第三方签收时是机柜的hallCode
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 网点联系方式
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 网点所在省份
        /// </summary>
        public string prov { get; set; }
        /// <summary>
        /// 网点所在城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 网点是否转运中心.1，是 2，否
        /// </summary>
        public int isTransfer { get; set; }
        /// <summary>
        /// 网点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 原始网点id,始终为网点id
        /// </summary>
        public long siteId { get; set; }
        /// <summary>
        /// 网点扫描时为网点id，入站出站第三方签收时为null（即存代码，不确认是否有业务方在使用）
        /// </summary>
        public long id { get; set; }
    }
}
