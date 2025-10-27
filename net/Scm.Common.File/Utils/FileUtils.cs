using System;
using System.Collections.Generic;
using System.IO;

namespace Com.Scm.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// 字节文件后缀
        /// </summary>
        private static List<string> ByteExts = new List<string> { ".bin", ".dat" };
        /// <summary>
        /// 字符文件后缀
        /// </summary>
        private static List<string> TextExts = new List<string> { ".txt", ".log", ".css", ".html", ".htm", ".js", ".xml", ".json", ".cs", ".java", ".py" };
        /// <summary>
        /// 代码文件后缀
        /// </summary>
        private static List<string> CodeExts = new List<string> { ".css", ".html", ".htm", ".js", ".xml", ".json", ".cs", ".java", ".py" };
        /// <summary>
        /// 图像文件后缀
        /// </summary>
        private static List<string> ImageExts = new List<string> { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".webp" };
        /// <summary>
        /// 媒体文件后缀
        /// </summary>
        private static List<string> MeidaExts = new List<string> { ".avi", ".wma", ".wmv", ".mp3", ".mp4", ".webm" };
        /// <summary>
        /// 
        /// </summary>
        private static List<string> OfficeExts = new List<string> { ".xls", ".xlsx", ".doc", ".docx", ".ppt", ".pptx", ".pdf" };

        /// <summary>
        /// 压缩文件后缀
        /// </summary>
        private static List<string> ArchiveExts = new List<string> { ".zip", ".rar", ".7z", ".gzip" };

        public static bool IsByteFile(string fileExt)
        {
            return ByteExts.Contains(fileExt.ToLower());
        }

        public static bool IsTextFile(string fileExt)
        {
            return TextExts.Contains(fileExt.ToLower());
        }

        public static bool IsImageFile(string fileExt)
        {
            return ImageExts.Contains(fileExt.ToLower());
        }

        public static bool IsMediaFile(string fileExt)
        {
            return MeidaExts.Contains(fileExt.ToLower());
        }

        public static bool IsOfficeFile(string fileExt)
        {
            return OfficeExts.Contains(fileExt.ToLower());
        }

        public static bool IsArchiveFile(string fileExt)
        {
            return ArchiveExts.Contains(fileExt.ToLower());
        }

        public static string GetMimeByExt(string ext)
        {
            var sub = ext;
            if (sub[0] == '.')
            {
                sub = sub.Substring(1);
            }
            if (IsImageFile(ext))
            {
                return "image/" + sub;
            }
            if (IsTextFile(ext))
            {
                return "plain/text";
            }

            return "";
        }

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath">文件夹的绝对路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
        #endregion

        #region 删除指定文件
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #region 删除指定目录及其所有子目录
        /// <summary>
        /// 删除指定目录及其所有子目录
        /// </summary>
        /// <param name="directoryPath">文件的绝对路径</param>
        public static void DeleteFolder(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public static bool IsEmptyFolder(string path)
        {
            var dir = new DirectoryInfo(path);
            return dir.GetFiles().Length < 1 && dir.GetDirectories().Length < 1;
        }
        #endregion

        public static string ToFileUom(long size)
        {
            var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i = 0;
            while (size > 1024)
            {
                size = size >> 10;
                i++;
            }
            return size + units[i];
        }

        public static long GetFileSize(string file)
        {
            var info = new FileInfo(file);
            if (!info.Exists)
            {
                return 0;
            }

            return info.Length;
        }

        public static void FileRename(string srcFile, string dstFile)
        {
            if (File.Exists(dstFile))
            {
                File.Delete(dstFile);
            }

            new FileInfo(srcFile).MoveTo(dstFile);
        }

        public static string ReadText(string file)
        {
            using (var stream = new StreamReader(file))
            {
                return stream.ReadToEnd();
            }
        }

        public static string Md5(Stream stream)
        {
            stream.Position = 0;

            var alg = System.Security.Cryptography.MD5.Create();
            var result = alg.ComputeHash(stream);
            return TextUtils.ToHexString(result);
        }

        public static string Md5(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var alg = System.Security.Cryptography.MD5.Create();
                var result = alg.ComputeHash(stream);
                return TextUtils.ToHexString(result);
            }
        }

        public static bool Copyto(string src, string dst)
        {
            if (File.Exists(src))
            {
                return CopyFile(src, dst);
            }

            if (!Directory.Exists(src))
            {
                return false;
            }

            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            var dirs = Directory.GetDirectories(src);
            foreach (var dir in dirs)
            {
                var tmp = Path.Combine(dst, Path.GetFileName(dir));
                Copyto(dir, tmp);
            }

            var files = Directory.GetFiles(src);
            foreach (var file in files)
            {
                var tmp = Path.Combine(dst, Path.GetFileName(file));
                if (!CopyFile(file, tmp))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CopyFile(string src, string dst)
        {
            var dir = Path.GetDirectoryName(dst);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.Copy(src, dst, true);
            return true;
        }

        public static bool Moveto(string src, string dst)
        {
            if (File.Exists(src))
            {
                return MoveFile(src, dst);
            }

            if (!Directory.Exists(src))
            {
                return false;
            }

            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            Directory.Move(src, dst);

            var dirs = Directory.GetDirectories(src);
            foreach (var dir in dirs)
            {
                var tmp = Path.Combine(dst, Path.GetFileName(dir));
                Moveto(dir, tmp);
            }

            var files = Directory.GetFiles(src);
            foreach (var file in files)
            {
                var tmp = Path.Combine(dst, Path.GetFileName(file));
                if (!MoveFile(file, tmp))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool MoveFile(string src, string dst)
        {
            if (File.Exists(dst))
            {
                File.Delete(dst);
            }

            var dir = Path.GetDirectoryName(dst);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.Move(src, dst);
            return true;
        }

        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }


        /// <summary>
        /// 获取重复后的文件名，返回格式：xxxx (nnnn).ext
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetValidName(string path, string file)
        {
            var idx = 0;
            var name = Path.GetFileNameWithoutExtension(file);
            var exts = Path.GetExtension(file);
            var tmp = "";
            while (idx < 1000000)
            {
                idx += 1;
                tmp = $"{name} ({idx}){exts}";
                if (File.Exists(tmp))
                {
                    continue;
                }
                break;
            }

            return tmp;
        }

        public static string ToHtmlBase64(string file)
        {
            var pre = "";
            var ext = Path.GetExtension(file)?.ToLower();
            if (ext == ".jpg" || ext == ".jpeg")
            {
                pre = "data:image/jpeg;base64,";
            }
            else if (ext == ".png")
            {
                pre = "data:image/png;base64,";
            }
            else if (ext == ".aac")
            {
                pre = "data:audio/aac;base64,";
            }
            else if (ext == ".mp3")
            {
                pre = "data:audio/mpeg;base64,";
            }
            else if (ext == ".mp4")
            {
                pre = "data:video/mp4;base64,";
            }
            else if (ext == ".wav")
            {
                pre = "data:audio/x-wav;base64,";
            }
            using (var stream = File.OpenRead(file))
            {
                var bytes = new byte[stream.Length];
                var len = stream.Read(bytes, 0, bytes.Length);
                return pre + Convert.ToBase64String(bytes, 0, len);
            }
        }

        private static Dictionary<string, bool> _ZipExts;

        public static bool IsZip(string ext)
        {
            if (_ZipExts == null)
            {
                _ZipExts = new Dictionary<string, bool>();
                _ZipExts[".zip"] = true;
                _ZipExts[".gzip"] = true;
                _ZipExts[".bzip2"] = true;
                _ZipExts[".tar"] = true;
                _ZipExts[".7z"] = true;
            }

            if (string.IsNullOrWhiteSpace(ext))
            {
                return false;
            }

            if (ext[0] != '.')
            {
                ext = '.' + ext;
            }
            return _ZipExts.ContainsKey(ext.ToLower());
        }

        public static FileType GetFileType(Stream stream)
        {
            stream.Position = 0;

            var len = 160;
            var bytes = new byte[160];
            if (stream.Length < len)
            {
                len = (int)stream.Length;
            }
            stream.Read(bytes, 0, len);

            var dict = InitFileTypes();
            foreach (var fileType in dict.Keys)
            {
                var signList = dict[fileType];
                foreach (var sign in signList)
                {
                    if (IsMatch(bytes, sign, 0))
                    {
                        return fileType;
                    }
                }
            }

            return FileType.UnKnown;
        }

        public static bool IsMathType(Stream stream, List<FileSign> fileSigns)
        {
            stream.Position = 0;

            var len = 160;
            var bytes = new byte[160];
            if (stream.Length < len)
            {
                len = (int)stream.Length;
            }
            stream.Read(bytes, 0, len);

            foreach (var fileSign in fileSigns)
            {
                foreach (var sign in fileSign.sign)
                {
                    if (IsMatch(bytes, sign, fileSign.offset))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsMatch(byte[] src, byte[] dst, int srcIndex)
        {
            if (srcIndex >= src.Length - dst.Length - 1)
            {
                return false;
            }

            for (int i = 0; i < dst.Length; i++)
            {
                if (src[srcIndex + i] != dst[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static Dictionary<FileType, List<byte[]>> InitFileTypes()
        {
            var list = new Dictionary<FileType, List<byte[]>>();
            list[FileType.Doc] = new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } };
            list[FileType.Docx] = new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } };
            list[FileType.Pdf] = new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } };
            list[FileType.Zip] = new List<byte[]>
                                {
                                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 },
                                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 }
                                };
            list[FileType.Png] = new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } };
            list[FileType.Jpg] = new List<byte[]> { new byte[] { 0xFF, 0xD8 } };
            //{
            //    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            //    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            //    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
            //};
            list[FileType.Gif] = new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } };
            list[FileType.Bmp] = new List<byte[]> { new byte[] { 0x42, 0x4D } };
            list[FileType.Mp4] = new List<byte[]>
                                {
                                    new byte[] { 0x00,0x00,0x00,0x18,0x66,0x74,0x79,0x70,0x6D,0x70,0x34,0x32 },
                                    new byte[] { 0x00,0x00,0x00,0x1C,0x66,0x74,0x79,0x70,0x6D,0x70,0x34,0x32 },
                                    new byte[] { 0x00,0x00,0x00,0x20,0x66,0x74,0x79,0x70,0x69,0x73,0x6F, 0x6D },
                                };
            list[FileType.Mkv] = new List<byte[]> { new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0xA3, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7 } };
            list[FileType.Mov] = new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74 } };
            list[FileType.M4v] = new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56 } };
            list[FileType.WebM] = new List<byte[]> { new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0x9F, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7, 0x81, 0x01, 0x42, 0xF2 } };
            list[FileType.Wmv] = new List<byte[]> { new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 } };
            list[FileType.Avi] = new List<byte[]> { new byte[] { 0x52, 0x49, 0x46, 0x46, 0x84, 0x4A, 0x1E, 0x00, 0x41, 0x56, 0x49 } };
            list[FileType.Flv] = new List<byte[]> { new byte[] { 0x46, 0x4C, 0x56 } };
            list[FileType.Xls] = new List<byte[]>
                                {
                                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                                    new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
                                };
            list[FileType.Xlsx] = new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } };
            list[FileType.Csv] = new List<byte[]> { new byte[] { 0x49, 0x44 } };
            return list;
        }
    }
}
