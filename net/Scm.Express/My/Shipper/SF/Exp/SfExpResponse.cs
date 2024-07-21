namespace Com.Scm.Express.My.Shipper.SF.Exp
{
    public class SfExpResponse<T> : SfResponse
    {
        /// <summary>
        /// true 请求成功，false 请求失败
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误编码，S0000成功
        /// </summary>
        public string errorCode { get; set; }

        /// <summary>
        /// errorMsg
        /// </summary>
        public string errorMsg { get; set; }

        /// <summary>
        /// 返回的详细数据
        /// </summary>
        public T msgData { get; set; }

        public override bool IsSuccess()
        {
            return success;
        }

        public override string GetMessage()
        {
            return errorMsg;
        }
    }
}
