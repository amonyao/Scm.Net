using Com.Scm.Image;
using Com.Scm.Image.Enums;
using Com.Scm.Image.SkiaSharp;
using Com.Scm.Utils;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Com.Scm.Cms.Fav.Uri
{
    public class FaviconReader
    {
        public string FileName { get; private set; }
        private string _Path;
        private string _Host;
        private string _File;

        public async Task<bool> Read(string filePath, string url)
        {
            return await Read(filePath, GetUri(url));
        }

        public async Task<bool> Read(string filePath, System.Uri uri)
        {
            if (uri == null)
            {
                return false;
            }

            _Path = filePath;
            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }

            FileName = uri.Host + ".png";
            _File = Path.Combine(_Path, FileName);
            //if (File.Exists(_File))
            //{
            //    return true;
            //}

            _Host = GetHost(uri);
            var result = await ReadIcon(_Host, "/favicon.ico");
            if (result)
            {
                return true;
            }

            var html = await ReadHtml(uri);
            if (string.IsNullOrEmpty(html))
            {
                return false;
            }

            var regex = new Regex("<link\\b[^>]*/?>", RegexOptions.IgnoreCase);
            var match = regex.Match(html);
            while (match.Success)
            {
                result = await ReadMatch(match);
                if (result)
                {
                    return true;
                }
                match = match.NextMatch();
            }

            return false;
        }

        private async Task<bool> ReadMatch(Match match)
        {
            var tag = match.Value;
            if (string.IsNullOrWhiteSpace(tag))
            {
                return false;
            }
            tag = tag.ToLower();
            if (!tag.EndsWith("/>"))
            {
                tag = tag.Substring(0, tag.Length - 1) + "/>";
            }

            var link = TextUtils.AsXmlObject<LinkTag>(tag);
            if (link == null)
            {
                return false;
            }
            var rel = link.rel ?? "";
            if (!rel.EndsWith("icon"))
            {
                return false;
            }

            var path = link.href;
            //return await ReadIcon(_Host, path, GetFormat(link.type, path));
            return await ReadIcon(_Host, path);
        }

        private ImageFormat GetFormat(string type, string path)
        {
            var text = type ?? "";
            if (text.EndsWith("png"))
            {
                return ImageFormat.Png;
            }

            if (text.EndsWith("jpg"))
            {
                return ImageFormat.Jpg;
            }

            if (text.EndsWith("bmp"))
            {
                return ImageFormat.Bmp;
            }

            text = path ?? "";
            if (text.EndsWith("png"))
            {
                return ImageFormat.Png;
            }

            if (text.EndsWith("jpg"))
            {
                return ImageFormat.Jpg;
            }

            if (text.EndsWith("bmp"))
            {
                return ImageFormat.Bmp;
            }

            return ImageFormat.Ico;
        }

        private System.Uri GetUri(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            if (url.StartsWith("//"))
            {
                url = "http:" + url;
            }
            if (url.IndexOf("://") < 0)
            {
                url = "http://" + url;
            }

            return new System.Uri(url);
        }

        private string GetHost(System.Uri uri)
        {
            return uri.Scheme + "://" + uri.Host;
        }

        private string CombineUrl(string host, string path)
        {
            if (host.EndsWith('/'))
            {
                host = host.Substring(0, host.Length - 1);
            }
            return host + path;
        }

        private bool IsPath2Host(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            return url.IndexOf("://") > 0;
        }

        private string TrimUrl(string url)
        {
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }
            return url;
        }

        private async Task<bool> ReadIcon(string url, string path)
        {
            path = TrimUrl(path);

            if (IsPath2Host(path))
            {
                url = path;
            }
            else
            {
                url = CombineUrl(url, path);
            }

            var client = new HttpClient();
            try
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    byte[] buff = new byte[1024];
                    int length = 0;
                    using (MemoryStream memory = new MemoryStream())
                    {
                        while ((length = stream.Read(buff, 0, buff.Length)) > 0)
                        {
                            memory.Write(buff, 0, length);
                        }

                        memory.Position = 0;
                        var engine = new ImageEngine();
                        var image = engine.Load(memory);
                        if (image == null)
                        {
                            return false;
                        }

                        var frame = GetFrame(image);
                        if (frame == null)
                        {
                            return false;
                        }

                        frame.Save(_File, Image.Enums.ImageFormat.Png);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private ScmFrame GetFrame(ScmImage image)
        {
            if (image == null)
            {
                return null;
            }

            if (image.Count > 0)
            {
                var frame = image.GetFrameBySize(16);
                if (frame != null)
                {
                    return frame;
                }
            }

            return image.GetFrame(0);
        }

        private async Task<string> ReadHtml(System.Uri uri)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0");
                return await client.GetStringAsync(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }

    [XmlRoot("link")]
    public class LinkTag
    {
        [XmlAttribute("rel")]
        public string rel { get; set; }
        [XmlAttribute("href")]
        public string href { get; set; }
        [XmlAttribute("type")]
        public string type { get; set; }
    }
}
