using System.Collections.Generic;

namespace Com.Scm.Utils
{
    /// <summary>
    /// 文件签名
    /// </summary>
    public class FileSign
    {
        public FileType type { get; set; }
        public int offset { get; set; }
        public List<byte[]> sign { get; set; }

        public FileSign()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">文件类型</param>
        /// <param name="offset">偏移量</param>
        /// <param name="sign">文件签名</param>
        public FileSign(FileType type, int offset, List<byte[]> sign)
        {
            this.type = type;
            this.offset = offset;
            this.sign = sign;
        }

        public static FileSign Doc = new FileSign(FileType.Doc, 0, new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } });
        public static FileSign Docx = new FileSign(FileType.Docx, 0, new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } });
        public static FileSign Xls = new FileSign(FileType.Xls, 0, new List<byte[]>
                                {
                                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                                    new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
                                });
        public static FileSign Xlsx = new FileSign(FileType.Xlsx, 0, new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } });
        public static FileSign Csv = new FileSign(FileType.Csv, 0, new List<byte[]> { new byte[] { 0x49, 0x44 } });
        public static FileSign Pdf = new FileSign(FileType.Pdf, 0, new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } });

        public static FileSign Bmp = new FileSign(FileType.Bmp, 0, new List<byte[]> { new byte[] { 0x42, 0x4D } });
        public static FileSign Gif = new FileSign(FileType.Gif, 0, new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } });
        public static FileSign Jpg = new FileSign(FileType.Jpg, 0, new List<byte[]> { new byte[] { 0xFF, 0xD8 } });
        //public static FileSign Jpg = new FileSign(FileType.Jpg, 0, new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46 } });
        //public static FileSign Jpeg = new FileSign(FileType.Jpg, 0, new List<byte[]>
        //                            {
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
        //                            });
        //public static FileSign Jpg = new FileSign(FileType.Jpg, 0, new List<byte[]>
        //                            {
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
        //                                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
        //                            });
        public static FileSign Png = new FileSign(FileType.Png, 0, new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } });
        public static FileSign Wmp = new FileSign(FileType.Wmp, 0, new List<byte[]>());
        public static FileSign Webp = new FileSign(FileType.WebP, 0, new List<byte[]> { new byte[] { 82, 73, 70, 70 } });

        public static FileSign Avi = new FileSign(FileType.Avi, 0, new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46, 0x84, 0x4A, 0x1E, 0x00, 0x41, 0x56, 0x49 } });
        public static FileSign Flv = new FileSign(FileType.Flv, 0, new List<byte[]> { new byte[] { 0x46, 0x4C, 0x56 } });
        public static FileSign Mp4 = new FileSign(FileType.Mp4, 0, new List<byte[]>
                                {
                                    new byte[] { 0x00,0x00,0x00,0x18,0x66,0x74,0x79,0x70,0x6D,0x70,0x34,0x32 },
                                    new byte[] { 0x00,0x00,0x00,0x1C,0x66,0x74,0x79,0x70,0x6D,0x70,0x34,0x32 },
                                    new byte[] { 0x00,0x00,0x00,0x20,0x66,0x74,0x79,0x70,0x69,0x73,0x6F, 0x6D },
                                });
        public static FileSign M4v = new FileSign(FileType.M4v, 0, new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56 } });
        public static FileSign Mkv = new FileSign(FileType.Mkv, 0, new List<byte[]> { new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0xA3, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7 } });
        public static FileSign Mov = new FileSign(FileType.Mov, 0, new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74 } });

        public static FileSign WebM = new FileSign(FileType.WebM, 0, new List<byte[]> { new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0x9F, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7, 0x81, 0x01, 0x42, 0xF2 } });
        public static FileSign Wmv = new FileSign(FileType.Wmv, 0, new List<byte[]> { new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 } });

        public static FileSign Zip = new FileSign(FileType.Zip, 0, new List<byte[]>
                                {
                                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 },
                                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 }
                                });
    }
}
