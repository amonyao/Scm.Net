using Com.Scm.Utils;
using Newtonsoft.Json;

namespace Test
{
    public class IconTool
    {
        public void Gen(string src, string dst)
        {
            // 读取现有JS文件
            var text = File.ReadAllText(src);
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var oldIcons = TextUtils.AsJsonObject<IconList>(text);
            if (oldIcons == null)
            {
                oldIcons = new IconList();
            }

            var newIcons = new IconList();
            foreach (var oldCat in oldIcons.icons)
            {
                newIcons.Add(new IconCat { name = oldCat.name, namee = oldCat.namee });
            }
            var otherCat = newIcons.Get("其它");
            if (otherCat == null)
            {
                otherCat = new IconCat();
                newIcons.Add(otherCat);
            }

            // 读取新的css文件
            var lines = File.ReadAllLines(dst);
            var newDict = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (!line.StartsWith(".sc-"))
                {
                    continue;
                }

                var idx = line.IndexOf(':');
                if (idx < 0)
                {
                    continue;
                }

                var key = line.Substring(4, idx - 4);
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                var val = 0;
                if (key.EndsWith("-fill"))
                {
                    val |= 1;
                }
                if (key.EndsWith("-line"))
                {
                    val |= 2;
                }
                if (val > 0)
                {
                    key = key.Substring(0, key.Length - 5);
                }

                if (!newDict.ContainsKey(key))
                {
                    newDict[key] = val;
                    continue;
                }

                newDict[key] |= val;
            }

            foreach (var key in newDict.Keys)
            {
                var icon = oldIcons.Find(key);
                var val = newDict[key];
                var type = GenType(val);
                if (icon == null)
                {
                    otherCat.Add(new Icon { name = key, desc = key, val = newDict[key], type = type });
                    continue;
                }

                icon.val = newDict[key];
                icon.type = type;
                newIcons.Add(icon);
            }

            File.WriteAllText("D:\\old.json", TextUtils.ToJsonString(oldIcons));
            File.WriteAllText("D:\\new.json", TextUtils.ToJsonString(newIcons));
        }

        private static string GenType(int val)
        {
            if (val == 3)
            {
                return "both";
            }
            if (val == 2)
            {
                return "fill";
            }
            if (val == 1)
            {
                return "line";
            }
            return "";
        }
    }

    public class IconList
    {
        public List<IconCat> icons;

        public IconCat Get(string name)
        {
            if (icons != null)
            {
                foreach (var icon in icons)
                {
                    if (icon.name == name) return icon;
                }
            }
            return null;
        }

        public IconCat GetLast()
        {
            if (icons == null)
            {
                icons = new List<IconCat>();
            }
            return icons[icons.Count - 1];
        }

        public void Add(IconCat cat)
        {
            if (icons == null)
            {
                icons = new List<IconCat>();
            }
            icons.Add(cat);
        }

        public Icon Find(string name)
        {
            foreach (IconCat cat in icons)
            {
                for (var i = 0; i < cat.icons.Count; i++)
                {
                    var icon = cat.icons[i];
                    if (icon.name == name)
                    {
                        cat.icons.RemoveAt(i);
                        icon.cat_name = cat.name;
                        return icon;
                    }
                }
            }
            return null;
        }

        public void Add(Icon icon)
        {
            foreach (var cat in icons)
            {
                if (cat.name != icon.cat_name)
                {
                    continue;
                }

                cat.Add(icon);
                return;
            }
        }
    }

    public class IconCat
    {
        public string name { get; set; }
        public string namee { get; set; }
        public List<Icon> icons { get; set; }
        public int size { get; set; }

        public void Add(Icon icon)
        {
            if (icons == null)
            {
                icons = new List<Icon>();
            }
            icons.Add(icon);
            size = icons.Count;
        }
    }

    public class Icon
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string type { get; set; }

        [JsonIgnore]
        public int val { get; set; }

        [JsonIgnore]
        public string cat_name { get; set; }
    }
}
