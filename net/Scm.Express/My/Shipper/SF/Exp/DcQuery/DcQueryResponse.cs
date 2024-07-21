using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.DcQuery
{
    public class DcQueryResponse
    {
        /// <summary>
        /// 状态 0成功 其他：失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 返回条数据
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 网点信息结果
        /// </summary>
        public List<DcInfo> result { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string x { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string y { get; set; }
    }

    public class DcInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int distance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string servertype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string servtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longitude { get; set; }
    }
}
