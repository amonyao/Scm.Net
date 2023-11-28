using Com.Scm.Cms.Doc.Daily.Dvo;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Jwt;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Share;
using Com.Scm.Share.Dvo;
using Com.Scm.Ur;

namespace Com.Scm.Cms.Doc.Share
{
    /// <summary>
    /// 分享
    /// </summary>
    public class CmsDocArticleShareService : ApiService
    {
        private readonly SugarRepository<CmsDocArticleDao> _articleRepository;
        private readonly IShareService _shareService;
        private readonly JwtContextHolder _contextHolder;

        public CmsDocArticleShareService(SugarRepository<CmsDocArticleDao> articleRepository, IShareService shareService, JwtContextHolder contextHolder)
        {
            _articleRepository = articleRepository;
            _shareService = shareService;
            _contextHolder = contextHolder;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<ShareHeaderDvo>> GetPagesAsync(SearchRequest request)
        {
            var token = _contextHolder.GetToken();
            var unitId = UnitDto.SYS_ID;
            var userId = UserDto.SYS_ID;
            if (token != null)
            {
                unitId = token.unit_id;
                userId = token.user_id;
            }

            return await _shareService.GetPagesShareData(request, CmsUtils.APP_ID, unitId, userId);
        }
    }
}
