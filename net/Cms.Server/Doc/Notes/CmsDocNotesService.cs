using Com.Scm.Cms.Doc.Article.Dvo;
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
        public async Task<PageResult<CmsDocArticleDto>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.types == Enums.ArticleTypesEnum.Notes)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsDocArticleDto>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CmsDocArticleDto>> GetListAsync(ListRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Com.Scm.Enums.ScmStatusEnum.Enabled)
                .WhereIF(IsValidId(request.cat_id), a => a.cat_id == request.cat_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.title.Contains(request.key))
                .OrderBy(m => m.id, SqlSugar.OrderByType.Desc)
                .Select(a => new CmsDocArticleDto { id = a.id, key = a.key, salt = a.salt, title = a.title, create_time = a.create_time, update_time = a.update_time })
                .ToListAsync();

            //Prepare(result);
            return result;
        }

        private void Prepare(List<CmsDocArticleDto> list)
        {
            foreach (var item in list)
            {
                item.update_names = GetUserNames(_userRepository, item.update_user);
                item.update_names = GetUserNames(_userRepository, item.update_user);
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsDocArticleDvo> GetAsync(long id)
        {
            var dvo = await _thisRepository
                .AsQueryable()
                .Where(a => a.id == id)
                .Select<CmsDocArticleDvo>()
                .FirstAsync();

            if (dvo == null)
            {
                dvo = new CmsDocArticleDvo();
            }

            dvo.content = dvo.summary;
            if (dvo.files > 0)
            {
                var file = GetFile(dvo.id);
                if (File.Exists(file))
                {
                    using (var reader = File.OpenText(file))
                    {
                        dvo.content = reader.ReadToEnd();
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
        public async Task<bool> AddAsync(CmsDocArticleDto model)
        {
            return await _thisRepository.InsertAsync(model.Adapt<CmsDocArticleDao>());
        }

        private string GetFile(long id)
        {
            var path = _EnvConfig.GetRootPath("articles");
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
        public async Task SaveAsync(CmsDocNotesDto model)
        {
            var content = model.content ?? "";

            var dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                dao = new CmsDocArticleDao();
                dao.id = model.id;
                dao.types = Enums.ArticleTypesEnum.Notes;
                dao.title = model.title;
                dao.summary = TextUtils.Left(content, 1024);
                dao.files = content.Length > 1024 ? 1 : 0;
                await _thisRepository.InsertAsync(dao);
            }
            else
            {
                dao.title = model.title;
                dao.summary = TextUtils.Left(model.content, 1024);
                dao.files = content.Length > 1024 ? 1 : 0;
                await _thisRepository.UpdateAsync(dao);
            }

            if (content.Length > 1024)
            {
                var file = GetFile(dao.id);
                using (var writer = new StreamWriter(file))
                {
                    await writer.WriteAsync(model.content);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsDocArticleDto model)
        {
            var dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                return;
            }

            dao = model.Adapt(dao);
            await _thisRepository.UpdateAsync(dao);
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
