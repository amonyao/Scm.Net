using Com.Scm.Cms.Res.Author.Dvo;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Cms.Res.Author
{
    /// <summary>
    /// 作者服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsResAuthorService : ApiService
    {
        private readonly SugarRepository<CmsResAuthorDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsResAuthorService(SugarRepository<CmsResAuthorDao> thisRepository,
            SugarRepository<UserDao> userRepository)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<CmsResAuthorDto>> GetPagesAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .WhereIF(IsValidId(request.dynasty_id), a => a.dynasty_id == request.dynasty_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.names.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsResAuthorDto>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CmsResAuthorDto>> GetListAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled)
                .WhereIF(IsValidId(request.dynasty_id), a => a.dynasty_id == request.dynasty_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.names.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsResAuthorDto>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ResOptionDvo>> GetOptionAsync(OptionRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Scm.Enums.ScmStatusEnum.Enabled && a.dynasty_id == request.dynasty_id)
                .OrderBy(a => a.id)
                .Select(a => new ResOptionDvo { id = a.id, label = a.names, value = a.id })
                .ToListAsync();

            return result;
        }

        private void Prepare(List<CmsResAuthorDto> list)
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
        public async Task<CmsResAuthorDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<CmsResAuthorDto>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsResAuthorDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsResAuthorDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsResAuthorDto> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsResAuthorDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CmsResAuthorDto model) =>
            await _thisRepository.InsertAsync(model.Adapt<CmsResAuthorDao>());

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsResAuthorDto model)
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
    }
}