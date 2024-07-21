using System.ComponentModel;

namespace Com.Scm.Express.Dto
{
    public enum ExpressTypeEnum
    {
        [Description("快递")]
        Kd,
        [Description("快运")]
        Ky,
        [Description("冷链")]
        Ll
    }
}
