using Com.Scm.Cjg;
using Com.Scm.Cms.Doc;
using Com.Scm.Utils;
using SqlSugar;

internal class Program
{
    public static void Main(string[] args)
    {
        var config = new ConnectionConfig();
        config.ConnectionString = "server=127.0.0.1;database=scm;uid=root;pwd=Scm.1234;charset='utf8';SslMode=None;";
        config.DbType = DbType.MySql;
        config.InitKeyType = InitKeyType.Attribute;

        var sqlClient = new SqlSugar.SqlSugarClient(config);
        while (true)
        {
            var qty = DoUpgrade(sqlClient);
            if (qty < 1)
            {
                break;
            }
        }

        Console.WriteLine("升级完成！");
    }

    public static int DoUpgrade(SqlSugarClient client)
    {
        var list = client.Queryable<CjgDocArticleSectionDao>()
             .Where(a => a.row_status == 1)
             .Take(100)
             .ToList();
        Console.WriteLine("读取数量：" + list.Count);

        var qty = list.Count;
        foreach (var item in list)
        {
            var dao = client.Queryable<CmsDocArticleDao>().First(a => a.id == item.article_id);
            if (dao == null)
            {
                continue;
            }

            var content = item.content;
            dao.summary = TextUtils.Left(content, 1024);
            client.Updateable(dao).ExecuteCommand();

            if (content.Length > 1024)
            {
                SaveFile(dao.id + ".txt", content);
            }
            item.row_status += 10;
            client.Updateable(item).ExecuteCommand();
        }
        return qty;
    }

    public static void SaveFile(string filename, string content)
    {
        using (var writer = new StreamWriter(filename))
        {
            writer.Write(content);
        }
    }
}