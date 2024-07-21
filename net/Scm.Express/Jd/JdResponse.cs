namespace Com.Scm.Express.JdExpress
{
    public class JdResponse<T>
    {
        public int statusCode { get; set; }
        public string statusMessage { get; set; }

        public T data { get; set; }

        public bool IsSuccess()
        {
            return statusCode == 0;
        }

        public string GetMessage()
        {
            return statusMessage;
        }
    }
}
