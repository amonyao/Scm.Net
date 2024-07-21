namespace Com.Scm.Express.My.Shipper.SF
{
    public class SfResponse
    {
        /// <summary>
        /// 统一接入平台校验成功，调用后端服务成功;
        /// 注意：
        /// 不代表后端业务处理成功，实际业务处理结果，
        /// 需要查看响应属性apiResultData中的详细结果
        /// </summary>
        public const string API_RESULT_CODE_A1000 = "A1000";
        /// <summary>
        /// 必传参数不可为空
        /// </summary>
        public const string API_RESULT_CODE_A1001 = "A1001";
        /// <summary>
        /// 请求时效已过期
        /// </summary>
        public const string API_RESULT_CODE_A1002 = "A1002";
        /// <summary>
        /// IP无效
        /// </summary>
        public const string API_RESULT_CODE_A1003 = "A1003";
        /// <summary>
        /// 无对应服务权限
        /// </summary>
        public const string API_RESULT_CODE_A1004 = "A1004";
        /// <summary>
        /// 流量受控
        /// </summary>
        public const string API_RESULT_CODE_A1005 = "A1005";
        /// <summary>
        /// 数字签名无效
        /// </summary>
        public const string API_RESULT_CODE_A1006 = "A1006";
        /// <summary>
        /// 重复请求
        /// </summary>
        public const string API_RESULT_CODE_A1007 = "A1007";
        /// <summary>
        /// 数据解密失败
        /// </summary>
        public const string API_RESULT_CODE_A1008 = "A1008";
        /// <summary>
        /// 目标服务异常或不可达
        /// </summary>
        public const string API_RESULT_CODE_A1009 = "A1009";
        /// <summary>
        /// 状态为沙箱测试
        /// </summary>
        public const string API_RESULT_CODE_A1010 = "A1010";
        /// <summary>
        /// 系统异常
        /// </summary>
        public const string API_RESULT_CODE_A1099 = "A1099";

        public string apiResultCode { get; set; }
        public string apiErrorMsg { get; set; }
        public string apiResponseID { get; set; }
        public string apiResultData { get; set; }

        public virtual bool IsSuccess()
        {
            return API_RESULT_CODE_A1000 == apiResultCode;
        }

        public virtual string GetMessage()
        {
            return apiErrorMsg;
        }
    }
}
