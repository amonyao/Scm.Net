using System.ComponentModel;

namespace Com.Scm.Cms.Enums
{
    public enum VisibleEnum
    {
        None = 0,
        [Description("公开")]
        Public,
        [Description("排除")]
        Exclude,
        [Description("包含")]
        Include,
        [Description("私有")]
        Private
    }
}
