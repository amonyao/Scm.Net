namespace Com.Scm.Http
{
    public class ScmHttpParam
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否查询参数
        /// </summary>
        public bool IsGet { get; set; }
    }
}
