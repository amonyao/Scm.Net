using Com.Scm.Cms.Doc.Daily.Dvo;
using Com.Scm.Cms.Log;
using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Jwt;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Com.Scm.Cms.Doc.Daily
{
    [ApiExplorerSettings(GroupName = "v1")]
    public class CmsDocDailyService : ApiService
    {
        private readonly SugarRepository<CmsLogUserDailyArticleDao> _thisRepository;
        private readonly SugarRepository<CmsDocArticleDao> _articleRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly JwtContextHolder _jwtContextHolder;

        public CmsDocDailyService(SugarRepository<CmsLogUserDailyArticleDao> thisRepository,
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
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<DailyArticleDvo> GetAsync(SearchRequest request)
        {
            var dates = request.dates;
            if (string.IsNullOrWhiteSpace(dates))
            {
                dates = TimeUtils.FormatDate(DateTime.Now);
            }

            var userId = UserDto.SYS_ID;
            var token = _jwtContextHolder.GetToken();
            if (token != null)
            {
                userId = token.user_id;
            }

            var dailyDao = await _thisRepository.GetFirstAsync(a => a.dates == dates && a.user_id == userId && a.row_status == Scm.Enums.ScmStatusEnum.Enabled);
            CmsDocArticleDao articleDao;

            if (dailyDao != null)
            {
                articleDao = await _articleRepository.GetByIdAsync(dailyDao.article_id);
            }
            else
            {
                var pair = await _articleRepository.AsQueryable()
                      .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled)
                      .Select(a => new DbIdPair { min_id = SqlFunc.AggregateMin(a.id), max_id = SqlFunc.AggregateMax(a.id) })
                      .FirstAsync();
                var id = CmsDocArticleDto.SYS_ID;
                if (pair != null)
                {
                    id = new Random().NextInt64(pair.min_id, pair.max_id);
                }

                articleDao = await _articleRepository.AsQueryable()
                   .Where(a => a.id >= id && a.row_status == Scm.Enums.ScmStatusEnum.Enabled)
                   .FirstAsync();

                dailyDao = new CmsLogUserDailyArticleDao();
                dailyDao.dates = dates;
                dailyDao.user_id = userId;
                dailyDao.article_id = articleDao.id;
                await _thisRepository.InsertAsync(dailyDao);
            }

            var articleDvo = articleDao.Adapt<CmsDocArticleDvo>();
            articleDvo.content = articleDvo.summary;
            if (articleDao.files > 0)
            {
                articleDvo.content = CmsUtils.ReadFile(_EnvConfig.GetDataPath("articles"), articleDao.id);
            }

            var dailyDvo = new DailyArticleDvo();
            dailyDvo.dates = dates;
            dailyDvo.article = articleDvo;

            return dailyDvo;
        }
    }

    public class DbIdPair
    {
        public long min_id { get; set; }
        public long max_id { get; set; }
    }
}
