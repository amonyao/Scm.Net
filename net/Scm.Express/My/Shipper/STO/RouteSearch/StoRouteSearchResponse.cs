using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;
using System.Linq;

namespace Com.Scm.Express.My.Shipper.STO.RouteSearch
{
    public class StoRouteSearchResponse : StoResponse<Dictionary<string, List<CommonResultVO>>>
    {
        public void ToResponse(RouteSearchResponse response)
        {
            if (data != null)
            {
                foreach (var k in data.Keys)
                {
                    var list = new List<RouteSearchResult>();
                    foreach (var t in data[k])
                    {
                        var result = new RouteSearchResult();
                        result.mail_no = t.waybillNo;
                        result.opt_time = t.opTime;
                        result.remark = t.memo;

                        result.opt_user_code = t.opEmpCode;
                        result.opt_user_name = t.opEmpName;
                        result.opt_user_phone = t.bizEmpPhone;

                        result.opt_unit_code = t.opOrgCode;
                        result.opt_unit_name = t.opOrgName;
                        result.opt_unit_phone = t.opOrgTel;

                        list.Add(result);
                    }
                    response.Append(k, list);
                }
            }
        }

        public bool IsCollect()
        {
            if (data == null)
            {
                return false;
            }

            var key = data.Keys.FirstOrDefault();
            if (key == null)
            {
                return false;
            }

            var t = data[key];
            return t.Count > 0;
        }

        public bool IsReceipt()
        {
            if (data == null)
            {
                return false;
            }

            var key = data.Keys.FirstOrDefault();
            if (key == null)
            {
                return false;
            }

            var t = data[key];
            var scanType = t[0].scanType ?? "";
            return scanType.IndexOf("签收") >= 0;
        }
    }

    public class CommonResultVO
    {
        /// <summary>
        /// 运单号	
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// 扫描网点名称	
        /// </summary>
        public string opOrgName { get; set; }
        /// <summary>
        /// 扫描网点编号	
        /// </summary>
        public string opOrgCode { get; set; }
        /// <summary>
        /// 扫描网点所在城市	
        /// </summary>
        public string opOrgCityName { get; set; }
        /// <summary>
        /// 扫描网点所在省份	
        /// </summary>
        public string opOrgProvinceName { get; set; }
        /// <summary>
        /// 扫描网点电话	
        /// </summary>
        public string opOrgTel { get; set; }
        /// <summary>
        /// 扫描时间	
        /// </summary>
        public string opTime { get; set; }
        /// <summary>
        /// 扫描类型	
        /// </summary>
        public string scanType { get; set; }
        /// <summary>
        /// 扫描员	
        /// </summary>
        public string opEmpName { get; set; }
        /// <summary>
        /// 扫描员编号	
        /// </summary>
        public string opEmpCode { get; set; }
        /// <summary>
        /// 轨迹描述信息	
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 派件员或收件员姓名	
        /// </summary>
        public string bizEmpName { get; set; }
        /// <summary>
        /// 派件员或收件员编号	
        /// </summary>
        public string bizEmpCode { get; set; }
        /// <summary>
        /// 派件员或收件员电话	
        /// </summary>
        public string bizEmpPhone { get; set; }
        /// <summary>
        /// 派件员或收件员电话	
        /// </summary>
        public string bizEmpTel { get; set; }
        /// <summary>
        /// 下一站名称	
        /// </summary>
        public string nextOrgName { get; set; }
        /// <summary>
        /// 下一站编号	
        /// </summary>
        public string nextOrgCode { get; set; }
        /// <summary>
        /// 问题件原因名称	
        /// </summary>
        public string issueName { get; set; }
        /// <summary>
        /// 签收人	
        /// </summary>
        public string signoffPeople { get; set; }
        /// <summary>
        /// 重量，单位：kg	
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 包号	
        /// </summary>
        public string containerNo { get; set; }
        /// <summary>
        /// 寄件网点编号	
        /// </summary>
        public string orderOrgCode { get; set; }
        /// <summary>
        /// 寄件网点名称	
        /// </summary>
        public string orderOrgName { get; set; }
        /// <summary>
        /// 运输任务号	
        /// </summary>
        public string transportTaskNo { get; set; }
        /// <summary>
        /// 车牌号	
        /// </summary>
        public string carNo { get; set; }
        /// <summary>
        /// 网点类型编号，0003为转运中心,其它都为独立网点	
        /// </summary>
        public string opOrgTypeCode { get; set; }
    }
}
