using Com.Scm.Dvo;

namespace Com.Scm.Iam.Cfg.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class IamCfgAppOspDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long app_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long osp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int qty { get; set; }
    }
}