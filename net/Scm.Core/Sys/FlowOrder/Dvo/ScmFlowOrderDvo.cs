using Com.Scm.Dvo;

namespace Com.Scm.Flow
{
    /// <summary>
    /// 
    /// </summary>
    public class ScmFlowOrderDvo : ScmDataDvo
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long flow_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// ͼ��
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        public string url { get; set; }
    }
}