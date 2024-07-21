namespace Com.Scm.Express.My.Shipper.YUNDA
{
    public class YdResponse<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool result { get; set; }
        /// <summary>
        /// 响应编码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string message { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }

        public T data { get; set; }

        public virtual bool IsSuccess()
        {
            return result;
        }

        public virtual string GetMessage()
        {
            return message;
        }
    }
}
