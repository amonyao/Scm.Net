using System.ComponentModel;

namespace Com.Scm.Pos.Enums
{
    public enum SpuTypesEnums
    {
        None = 0,
        [Description("实物商品")]
        A = 1,
        [Description("虚拟商品")]
        B = 2
    }

    public enum SpuShowEnums
    {
        None = 0,
        Show = 1,
        Hide = 2
    }

    public enum SpuSaleEnums
    {
        None = 0,
        OnSale = 1,
        Unsale = 2
    }

    public enum SpuAssEnums
    {
        None = 0,
        [Description("退款")]
        Refund = 1,
        [Description("退货")]
        Return = 2,
        [Description("换货")]
        Exchange = 3
    }
}
