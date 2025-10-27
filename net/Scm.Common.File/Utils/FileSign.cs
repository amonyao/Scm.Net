using System.Collections.Generic;

namespace Com.Scm.Utils
{
    public class FileSign
    {
        public FileType type { get; set; }
        public int offset { get; set; }
        public List<byte[]> sign { get; set; }

        public FileSign()
        {
        }

        public FileSign(FileType type, int offset, List<byte[]> sign)
        {
            this.type = type;
            this.offset = offset;
            this.sign = sign;
        }

        public static FileSign Doc = new FileSign(FileType.Doc, 0, new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } });
        public static FileSign Docx = new FileSign(FileType.Docx, 0, new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } });

        public static FileSign Bmp = new FileSign(FileType.Bmp, 0, new List<byte[]> { new byte[] { 0x42, 0x4D } });
        public static FileSign Gif = new FileSign(FileType.Gif, 0, new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } });
        public static FileSign Jfif = new FileSign(FileType.Jpg, 0, new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46 } });
        public static FileSign Jpeg = new FileSign(FileType.Jpg, 0, new List<byte[]>
                                    {
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                                    });
        public static FileSign Jpg = new FileSign(FileType.Jpg, 0, new List<byte[]>
                                    {
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                                    });
        public static FileSign Png = new FileSign(FileType.Png, 0, new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } });
        public static FileSign Wmp = new FileSign(FileType.Wmp, 0, new List<byte[]>());
        public static FileSign Webp = new FileSign(FileType.WebP, 0, new List<byte[]> { new byte[] { 82, 73, 70, 70 } });
    }
}
