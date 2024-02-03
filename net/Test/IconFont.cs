using Newtonsoft.Json;

namespace Test
{
    public class IconFont
    {
        public void GetUnicode(string file)
        {
            var obj = ReadFile(file);
            if (obj == null)
            {
                return;
            }

            var min = 60000;
            var max = 60000;
            foreach (var icon in obj.data.icons)
            {
                if (icon.unicode < min)
                {
                    min = icon.unicode;
                    continue;
                }
                if (icon.unicode > max)
                {
                    max = icon.unicode;
                    continue;
                }
            }
            Console.WriteLine($"Min:{min},Max:{max}");
        }

        private int _Min = 0xe600;
        public void GetNextUnicode(string file)
        {
            var obj = ReadFile(file);
            if (obj == null)
            {
                return;
            }

            var min = 0xe600;
            while (true)
            {
                if (!Exists(obj.data.icons, min))
                {
                    break;
                }
                min += 1;
            }
            Console.WriteLine("UniCode:" + min.ToString("x4"));
        }

        private bool Exists(List<Obj3> icons, int number)
        {
            foreach (var icon in icons)
            {
                if (icon.unicode == number)
                {
                    return true;
                }
            }
            return false;
        }

        private Obj1 ReadFile(string file)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("JSON文件不存在！");
                return null;
            }

            string text = "";
            using (var stream = File.OpenRead(file))
            {
                var reader = new StreamReader(stream);
                text = reader.ReadToEnd();
            }


            var obj = JsonConvert.DeserializeObject<Obj1>(text);
            if (obj == null)
            {
                Console.WriteLine("JSON解析失败！");
                return null;
            }

            var data = obj.data;
            if (data == null)
            {
                Console.WriteLine("DATA内容为空！");
                return null;
            }

            var icons = data.icons;
            if (icons == null)
            {
                Console.WriteLine("ICON内容为空！");
                return null;
            }

            return obj;
        }

        public void Compare(string file, string folder)
        {
            var obj = ReadFile(file);
            if (obj == null)
            {
                return;
            }

            var path = Path.Combine(folder, "done");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var icon in obj.data.icons)
            {
                var name = icon.font_class + ".svg";
                var svg = Path.Combine(folder, name);
                if (!File.Exists(svg))
                {
                    Console.WriteLine($"{name}不存在！");
                    continue;
                }

                File.Move(svg, Path.Combine(path, name));
            }
        }

        public void SaveSvg(string root)
        {
            var file = Path.Combine(root, "icon.json");
            var obj = ReadFile(file);
            if (obj == null)
            {
                return;
            }

            var path = Path.Combine(root, "svg");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var qty = 0;
            var col = 0;
            var html = "<html>\r\n<head>\r\n<style>\r\nimg{width:48px;height:48px;}\r\n</style>\r\n</head>\r\n<body>\r\n\t<table>\r\n\t\t<tr>";
            foreach (var icon in obj.data.icons)
            {
                var svg = icon.show_svg;
                if (svg == null)
                {
                    continue;
                }

                svg = svg.Replace("style=\"width: 1em;height: 1em;vertical-align: middle;fill: currentColor;overflow: hidden;\" ", "");
                var name = icon.font_class;

                var dir = "";
                if (name.EndsWith("-fill"))
                {
                    dir = "fill";
                    file = Path.Combine(path, "fill");
                    if (!Directory.Exists(file))
                    {
                        Directory.CreateDirectory(file);
                    }
                    name = name.Substring(0, name.Length - 5);
                }
                else if (name.EndsWith("-line"))
                {
                    dir = "line";
                    name = name.Substring(0, name.Length - 5);
                }

                file = path;
                if (!string.IsNullOrEmpty(dir))
                {
                    file = Path.Combine(file, dir);
                    if (!Directory.Exists(file))
                    {
                        Directory.CreateDirectory(file);
                    }
                }

                name += ".svg";

                file = Path.Combine(file, name);
                File.WriteAllText(file, svg);
                qty += 1;

                html += "<td>";
                html += "<img src=\"./svg/" + dir + "/" + name + "\" alt=\"\" title=\"./svg/" + dir + "/" + name + "\"/>";
                html += "</td>";
                col += 1;
                if (col >= 8)
                {
                    html += "</tr>";
                    html += "<tr>";
                    col = 0;
                }
            }

            html += "\t\t</tr>\r\n\t</table>\r\n</body>\r\n</html>";
            path = Path.Combine(root, "index.html");
            File.WriteAllText(path, html);
            Console.WriteLine("文件转换成功：" + qty);
        }
    }
    public class Obj1
    {
        public int code { get; set; }
        public Obj2 data { get; set; }
    }

    public class Obj2
    {
        public List<Obj3> icons { get; set; }
    }

    public class Obj3
    {
        public int id { get; set; }
        public string name { get; set; }
        public int project_id { get; set; }
        public string show_svg { get; set; }
        public int unicode { get; set; }
        public string font_class { get; set; }
    }
}
