namespace Com.Scm.Dao.Aop
{
    public class SortableDao : ISortableDao
    {
        public long id { get; set; }
        public int od { get; set; }

        public long update_time { get; set; }
        public long update_user { get; set; }
    }
}
