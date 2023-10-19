using Com.Scm.Cms.Doc.Article.Dvo;
using Com.Scm.Cms.Doc.Notes.Dvo;
using Com.Scm.Cms.Res;
using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Filter;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Cms.Doc.Notes
{
    public class CmsDocNotesService : ApiService
    {
        private readonly SugarRepository<CmsDocArticleDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsDocNotesService(SugarRepository<CmsDocArticleDao> thisRepository,
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
        public async Task<PageResult<CmsDocNotesDvo>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.types == Enums.ArticleTypesEnum.Notes)
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
        public async Task<List<CmsDocNotesDvo>> GetListAsync(ListRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Com.Scm.Enums.ScmStatusEnum.Enabled)
                .WhereIF(IsValidId(request.cat_id), a => a.cat_id == request.cat_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.title.Contains(request.key))
                .OrderBy(m => m.id, SqlSugar.OrderByType.Desc)
                .Select<CmsDocNotesDvo>()
                .ToListAsync();

            //Prepare(result);
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
        public async Task<CmsDocNotesDvo> GetAsync(long id)
        {
            var dvo = new CmsDocNotesDvo();

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
        public async Task<CmsDocArticleDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsDocArticleDto>()
                .FirstAsync(m => m.id == id && m.types == Enums.ArticleTypesEnum.Notes);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsDocArticleDto> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsDocArticleDto>()
                .FirstAsync(m => m.id == id && m.types == Enums.ArticleTypesEnum.Notes);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CmsDocNotesDto model)
        {
            var dao = model.Adapt<CmsDocArticleDao>();
            if (IsValidId(dao.cat_id))
            {
                dao.cat_id = CmsResCatDto.SYS_ID;
            }

            dao.types = Enums.ArticleTypesEnum.Notes;
            dao.nation_id = CmsResNationDto.SYS_ID;
            dao.dynasty_id = CmsResDynastyDto.SYS_ID;
            dao.author_id = CmsResAuthorDto.SYS_ID;
            dao.origin_id = CmsResOriginDto.SYS_ID;
            dao.style_id = CmsResStyleDto.SYS_ID;

            var content = model.content;
            dao.files = content.Length > CmsDocArticleDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(model.content, CmsDocArticleDto.SUMMARY_SIZE);

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

            CmsDocArticleDao dao = null;
            var content = model.content ?? "";
            var tooLong = content.Length > 1024;

            if (IsNormalId(model.id))
            {
                dao = await _thisRepository.GetByIdAsync(model.id);
            }

            if (dao == null)
            {
                dao = new CmsDocArticleDao();
                dao.id = model.id;
                dao.types = Enums.ArticleTypesEnum.Notes;
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

            dao.types = Enums.ArticleTypesEnum.Notes;
            dao.nation_id = CmsResNationDto.SYS_ID;
            dao.dynasty_id = CmsResDynastyDto.SYS_ID;
            dao.author_id = CmsResAuthorDto.SYS_ID;
            dao.origin_id = CmsResOriginDto.SYS_ID;
            dao.style_id = CmsResStyleDto.SYS_ID;

            var content = model.content;
            dao.files = content.Length > CmsDocArticleDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(model.content, CmsDocArticleDto.SUMMARY_SIZE);

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

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, NoJsonResult]
        public async Task<UploadResponse> UploadAsync([FromForm] UploadRequest request)
        {
            var response = new UploadResponse();

            //判断是否上传了文件内容
            if (request.file == null)
            {
                response.SetFailure("上传内容为空！");
                return response;
            }

            #region 保存文件
            var fileName = request.file.FileName;
            var ext = Path.GetExtension(fileName);
            fileName = System.DateTime.UtcNow.Ticks.ToString() + ext;

            var path = _EnvConfig.GetUploadPath(fileName);
            using (var stream = File.OpenWrite(path))
            {
                //将文件内容复制到流中
                await request.file.CopyToAsync(stream);
            }
            response.file = fileName;
            response.location = _EnvConfig.ToUri(path);
            response.SetSuccess("文件上传成功！");
            #endregion

            return response;
        }
    }
}
