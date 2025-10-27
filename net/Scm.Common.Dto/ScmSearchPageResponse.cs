using Com.Scm.Api;
using System.Collections.Generic;

namespace Com.Scm
{
    public class ScmSearchPageResponse<T> : ScmApiResponse
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public List<T> Items { get; set; }
    }
}
