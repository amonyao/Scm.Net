using SixLabors.Fonts;
using SixLabors.Fonts.Write;

namespace Test
{
    public class WoffTest
    {
        public void Test()
        {
            // 创建一个字体实例  
            Font font = Font.FromGlyphs("ABC");

            // 创建一个 WOFF 写入器  
            using (Stream outputStream = new FileStream("output.woff", FileMode.Create, FileAccess.Write))
            {
                SixLabors.Fonts.
                // 创建 WOFF 写入器实例  
                WoffFontWriter writer = new WoffFontWriter(outputStream);

                // 写入字体到 WOFF 格式  
                writer.WriteFont(font);
            }
        }
    }
}
