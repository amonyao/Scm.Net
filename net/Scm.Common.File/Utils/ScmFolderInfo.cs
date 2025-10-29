using System.Collections.Generic;

namespace Com.Scm.Utils
{
    /// <summary>
    /// 文件目录
    /// </summary>
    public class ScmFolderInfo
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public List<ScmFolderInfo> Children { get; set; }
    }
}
