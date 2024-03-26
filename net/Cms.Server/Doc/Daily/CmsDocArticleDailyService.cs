using Com.Scm.Cms.Doc.Daily.Dvo;
using Com.Scm.Cms.Log;
using Com.Scm.Config;
using Com.Scm.Jwt;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Com.Scm.Cms.Doc.Daily
{
    /// <summary>
    /// 每日推荐
    /// </summary>
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsDocArticleDailyService : ApiService
    {
        private readonly SugarRepository<CmsLogUserDailyArticleDao> _thisRepository;
        private readonly SugarRepository<CmsDocArticleDao> _articleRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly JwtContextHolder _jwtContextHolder;

        public CmsDocArticleDailyService(SugarRepository<CmsLogUserDailyArticleDao> thisRepository,
            SugarRepository<CmsDocArticleDao> articleRepository,
            SugarRepository<UserDao> userRepository,
            JwtContextHolder jwtContextHolder,
            EnvConfig config)
        {
            _thisRepository = thisRepository;
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _jwtContextHolder = jwtContextHolder;
            _EnvConfig = config;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<CmsLogUserDailyArticleDvo>> GetPagesAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                .WhereIF(!string.IsNullOrEmpty(request.dates), a => a.dates == request.dates)
                .OrderBy(a => a.id, OrderByType.Desc)
                .Select<CmsLogUserDailyArticleDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        private void Prepare(List<CmsLogUserDailyArticleDvo> list)
        {
            foreach (var item in list)
            {
                item.create_names = GetUserNames(_userRepository, item.create_user);
                item.update_names = GetUserNames(_userRepository, item.update_user);
            }
        }
    }
}
