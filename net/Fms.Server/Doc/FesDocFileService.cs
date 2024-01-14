using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Fes.Doc;
using Com.Scm.Result;
using Com.Scm.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Application.Fes.Doc
{
    /// <summary>
    /// 服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Fes")]
    public class FesDocFileService : ApiService
    {
        private readonly SugarRepository<FileDao> _thisRepository;
        public FesDocFileService(SugarRepository<FileDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 查询所有——分页
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PageResult<FileDvo>> GetPagesAsync(ScmSearchPageRequest param)
        {
            var query = await _thisRepository.AsQueryable()
                //.WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .OrderBy(m => m.id)
                .Select<FileDvo>()
                .ToPageAsync(param.page, param.limit);
            return query;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<FileDvo>> GetListAsync(ScmSearchRequest param)
        {
            var list = await _thisRepository.AsQueryable()
                        .OrderBy(m => m.id)
                        .Select<FileDvo>()
                        .ToListAsync();
            return list;
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FileDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<FileDto>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FileDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<FileDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FileDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<FileDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(FileDto model)
        {
            return await _thisRepository.InsertAsync(model.Adapt<FileDao>());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(FileDto model)
        {
            return await _thisRepository.UpdateAsync(model.Adapt<FileDao>());
        }

        /// <summary>
        /// 删除,支持多个
        /// </summary>
        /// <param name="ids">逗号分隔</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync([FromBody] List<long> ids) =>
            await _thisRepository.DeleteAsync(m => ids.Contains(m.id));
    }
}