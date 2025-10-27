using Com.Scm.Enums;

namespace Com.Scm.Dao
{
    public interface IDeleteDao : IUpdateDao
    {
        public long id { get; set; }

        public ScmDeleteEnum row_delete { get; set; }
    }
}
