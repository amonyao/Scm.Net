namespace Com.Scm.Express.My.Shipper.SF.Exp.Auth
{
    public class AuthResponse : ScmResponse
    {
        /// <summary>
        /// 响应ID
        /// </summary>
        public string apiResponseID { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        public string apiResultCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string apiErrorMsg { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// accessToken访问令牌过期时间，单位（秒）,默认7200秒。从申请成功后，开始倒计时7200s,令牌过期后（即expiresIn=0）需要重新获取
        /// </summary>
        public long? expiresIn { get; set; }

        public override bool IsSuccess()
        {
            return "A1000" == apiResultCode;
        }

        public override string GetMessage()
        {
            return apiErrorMsg;
        }
    }
}
