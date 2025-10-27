using Com.Scm.Enums;

namespace Com.Scm.Dao.Aop
{
    public class SystemDao : ISystemDao
    {
        public long id { get; set; }

        public ScmSystemEnum row_system { get; set; }

        public long update_time { get; set; }
        public long update_user { get; set; }
    }
}
