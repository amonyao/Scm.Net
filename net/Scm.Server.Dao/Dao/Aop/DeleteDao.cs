using Com.Scm.Enums;

namespace Com.Scm.Dao.Aop
{
    public class DeleteDao : IDeleteDao
    {
        public long id { get; set; }

        public ScmDeleteEnum row_delete { get; set; }
        
        public long update_time { get; set; }
        public long update_user { get; set; }
    }
}
