namespace Com.Scm.Express.My.Shipper.YTO
{
    public class ApiRequest : MyRequest
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// param格式(JSON/XML)
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 传递的参数
        /// </summary>
        public string param { get; set; }
    }
}
