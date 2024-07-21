using System.ComponentModel;

namespace Com.Scm.Express.Dto
{
    public enum GoodsTypeEnum
    {
        None = 0,
        [Description("文件")]
        Files = 1,
        [Description("服饰")]
        Clothes = 2,
        [Description("食品")]
        Foods = 3,
        [Description("生鲜")]
        Fresh = 4,
        [Description("数码产品")]
        Digital = 5,
        [Description("生活用品")]
        Commodity = 6,
        [Description("易碎品")]
        Fragile = 7,
        [Description("化妆品")]
        Cosmetics = 8,
        [Description("医药品")]
        Drugs = 9,
        [Description("其它")]
        Other = 10
    }

    public enum GoodsDDDEnum
    {
        battery,
        liquid,

    }
}
