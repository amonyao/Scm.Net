using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_share_detail")]
    public class CmsDocShareDetailDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long unit_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }
    }
}
