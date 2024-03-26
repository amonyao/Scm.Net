using Com.Scm.Cms.Res.Cat.Dvo;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Cms.Res.Cat
{
    /// <summary>
    /// 服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsResCatService : ApiService
    {
        private readonly SugarRepository<CmsResCatDao> _thisRepository;
        public CmsResCatService(SugarRepository<CmsResCatDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 查询所有——分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<CmsResCatDto>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var query = await _thisRepository.AsQueryable()
                .OrderByDescending(m => m.id)
                .Select<CmsResCatDto>()
                .ToPageAsync(request.page, request.limit);
            return query;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<CmsResCatDto>> GetListAsync(ScmSearchPageRequest param)
        {
            var list = await _thisRepository
                .AsQueryable()
                .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled)
                .OrderByDescending(m => m.id)
                .Select<CmsResCatDto>()
                .ToListAsync();
            return list;
        }

        public async Task<List<ResOptionDvo>> GetOptionAsync(OptionRequest request)
        {
            var list = await _thisRepository
                .AsQueryable()
                .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled && a.id != request.id)
                .OrderByDescending(m => m.id)
                .Select(a => new ResOptionDvo { id = a.id, label = a.namec, value = a.id, parentId = a.pid })
                .ToListAsync();
            return list;
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsResCatDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<CmsResCatDto>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsResCatDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsResCatDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsResCatDto> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsResCatDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CmsResCatDto model)
        {
            //model.ParentId = long.Parse(model.ParentIdList.Last());
            //var upModel = await _thisRepository.GetFirstAsync(m => true, m => m.Sort);
            //model.Sort = upModel.Sort + 1;
            return await _thisRepository.InsertAsync(model.Adapt<CmsResCatDao>());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(CmsResCatDto model)
        {
            //model.ParentId = long.Parse(model.ParentIdList.Last());
            return await _thisRepository.UpdateAsync(model.Adapt<CmsResCatDao>());
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