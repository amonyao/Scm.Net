using Com.Scm.Cms.Doc.Note.Dvo;
using Com.Scm.Config;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Cms.Doc.Note
{
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsBlogService : ApiService
    {
        private readonly SugarRepository<CmsDocNoteDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsBlogService(SugarRepository<CmsDocNoteDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            EnvConfig config)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _EnvConfig = config;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<NoteBasicDvo>> GetPagesAsync(BlogSearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.types == Enums.NoteTypesEnum.HtmlPad)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<NoteBasicDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<NoteBasicDvo>> GetListAsync(BlogSearchRequest request)
        {
            var query = _thisRepository.AsQueryable()
                .Where(a => a.types == Enums.NoteTypesEnum.HtmlPad)
                .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled)
                .WhereIF(IsValidId(request.cat_id), a => a.cat_id == request.cat_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.title.Contains(request.key));

            if (request.types == SortTypeEnums.ByCreateTime)
            {
                query = query.OrderBy(a => a.create_time, SqlSugar.OrderByType.Desc);
            }
            else if (request.types == SortTypeEnums.ByQty)
            {
                query = query.OrderBy(a => a.qty, SqlSugar.OrderByType.Desc);
            }

            var result = await query.Select<NoteBasicDvo>().ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<NoteBasicDvo> list)
        {
            foreach (var item in list)
            {
                item.create_names = GetUserNames(_userRepository, item.create_user);
                item.update_names = GetUserNames(_userRepository, item.update_user);
            }
        }
    }
}