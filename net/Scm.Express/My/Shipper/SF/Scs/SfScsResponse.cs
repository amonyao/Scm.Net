namespace Com.Scm.Express.My.Shipper.SF.Scs
{
    public class SfScsResponse<T> : SfResponse
    {
        /// <summary>
        /// true 请求成功，false 请求失败
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误编码，S0000成功
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// errorMsg
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 返回的详细数据
        /// </summary>
        public T data { get; set; }

        public override bool IsSuccess()
        {
            return success;
        }

        public override string GetMessage()
        {
            return message;
        }
    }
}
