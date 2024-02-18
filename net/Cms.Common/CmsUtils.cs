namespace Com.Scm.Cms
{
    public class CmsUtils
    {
        public const long APP_ID = 1714913846931623936L;

        public static void SaveFile(string path, long id, string content)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var file = Path.Combine(path, id + ".txt");
            using (var writer = new StreamWriter(file))
            {
                writer.Write(content);
            }
        }

        public static string ReadFile(string path, long id)
        {
            var file = Path.Combine(path, id + ".txt");
            if (!File.Exists(file))
            {
                return "";
            }

            using (var reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
