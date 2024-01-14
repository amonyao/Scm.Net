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
    public class FesResCatService : ApiService
    {
        private readonly SugarRepository<CatDao> _thisRepository;
        public FesResCatService(SugarRepository<CatDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 查询所有——分页
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PageResult<Com.Scm.Fes.Doc.CatDvo>> GetPagesAsync(ScmSearchPageRequest param)
        {
            var query = await _thisRepository.AsQueryable()
                //.WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .OrderBy(m => m.id)
                .Select<Com.Scm.Fes.Doc.CatDvo>()
                .ToPageAsync(param.page, param.limit);
            return query;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<Com.Scm.Fes.Doc.CatDvo>> GetListAsync(ScmSearchRequest param)
        {
            var list = await _thisRepository.AsQueryable()
                        .OrderBy(m => m.id)
                        .Select<Com.Scm.Fes.Doc.CatDvo>()
                        .ToListAsync();
            return list;
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CatDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<CatDto>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CatDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CatDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Com.Scm.Fes.Doc.CatDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<Com.Scm.Fes.Doc.CatDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CatDto model)
        {
            return await _thisRepository.InsertAsync(model.Adapt<CatDao>());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(CatDto model)
        {
            return await _thisRepository.UpdateAsync(model.Adapt<CatDao>());
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