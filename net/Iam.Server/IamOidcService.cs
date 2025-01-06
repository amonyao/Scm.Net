using Com.Scm.Iam.Cfg;
using Com.Scm.Iam.Dvo;
using Com.Scm.Iam.Res;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Com.Scm.Iam
{
    /// <summary>
    /// 服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Iam")]
    public class IamOidcService : ApiService
    {
        private readonly SugarRepository<IamOidcDetailDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public IamOidcService(ISqlSugarClient client, SugarRepository<IamOidcDetailDao> thisRepository, SugarRepository<UserDao> userRepository)
        {
            _SqlClient = client;
            _thisRepository = thisRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<IamOidcDvo>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<IamOidcDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<IamOidcDvo>> GetListAsync(ScmSearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == Enums.ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<IamOidcDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<IamOidcDvo> list)
        {
            var ospDict = new Dictionary<long, IamResOspDao>();

            foreach (var item in list)
            {
                IamResOspDao ospDao;
                if (!ospDict.ContainsKey(item.osp_id))
                {
                    ospDao = _SqlClient.Queryable<IamResOspDao>().First(a => a.id == item.osp_id);
                    if (ospDao != null)
                    {
                        ospDict[item.osp_id] = ospDao;
                    }
                }
                else
                {
                    ospDao = ospDict[item.osp_id];
                }
                item.osp_code = ospDao?.code;
                item.osp_name = ospDao?.name;

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
        public async Task<IamOidcDvo> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<IamOidcDvo>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IamOidcDvo> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<IamOidcDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IamOidcDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<IamOidcDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(IamOidcDetailDto model) =>
            await _thisRepository.InsertAsync(model.Adapt<IamOidcDetailDao>());

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IamOidcDetailDto model)
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