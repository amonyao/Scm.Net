using Com.Scm.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Com.Scm.Utils
{
    /// <summary>
    /// 文件公共方法
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// 目录分隔符
        /// windows "\" OSX and Linux  "/"
        /// </summary>
        private static char _directorySeparatorChar = Path.DirectorySeparatorChar;
        /// <summary>
        /// 包含应用程序的目录的绝对路径
        /// </summary>
        private static string _contentRootPath = $@"{AppContext.BaseDirectory}\";

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

        public static string ToMachinePath(string uri)
        {
            return uri.Replace('/', _directorySeparatorChar);
        }

        public static string ToVirtualPath(string path, string basePath)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(basePath))
            {
                return path;
            }

            if (basePath[basePath.Length - 1] == _directorySeparatorChar)
            {
                basePath = basePath.Substring(0, basePath.Length - 1);
            }
            return path.Replace(basePath, "").Replace(_directorySeparatorChar, '/');
        }

        #region 检测指定路径是否存在
        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">目录的绝对路径</param> 
        public static bool IsExistDirectory(string path)
        {
            return Directory.Exists(path);
        }
        #endregion

        #region 检测指定文件是否存在,如果存在则返回true
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>  
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

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

        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
        public static void Moveto(string sourceFilePath, string descDirectoryPath)
        {
            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除
                if (IsExistFile(descDirectoryPath + "\\" + sourceFilePath))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFilePath);
                }
                else
                {
                    //将文件移动到指定目录
                    File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFilePath);
                }
            }
        }

        /// <summary>
        /// 将来源目录的内容复制到目标文件中
        /// </summary>
        /// <param name="srcDir">来源目录的绝对路径</param>
        /// <param name="dstDir">目标目录的绝对路径</param>
        public static int Copyto(DirectoryInfo srcDir, DirectoryInfo dstDir)
        {
            if (!srcDir.Exists)
            {
                return 0;
            }

            if (!dstDir.Exists)
            {
                dstDir.Create();
            }

            var qty = 0;
            foreach (var src in srcDir.GetDirectories())
            {
                var dst = new DirectoryInfo(Path.Combine(dstDir.FullName, src.Name));
                qty += Copyto(src, dst);
            }

            foreach (var srcFile in srcDir.GetFiles())
            {
                srcFile.CopyTo(Path.Combine(dstDir.FullName, srcFile.Name), true);
                qty += 1;
            }

            return qty;
        }

        public static string ReadText(string file)
        {
            using (var stream = new StreamReader(file))
            {
                return stream.ReadToEnd();
            }
        }

        #region 删除指定文件
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
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
            if (IsExistDirectory(directoryPath))
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

        /// <summary>
        /// 剪切文件
        /// </summary>
        /// <param name="source">原路径</param> 
        /// <param name="destination">新路径</param> 
        public bool FileMove(string source, string destination)
        {
            bool ret = false;
            FileInfo file_s = new FileInfo(source);
            FileInfo file_d = new FileInfo(destination);
            if (file_s.Exists)
            {
                if (!file_d.Exists)
                {
                    file_s.MoveTo(destination);
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        ///  文件大小转为B、KB、MB、GB...
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToFileSize(long size)
        {
            var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i = 0;
            while (size > 0)
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

        #region 获得文件目录
        public static List<ScmFolderInfo> GetFolders(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                return null;
            }

            return GetFolders(dir, "");
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public static List<ScmFolderInfo> GetFolders(DirectoryInfo dir, string baseUri)
        {
            if (dir == null || !dir.Exists)
            {
                return null;
            }

            var list = new List<ScmFolderInfo>();
            foreach (var sub in dir.GetDirectories())
            {
                var folder = new ScmFolderInfo();
                folder.Name = sub.Name;
                folder.Uri = baseUri + "/" + sub.Name;
                folder.Children = GetFolders(sub, folder.Uri);
                list.Add(folder);
            }
            return list;
        }
        #endregion

        #region 获得文件目录下的文件，支持子目录
        public static List<ScmFileInfo> GetFiles(string path, FileTypeEnum type, string basePath)
        {
            var list = new List<ScmFileInfo>();
            var folder = new DirectoryInfo(path);
            if (folder.Exists)
            {
                foreach (var file in folder.GetFiles())
                {
                    var exts = file.Extension;
                    if (string.IsNullOrWhiteSpace(exts))
                    {
                        continue;
                    }

                    exts = exts.Trim().ToLower();
                    if (type == FileTypeEnum.None)
                    {
                        if (IsByteFile(exts))
                        {
                            list.Add(GetFileInfo(file, FileTypeEnum.Byte, basePath));
                            continue;
                        }
                        if (IsTextFile(exts))
                        {
                            list.Add(GetFileInfo(file, FileTypeEnum.Text, basePath));
                            continue;
                        }
                        if (IsImageFile(exts))
                        {
                            list.Add(GetFileInfo(file, FileTypeEnum.Image, basePath));
                            continue;
                        }
                        if (IsMediaFile(exts))
                        {
                            list.Add(GetFileInfo(file, FileTypeEnum.Media, basePath));
                            continue;
                        }
                        if (IsOfficeFile(exts))
                        {
                            list.Add(GetFileInfo(file, FileTypeEnum.Office, basePath));
                            continue;
                        }

                        list.Add(GetFileInfo(file, type, basePath));
                        continue;
                    }

                    if (type == FileTypeEnum.Byte && IsByteFile(exts))
                    {
                        list.Add(GetFileInfo(file, FileTypeEnum.Byte, basePath));
                        continue;
                    }
                    if (type == FileTypeEnum.Text && IsTextFile(exts))
                    {
                        list.Add(GetFileInfo(file, FileTypeEnum.Text, basePath));
                        continue;
                    }
                    if (type == FileTypeEnum.Image && IsImageFile(exts))
                    {
                        list.Add(GetFileInfo(file, FileTypeEnum.Image, basePath));
                        continue;
                    }
                    if (type == FileTypeEnum.Media && IsMediaFile(exts))
                    {
                        list.Add(GetFileInfo(file, FileTypeEnum.Media, basePath));
                        continue;
                    }
                    if (type == FileTypeEnum.Office && IsOfficeFile(exts))
                    {
                        list.Add(GetFileInfo(file, FileTypeEnum.Office, basePath));
                        continue;
                    }
                }
            }
            return list;
        }

        private static ScmFileInfo GetFileInfo(FileInfo file, FileTypeEnum type, string basePath)
        {
            var item = new ScmFileInfo();
            item.Type = type;
            item.Name = file.Name;
            //item.FullName = file.FullName;
            item.Extension = file.Extension;
            item.Length = file.Length;
            //item.FileSize = ToFileSize(file.Length);
            item.Uri = ToVirtualPath(file.FullName, basePath);
            item.CreationTime = file.CreationTime;
            item.LastWriteTime = file.LastWriteTime;
            item.LastAccessTime = file.LastAccessTime;
            return item;
        }
        #endregion
    }

    /// <summary>
    /// 文件目录
    /// </summary>
    public class ScmFolderInfo
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public List<ScmFolderInfo> Children { get; set; }
    }

    /// <summary>
    /// 文件内容
    /// </summary>
    public class ScmFileInfo
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileTypeEnum Type { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件物理路径
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 原始大小（字节）
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 文件虚拟路径
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileStyle { get; set; }
        /// <summary>
        /// 文件图标
        /// </summary>
        public string FileIcon { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastWriteTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }
    }

    /// <summary>
    /// 远程文件下载
    /// </summary>
    public class HttpDldFile
    {
        /// <summary>
        /// Http方式下载文件
        /// </summary>
        /// <param name="url">http地址</param>
        /// <param name="localfile">本地文件</param>
        /// <returns></returns>
        public bool Download(string url, string localfile)
        {
            bool flag = false;
            long startPosition = 0; // 上次下载的文件起始位置
            FileStream writeStream; // 写入本地文件流对象

            long remoteFileLength = GetHttpLength(url);// 取得远程文件长度
                                                       //System.Console.WriteLine("remoteFileLength=" + remoteFileLength);
            if (remoteFileLength == 745)
            {
                LogUtils.Info("远程文件不存在.");
                return false;
            }

            // 判断要下载的文件夹是否存在
            if (File.Exists(localfile))
            {

                writeStream = File.OpenWrite(localfile);             // 存在则打开要下载的文件
                startPosition = writeStream.Length;                  // 获取已经下载的长度

                if (startPosition >= remoteFileLength)
                {
                    LogUtils.Info("本地文件长度" + startPosition + "已经大于等于远程文件长度" + remoteFileLength);
                    writeStream.Close();

                    return false;
                }
                else
                {
                    writeStream.Seek(startPosition, SeekOrigin.Current); // 本地文件写入位置定位
                }
            }
            else
            {
                writeStream = new FileStream(localfile, FileMode.Create);// 文件不保存创建一个文件
                startPosition = 0;
            }


            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接

                if (startPosition > 0)
                {
                    myRequest.AddRange((int)startPosition);// 设置Range值,与上面的writeStream.Seek用意相同,是为了定义远程文件读取位置
                }


                Stream readStream = myRequest.GetResponse().GetResponseStream();// 向服务器请求,获得服务器的回应数据流


                byte[] btArray = new byte[512];// 定义一个字节数据,用来向readStream读取内容和向writeStream写入内容
                int contentSize = readStream.Read(btArray, 0, btArray.Length);// 向远程文件读第一次

                long currPostion = startPosition;

                while (contentSize > 0)// 如果读取长度大于零则继续读
                {
                    currPostion += contentSize;
                    int percent = (int)(currPostion * 100 / remoteFileLength);
                    LogUtils.Info("percent=" + percent + "%");

                    writeStream.Write(btArray, 0, contentSize);// 写入本地文件
                    contentSize = readStream.Read(btArray, 0, btArray.Length);// 继续向远程文件读取
                }

                //关闭流
                writeStream.Close();
                readStream.Close();

                flag = true;        //返回true下载成功
            }
            catch (Exception)
            {
                writeStream.Close();
                flag = false;       //返回false下载失败
            }

            return flag;
        }

        // 从文件头得到远程文件的长度
        private static long GetHttpLength(string url)
        {
            long length = 0;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

            if (rsp.StatusCode == HttpStatusCode.OK)
            {
                length = rsp.ContentLength;// 从文件头得到远程文件的长度
            }

            rsp.Close();
            return length;
        }
    }
}