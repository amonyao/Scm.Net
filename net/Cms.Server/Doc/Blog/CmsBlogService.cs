using Com.Scm.Cms.Doc.Blog.Dvo;
using Com.Scm.Cms.Doc.Notes.Dvo;
using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Cms.Doc.Blog
{
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsBlogService : ApiService
    {
        private readonly SugarRepository<CmsBlogDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsBlogService(SugarRepository<CmsBlogDao> thisRepository,
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
        public async Task<PageResult<CmsDocNotesDvo>> GetPagesAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsDocNotesDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CmsDocNotesDvo>> GetListAsync(SearchRequest request)
        {
            var query = _thisRepository.AsQueryable()
                .Where(a => a.row_status == Com.Scm.Enums.ScmStatusEnum.Enabled)
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

            var result = await query.Select<CmsDocNotesDvo>().ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<CmsDocNotesDvo> list)
        {
            foreach (var item in list)
            {
                item.create_names = GetUserNames(_userRepository, item.create_user);
                item.update_names = GetUserNames(_userRepository, item.update_user);
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsBlogDvo> GetAsync(long id)
        {
            var dvo = new CmsBlogDvo();

            var dao = await _thisRepository
                .AsQueryable()
                .Where(a => a.id == id)
                .FirstAsync();

            if (dao != null)
            {
                dvo.id = dao.id;
                dvo.title = dao.title;
                dvo.content = dao.summary;

                if (dao.files > 0)
                {
                    var file = GetFile(dao.id);
                    if (File.Exists(file))
                    {
                        using (var reader = File.OpenText(file))
                        {
                            dvo.content = reader.ReadToEnd();
                        }
                    }
                }
            }

            return dvo;
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsBlogDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsBlogDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsBlogDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsBlogDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CmsDocNotesDto model)
        {
            var dao = model.Adapt<CmsBlogDao>();
            if (IsValidId(dao.cat_id))
            {
                dao.cat_id = CmsResCatDto.SYS_ID;
            }

            dao.types = Enums.BlogTypesEnum.Draft;

            var content = model.content;
            dao.files = content.Length > CmsBlogDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(model.content, CmsBlogDto.SUMMARY_SIZE);

            var qty = await _thisRepository.InsertAsync(dao);
            if (dao.files > 0)
            {
                CmsUtils.SaveFile(_EnvConfig.GetDataPath("articles"), dao.id, content);
            }
            return qty;
        }

        private string GetFile(long id)
        {
            var path = _EnvConfig.GetDataPath("articles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, id + ".txt");
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SaveResponse> SaveAsync(CmsDocNotesDto model)
        {
            var response = new SaveResponse();

            CmsBlogDao dao = null;
            var content = model.content ?? "";
            var tooLong = content.Length > 1024;

            if (IsNormalId(model.id))
            {
                dao = await _thisRepository.GetByIdAsync(model.id);
            }

            if (dao == null)
            {
                dao = new CmsBlogDao();
                dao.id = model.id;
                dao.types = Enums.BlogTypesEnum.Draft;
                dao.title = model.title;
                dao.summary = TextUtils.Left(content, 1024);
                dao.files = tooLong ? 1 : 0;
                await _thisRepository.InsertAsync(dao);
            }
            else
            {
                dao.title = model.title;
                dao.summary = TextUtils.Left(model.content, 1024);
                dao.files = tooLong ? 1 : 0;
                await _thisRepository.UpdateAsync(dao);
            }

            if (tooLong)
            {
                var file = GetFile(dao.id);
                using (var writer = new StreamWriter(file))
                {
                    await writer.WriteAsync(model.content);
                }
            }

            response.data = dao.id;
            return response;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsDocNotesDto model)
        {
            var dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                return;
            }

            dao = model.Adapt(dao);
            if (!IsValidId(dao.cat_id))
            {
                dao.cat_id = CmsResCatDto.SYS_ID;
            }

            dao.types = Enums.BlogTypesEnum.Draft;

            var content = model.content;
            dao.files = content.Length > CmsBlogDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(model.content, CmsBlogDto.SUMMARY_SIZE);

            await _thisRepository.UpdateAsync(dao);

            if (dao.files > 0)
            {
                CmsUtils.SaveFile(_EnvConfig.GetDataPath("articles"), dao.id, content);
            }
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="param">逗号分隔</param>
        /// <returns></returns>
        public async Task<int> StatusAsync(ScmChangeStatusRequest param)
        {
            return await UpdateStatus(_thisRepository, param.ids, param.status);
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="ids">逗号分隔</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<int> DeleteAsync(string ids)
        {
            return await DeleteRecord(_thisRepository, ids.ToListLong());
        }
    }
}