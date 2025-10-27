using Com.Scm.Enums;

namespace Com.Scm.Dao.Aop
{
    public class StatusDao : IStatusDao
    {
        public long id { get; set; }

        public ScmRowStatusEnum row_status { get; set; }

        public long update_time { get; set; }
        public long update_user { get; set; }
    }
}
