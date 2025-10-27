using Com.Scm.Api;
using Com.Scm.Dvo;
using SqlSugar;

namespace Com.Scm.Dev.Sql.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDbResponse : ScmApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DbTableInfo> TableList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ResOptionDvo> PresqlList { get; set; }
    }
}
