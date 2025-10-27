using System.Collections.Generic;

namespace Com.Scm.Api
{
    public class ScmApiListResponse<T> : ScmApiResponse
    {
        /// <summary>
        /// 返回列表
        /// </summary>
        public List<T> Data { get; set; }

        public void SetSuccess(List<T> list)
        {
            _rr = true;
            _rc = 0;
            Data = list;
        }
    }
}
