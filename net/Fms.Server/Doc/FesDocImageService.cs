using Com.Scm.Config;
using Com.Scm.Enums;
using Com.Scm.Exceptions;
using Com.Scm.Filters;
using Com.Scm.Fms.Doc.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 图片数据服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Fes")]
    public class FesDocImageService : ApiService
    {
        private readonly SugarRepository<FileDao> _fileRepository;
        private readonly SugarRepository<ImageDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly EnvConfig _envConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FesDocImageService(
            SugarRepository<FileDao> fileRepository,
            SugarRepository<ImageDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            EnvConfig envConfig)
        {
            _fileRepository = fileRepository;
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _envConfig = envConfig;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<ImageDvo>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _fileRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(a => a.id)
                .Select<ImageDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ImageDvo>> GetListAsync(ScmSearchRequest request)
        {
            var result = await _fileRepository.AsQueryable()
                .Where(a => a.row_status == ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(a => a.id)
                .Select<ImageDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void Prepare(List<ImageDvo> list)
        {
            foreach (var item in list)
            {
                var updateDao = _userRepository.GetById(item.update_user);
                item.update_names = updateDao?.names;

                var createDao = _userRepository.GetById(item.create_user);
                item.create_names = createDao?.names;

                var dao = _thisRepository.GetById(item.id);
                if (dao != null)
                {
                    item.width = dao.width;
                    item.height = dao.height;
                    item.exif = dao.exif;
                }
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), AllowAnonymous, NoJsonResult]
        public async Task<IActionResult> GetAsync(long id)
        {
            var dao = await _fileRepository
                .AsQueryable()
                .ClearFilter()
                .Where(a => a.id == id)
                .FirstAsync();

            if (dao != null)
            {
                var file = _envConfig.GetUploadPath(dao.path);
                if (File.Exists(file))
                {
                    using (var stream = File.OpenRead(file))
                    {
                        var bytes = new byte[stream.Length];
                        await stream.ReadAsync(bytes, 0, bytes.Length);
                        return new FileContentResult(bytes, "image/png");
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ImageDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<ImageDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ImageDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<ImageDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(ImageDto model)
        {
            return await _thisRepository.InsertAsync(model.Adapt<ImageDao>());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ImageDto model)
        {
            var dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                throw new BusinessException($"无效的数据信息，更新失败！");
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
    }
}