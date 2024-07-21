using System.ComponentModel;

namespace Com.Scm.Express.My.Shipper.SF.Exp.Model
{
    public enum WaybillType
    {
        None = 0,
        [Description("母单")]
        A = 1,
        [Description("子单")]
        B = 2,
        [Description("签回单")]
        C = 3
    }
}
