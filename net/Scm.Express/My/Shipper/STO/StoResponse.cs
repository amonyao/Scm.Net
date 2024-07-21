using System;

namespace Com.Scm.Express.My.Shipper.STO
{
    public class StoResponse<T>
    {
        public string success { get; set; }

        public string needRetry { get; set; }

        public string errorCode { get; set; }

        public string errorMsg { get; set; }

        public string requestId { get; set; }

        public T data { get; set; }

        public virtual bool IsSuccess()
        {
            return "true".Equals(success, StringComparison.CurrentCultureIgnoreCase);
        }

        public virtual string GetMessage()
        {
            return errorMsg;
        }

        public bool NeedRetry()
        {
            return "true".Equals(needRetry, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
