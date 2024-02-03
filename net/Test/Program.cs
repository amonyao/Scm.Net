using Com.Scm.Utils;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Test;

internal class Program
{
    public static void Main(string[] args)
    {
        //new IconTool().Gen("D:\\js.js", "D:\\css.css");
        //new IconFont().Save(@"D:\workspace");
        //new IconFont().Compare(@"D:\workspace\icon.json", "D:\\workspace\\svg");
        //new IconFont().Compare(@"D:\workspace\fill.json", "D:\\workspace\\svg\\fill");
        //new IconFont().Compare(@"D:\workspace\line.json", "D:\\workspace\\svg\\line");
        new IconFont().GetNextUnicode(@"D:\workspace\line.json");
        Console.WriteLine("升级完成！");
    }

    public async void ReadIcoAsync(string address)
    {
        string url = GetHost(address);

        //ReadIco(url, "/favicon.ico");

        var html = await ReadHtml(url);
        if (string.IsNullOrEmpty(html))
        {
            return;
        }

        var regex = new Regex("<link\\b[^>]*/>");
        var match = regex.Match(html);
        while (match.Success)
        {
            var tag = TextUtils.AsXmlObject<LinkTag>(match.Result(""));
            if (tag == null)
            {
                continue;
            }
            var rel = tag.rel ?? "";
            if (rel.ToLower().IndexOf("icon") < 0)
            {
                continue;
            }

            var path = tag.href;
            ReadIcon(url, path);
            break;
        }
    }

    private string GetHost(string url)
    {
        var idx = url.IndexOf("://");
        if (idx < 0)
        {
            url = "http://" + url;
            idx = "http://".Length;
        }

        idx = url.IndexOf('/', idx + 1);
        if (idx > 0)
        {
            return url.Substring(0, idx);
        }

        return url;
    }

    private async void ReadIcon(string host, string path)
    {
        var url = host + path;

        var client = new HttpClient();
        try
        {
            using (var stream = await client.GetStreamAsync(url))
            {
                byte[] buff = new byte[1024];
                int length = 0;
                using (MemoryStream memory = new MemoryStream())
                {
                    length = stream.Read(buff, 0, buff.Length);
                    while (length > 0)
                    {
                        memory.Write(buff, 0, length);
                    }

                    var arr = memory.ToArray();
                    //info.functionIcon = Convert.ToBase64String(arr).Trim();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task<string> ReadHtml(string url)
    {
        try
        {
            return await new HttpClient().GetStringAsync(url);
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