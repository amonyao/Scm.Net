internal class Program
{
    private static void Main(string[] args)
    {
        var dto1 = new ChildDto();
        dto1.id = 1;
        dto1.name = "name1";
        dto1.time1 = DateTime.Now;
        dto1.time2 = DateTime.Now;

        var dto2 = new ChildDto();
        dto2.id = 2;
        DtoUtils.Adapt(dto1, dto2);
        Console.WriteLine(dto2.ToString());
    }
}

public class BaseDto
{
    public long id { get; set; }

    public DateTime time1 { get; set; }
}

public class ChildDto : BaseDto
{
    public string name { get; set; }

    public DateTime time2 { get; set; }
}

public class DtoUtils
{
    public static void Adapt<T>(object src, T dst)
    {
        if (dst == null)
        {
            return;
        }

        var dstType = dst.GetType();
        var props = dstType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
        if (props == null)
        {
            return;
        }

        var srcType = src.GetType();
        foreach (var prop in props)
        {
            var srcProp = srcType.GetProperty(prop.Name);
            if (srcProp == null)
            {
                continue;
            }

            prop.SetValue(dst, srcProp.GetValue(src));
        }
    }
}