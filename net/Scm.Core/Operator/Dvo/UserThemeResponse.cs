using Com.Scm.Api;
using Com.Scm.Cfg.UserTheme;

namespace Com.Scm.Operator.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class UserThemeResponse : ScmApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public UserThemeDto item { get; set; }
    }
}
