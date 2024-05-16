using Com.Scm.Enums;
using Com.Scm.Exceptions;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Ur;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 商品服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Pos")]
    public class PosResSpuService : ApiService
    {
        private readonly SugarRepository<PosResSpuDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PosResSpuService(SugarRepository<PosResSpuDao> thisRepository, SugarRepository<UserDao> userRepository)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<PosResSpuDvo>> GetPagesAsync(ScmSearchPageRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.option_id), a => a.option_id == request.option_id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(a => a.id)
                .Select<PosResSpuDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PosResSpuDvo>> GetListAsync(ScmSearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .Where(a => a.row_status == ScmStatusEnum.Enabled)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(a => a.id)
                .Select<PosResSpuDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void Prepare(List<PosResSpuDvo> list)
        {
            foreach (var item in list)
            {
                var updateDao = _userRepository.GetById(item.update_user);
                item.update_names = updateDao?.names;

                var createDao = _userRepository.GetById(item.create_user);
                item.create_names = createDao?.names;
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PosResSpuDto> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<PosResSpuDto>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PosResSpuDto> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<PosResSpuDto>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PosResSpuDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<PosResSpuDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(PosResSpuDto model)
        {
            var dao = await _thisRepository.GetFirstAsync(a => a.codec == model.codec);
            if (dao != null)
            {
                throw new BusinessException($"已存在编码为{model.codec}的商品！");
            }

            if (string.IsNullOrWhiteSpace(model.names))
            {
                model.names = model.namec;
            }
            dao = await _thisRepository.GetFirstAsync(a => a.names == model.names);
            if (dao != null)
            {
                throw new BusinessException($"已存在简称为{model.names}的商品！");
            }

            return await _thisRepository.InsertAsync(model.Adapt<PosResSpuDao>());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(PosResSpuDto model)
        {
            var dao = await _thisRepository.GetFirstAsync(a => a.codec == model.codec && a.id != model.id);
            if (dao != null)
            {
                throw new BusinessException($"已存在编码为{model.codec}的商品！");
            }

            if (string.IsNullOrWhiteSpace(model.names))
            {
                model.names = model.namec;
            }
            dao = await _thisRepository.GetFirstAsync(a => a.names == model.names && a.id != model.id);
            if (dao != null)
            {
                throw new BusinessException($"已存在简称为{model.names}的商品！");
            }

            dao = await _thisRepository.GetByIdAsync(model.id);
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