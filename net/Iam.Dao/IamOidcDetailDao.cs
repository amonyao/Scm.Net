using Com.Scm.Dao;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    /// <summary>
    /// ������ά�ȵ�OAuth
    /// </summary>
    [SugarTable("iam_oidc_detail")]
    public class IamOidcDetailDao : ScmDataDao
    {
        /// <summary>
        /// �����ID
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// �����ID
        /// </summary>
        public long osp_id { get; set; }

        /// <summary>
        /// OIDC����
        /// </summary>
        [StringLength(64)]
        public string code { get; set; }

        /// <summary>
        /// ��Ȩ����
        /// </summary>
        [StringLength(128)]
        public string oauth_code { get; set; }

        /// <summary>
        /// ��¼�û�
        /// </summary>
        [StringLength(64)]
        public string user { get; set; }

        /// <summary>
        /// չʾ����
        /// </summary>
        [StringLength(64)]
        public string name { get; set; }

        /// <summary>
        /// �û�ͷ��
        /// </summary>
        [StringLength(64)]
        public string avatar { get; set; }

        /// <summary>
        /// �ֻ�����
        /// </summary>
        [StringLength(32)]
        public string phone { get; set; }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        [StringLength(32)]
        public string email { get; set; }

        /// <summary>
        /// ��¼����
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// ��������(������)
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// �������ƹ���ʱ��
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// ˢ������(������)
        /// </summary>
        [StringLength(128)]
        public string refresh_token { get; set; }

        /// <summary>
        /// ˢ�����ƹ���ʱ��
        /// </summary>
        public long refresh_expires { get; set; }

        public bool IsAccessExpired(DateTime time)
        {
            return TimeUtils.GetUnixTime(time) > access_expires;
        }

        public bool IsRefreshExpired(DateTime time)
        {
            return TimeUtils.GetUnixTime(time) > access_expires;
        }
    }
}