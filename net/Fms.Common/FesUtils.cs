namespace Com.Scm.Fms
{
    public class FesUtils
    {
        private static long _Id;
        private static readonly object _lock = new object();

        public static long NextFileId()
        {
            if (_Id < 1)
            {
                lock (_lock)
                {
                    _Id = DateTime.Now.ToFileTimeUtc();
                }
            }
            return _Id++;
        }
    }
}
