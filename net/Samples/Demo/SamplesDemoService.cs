using Com.Scm.Config;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Samples.Demo.Dao;
using Com.Scm.Samples.Demo.Dto;
using Com.Scm.Samples.Demo.Dvo;
using Com.Scm.Service;
using Com.Scm.Utils;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Samples.Demo
{
    /// <summary>
    /// 示例代码服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    public class SamplesDemoService : ApiService
    {
        private readonly SugarRepository<DemoDao> _thisRepository;
        private readonly EnvConfig _Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="thisRepository"></param>
        public SamplesDemoService(SugarRepository<DemoDao> thisRepository, EnvConfig config)
        {
            _thisRepository = thisRepository;
            _Config = config;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<DemoDto>> GetPagesAsync(SearchRequest request)
        {
            return await _thisRepository.AsQueryable()
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderByDescending(m => m.id)
                .Select<DemoDto>()
                .ToPageAsync(request.page, request.limit);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DemoDto>> GetListAsync(SearchRequest request)
        {
            return await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Enums.ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderByDescending(m => m.id)
                .Select<DemoDto>()
                .ToListAsync();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DemoDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<DemoDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DemoDto> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<DemoDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(DemoDto model)
        {
            await _thisRepository.InsertAsync(model.Clone<DemoDao>());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(DemoDto model)
        {
            await _thisRepository.UpdateAsync(model.Clone<DemoDao>());
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
        [HttpPost]
        public async Task<string> UploadAsync([FromForm] UploadRequest request)
        {
            //判断是否上传了文件内容
            if (request.file == null)
            {
                return "";
            }

            var fileName = request.file.FileName;

            var path = _Config.GetUploadPath(fileName);
            using (var stream = File.OpenWrite(path))
            {
                //将文件内容复制到流中
                await request.file.CopyToAsync(stream);
            }

            return fileName;
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<FileResult> ViewFileAsync(string file)
        {
            var path = _Config.GetUploadPath(file);
            using (var stream = File.OpenRead(path))
            {
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, bytes.Length);
                return new FileContentResult(bytes, "image/png");
            }
        }
    }
}
