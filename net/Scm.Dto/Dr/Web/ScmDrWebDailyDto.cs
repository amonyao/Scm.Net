using Com.Scm.Dto;

namespace Com.Scm.Dr.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ScmDrWebDailyDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string day { get; set; }

        /// <summary>
        /// ҳ�������
        /// </summary>
        public int pv { get; set; }

        /// <summary>
        /// �û�������
        /// </summary>
        public int uv { get; set; }
    }
}