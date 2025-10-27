using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Dr.Web
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_dr_web_daily")]
    public class ScmDrWebDailyDao : ScmDataDao
    {
        /// <summary>
        /// ����
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