using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Flow
{
    /// <summary>
    /// 
    /// </summary>
    public class ScmFlowOrderDto : ScmDataDto
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long flow_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// ͼ��
        /// </summary>
        [StringLength(32)]
        public string icon { get; set; }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        [StringLength(128)]
        public string url { get; set; }
    }
}