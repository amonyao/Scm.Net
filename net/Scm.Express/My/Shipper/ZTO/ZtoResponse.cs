namespace Com.Scm.Express.My.Shipper.ZTO
{
    public class ZtoResponse<T>
    {
        public bool status { get; set; }
        public string statusCode { get; set; }
        public string message { get; set; }
        public T result { get; set; }

        public virtual bool IsSuccess()
        {
            return status;
        }

        public virtual string GetMessage()
        {
            return message;
        }
    }
}
