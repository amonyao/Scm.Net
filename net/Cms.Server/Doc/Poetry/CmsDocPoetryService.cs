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

namespace Com.Scm.Cms.Doc.Poetry
{
    [ApiExplorerSettings(GroupName = "cms")]
    public class CmsDocPoetryService : ApiService
    {
        private readonly SugarRepository<CmsDocArticleDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsDocPoetryService(SugarRepository<CmsDocArticleDao> thisRepository,
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
                .Where(a => a.types == Enums.ArticleTypesEnum.Poetry)
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
                .Select(a => new CmsDocArticleDto { id = a.id, key = a.key, title = a.title, create_time = a.create_time, update_time = a.update_time })
                .ToListAsync();

            //Prepare(result);
            return result;
        }

        private void Prepare(List<CmsDocArticleDto> list)
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
        public async Task<CmsDocArticleDvo> GetAsync(long id)
        {
            var dvo = await _thisRepository
                .AsQueryable()
                .Where(a => a.id == id && a.types == Enums.ArticleTypesEnum.Poetry)
                .Select<CmsDocArticleDvo>()
                .FirstAsync();

            if (dvo == null)
            {
                dvo = new CmsDocArticleDvo();
            }

            dvo.content = dvo.summary;

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
            var dto = await _thisRepository
                .AsQueryable()
                .Select<CmsDocArticleDto>()
                .FirstAsync(m => m.id == id && m.types == Enums.ArticleTypesEnum.Poetry);

            dto.content = dto.summary;

            return dto;
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsDocArticleDto> GetViewAsync(long id)
        {
            var dto = await _thisRepository
                .AsQueryable()
                .Select<CmsDocArticleDto>()
                .FirstAsync(m => m.id == id && m.types == Enums.ArticleTypesEnum.Poetry);

            dto.content = dto.summary;

            return dto;
        }

        /// <summary>
        /// 新增诗词
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(CmsDocPoetryDto model)
        {
            var dao = model.Adapt<CmsDocArticleDao>();
            dao.types = Enums.ArticleTypesEnum.Poetry;

            var content = model.content;
            dao.files = content.Length > CmsDocArticleDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(content, CmsDocArticleDto.SUMMARY_SIZE);

            var result = await _thisRepository.InsertAsync(dao);
            if (!result)
            {
                return 0;
            }

            if (dao.files > 0)
            {
                CmsUtils.SaveFile(_EnvConfig.GetDataPath("articles"), dao.id, content);
            }

            return 1;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsDocPoetryDto model)
        {
            var dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                return;
            }

            dao = CommonUtils.Adapt(model, dao);

            var content = model.content;
            dao.files = content.Length > CmsDocArticleDto.SUMMARY_SIZE ? 1 : 0;
            dao.summary = TextUtils.Left(content, CmsDocArticleDto.SUMMARY_SIZE);

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
