using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Flow
{
    /// <summary>
    /// �������̱�������Ϣ
    /// </summary>
    [SugarTable("scm_flow_order")]
    public class ScmFlowOrderDao : ScmDataDao
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long flow_id { get; set; }

        /// <summary>
        /// ���ݴ���
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// ��������
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