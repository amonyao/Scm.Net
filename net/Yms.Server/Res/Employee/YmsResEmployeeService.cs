using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Com.Scm.Yms.Res.Employee.Dvo;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Yms.Res.Employee
{
    /// <summary>
    /// 租客服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "yms")]
    public class YmsResEmployeeService : ApiService
    {
        private readonly SugarRepository<YmsResEmployeeDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly SugarRepository<YmsResMerchantDao> _merchantRepository;

        public YmsResEmployeeService(SugarRepository<YmsResEmployeeDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            SugarRepository<YmsResMerchantDao> merchantRepository)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _merchantRepository = merchantRepository;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<YmsResEmployeeDvo>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<YmsResEmployeeDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<YmsResEmployeeDvo>> GetListAsync(ScmSearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Enums.ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<YmsResEmployeeDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<YmsResEmployeeDvo> list)
        {
            foreach (var item in list)
            {
                item.merchant_names = _merchantRepository.GetById(item.merchant_id)?.names;
                item.update_names = GetUserNames(_userRepository, item.update_user);
                item.create_names = GetUserNames(_userRepository, item.create_user);
            }
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ResOptionDvo>> GetOptionAsync(ScmOptionRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Enums.ScmStatusEnum.Enabled)
                .OrderBy(a => a.id)
                .Select(a => new ResOptionDvo { id = a.id, label = a.names, value = a.id })
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<YmsResEmployeeDvo> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<YmsResEmployeeDvo>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<YmsResEmployeeDvo> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<YmsResEmployeeDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<YmsResEmployeeDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<YmsResEmployeeDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(YmsResEmployeeDto model) =>
            await _thisRepository.InsertAsync(model.Adapt<YmsResEmployeeDao>());

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(YmsResEmployeeDto model)
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