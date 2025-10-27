namespace Com.Scm.Dev.Dif.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class CompareRequest : ScmRequest
    {
        /// <summary>
        /// 来源主机
        /// </summary>
        public string SrcHost { get; set; }
        /// <summary>
        /// 来源端口
        /// </summary>
        public int SrcPort { get; set; }
        /// <summary>
        /// 来源用户
        /// </summary>
        public string SrcUser { get; set; }
        /// <summary>
        /// 来源密码
        /// </summary>
        public string SrcPass { get; set; }
        /// <summary>
        /// 来源数据库
        /// </summary>
        public string SrcDb { get; set; }

        /// <summary>
        /// 目标主机
        /// </summary>
        public string DstHost { get; set; }
        /// <summary>
        /// 目标端口
        /// </summary>
        public int DstPort { get; set; }
        /// <summary>
        /// 目标用户
        /// </summary>
        public string DstUser { get; set; }
        /// <summary>
        /// 目标密码
        /// </summary>
        public string DstPass { get; set; }
        /// <summary>
        /// 目标数据库
        /// </summary>
        public string DstDb { get; set; }

        /// <summary>
        /// 是否支持注释
        /// </summary>
        public bool comment { get; set; }
        /// <summary>
        /// 是否支持排序
        /// </summary>
        public bool reorder { get; set; }
    }
}
