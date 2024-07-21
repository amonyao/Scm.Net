using System.ComponentModel;

namespace Com.Scm.Express.Dto
{
    public enum EnvironmentEnum
    {
        [Description("开发环境")]
        Debug,
        [Description("沙箱环境")]
        Sandbox,
        [Description("生产环境")]
        Release
    }
}
