using System.ComponentModel;

namespace Com.Scm.Enums
{
    public enum FileTypeEnum
    {
        None = 0,
        [Description("字节文件")]
        Byte = 1,
        [Description("字符文件")]
        Text = 2,
        [Description("图像文件")]
        Image = 3,
        [Description("媒体文件")]
        Media = 4,
        [Description("办公文件")]
        Office = 5,
        [Description("压缩文件")]
        Archive = 6,
    }
}
