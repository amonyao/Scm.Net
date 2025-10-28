namespace Com.Scm.Quartz
{
    public class JobResult
    {
        public bool status { get; set; }

        public string message { get; set; }
    }

    public class ResultData<T> where T : class
    {
        public int total { get; set; }
        public List<T> data { get; set; }
    }
}
