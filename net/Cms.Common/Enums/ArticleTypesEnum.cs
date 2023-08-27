using System.ComponentModel;

namespace Com.Scm.Cms.Enums
{
    public enum ArticleTypesEnum
    {
        None = 0,
        [Description("诗词歌赋")]
        Poetry = 10,
        [Description("读书笔记")]
        Litera = 20,
        [Description("楹联谚语")]
        Proverb = 30,
        [Description("字谜歇后")]
        Puzzle = 40,
        [Description("笔记")]
        Notes = 50
    }
}
