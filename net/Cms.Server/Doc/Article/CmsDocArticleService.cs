using Com.Scm.Cms.Doc.Article.Dvo;
using Com.Scm.Cms.Res;
using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    public class CmsDocArticleService : ApiService
    {
        private readonly SugarRepository<CmsDocArticleDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly EnvConfig _Config;

        public CmsDocArticleService(SugarRepository<CmsDocArticleDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            EnvConfig config)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _Config = config;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<CmsDocArticleDto>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
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
        public async Task<List<CmsDocArticleDto>> GetListAsync(ScmSearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Com.Scm.Enums.ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsDocArticleDto>()
                .ToListAsync();

            Prepare(result);
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
        public async Task<CmsDocArticleDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<CmsDocArticleDto>();
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
                .FirstAsync(m => m.id == id);
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
                .FirstAsync(m => m.id == id);
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

        /// <summary>
        /// 新增诗词
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddPoetryAsync(CmsDocPoetryDto model)
        {
            var dao = model.Adapt<CmsDocArticleDao>();
            dao.types = Enums.ArticleTypesEnum.Poetry;
            dao.summary = model.content;

            return await _thisRepository.InsertAsync(dao) ? 1 : 0;
        }

        /// <summary>
        /// 新增网文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddLiteraAsync(CmsDocLiteraDto model)
        {
            if (model == null)
            {
                return 0;
            }

            var qty = 0;
            if (model.batch)
            {
                if (model.blank < 1)
                {
                    model.blank = 1;
                }

                var content = model.content.Replace("\r", "");
                var array = new Regex(@"\n{" + model.blank + ",}").Split(content);
                foreach (var item in array)
                {
                    qty += await CreateLiteraDaoAsync(model, item);
                }
            }
            else
            {
                qty += await CreateLiteraDaoAsync(model, model.content);
            }
            return qty;
        }

        private async Task<int> CreateLiteraDaoAsync(CmsDocLiteraDto dto, string content)
        {
            var dao = new CmsDocArticleDao();
            dao.types = Enums.ArticleTypesEnum.Litera;
            dao.cat_id = dto.cat_id;
            dao.nation_id = CmsResNationDto.SYS_ID;
            dao.dynasty_id = CmsResDynastyDto.SYS_ID;
            dao.author_id = CmsResAuthorDto.SYS_ID;
            dao.origin_id = CmsResOriginDto.SYS_ID;
            dao.visible = dto.visible;
            dao.title = TextUtils.Left(content, 128);
            dao.summary = content;

            if (await _thisRepository.InsertAsync(dao))
            {
                return 1;
            }

            return 0;
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
        [HttpPost, AllowAnonymous]
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

            var path = _Config.GetUploadPath(fileName);
            using (var stream = File.OpenWrite(path))
            {
                //将文件内容复制到流中
                await request.file.CopyToAsync(stream);
            }
            response.file = fileName;
            response.SetSuccess("文件上传成功！");
            #endregion

            return response;
        }
    }
}