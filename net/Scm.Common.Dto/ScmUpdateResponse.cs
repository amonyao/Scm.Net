using Com.Scm.Api;

namespace Com.Scm
{
    public class ScmUpdateResponse<T> : ScmApiResponse
    {
        public T Data { get; set; }

        public void SetSuccess(T data)
        {
            _rr = true;
            _rm = "";
            Data = data;
        }
    }
}
