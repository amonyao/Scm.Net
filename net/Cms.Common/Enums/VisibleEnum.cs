using System.ComponentModel;

namespace Com.Scm.Cms.Enums
{
    public enum VisibleEnum
    {
        None = 0,
        [Description("私有")]
        Private = 10,
        [Description("公开")]
        Public = 20,
        [Description("包含机构")]
        ByUnitInclude = 31,
        [Description("排除机构")]
        ByUnitExclude = 32,
        [Description("包含人员")]
        ByUserInclude = 41,
        [Description("排除人员")]
        ByUserExclude = 42,
    }
}
