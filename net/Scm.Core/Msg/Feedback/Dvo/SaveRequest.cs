namespace Com.Scm.Msg.Feedback.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveRequest : ScmRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string content { get; set; }
    }
}
