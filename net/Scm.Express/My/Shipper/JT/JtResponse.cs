namespace Com.Scm.Express.My.Shipper.JT
{
    public class JtResponse<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }

        public virtual bool IsSuccess()
        {
            return 1 == code;
        }

        public virtual string GetMessage()
        {
            return msg;
        }
    }
}
