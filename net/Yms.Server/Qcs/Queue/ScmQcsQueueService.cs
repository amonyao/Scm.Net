using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Exceptions;
using Com.Scm.Jwt;
using Com.Scm.Qcs;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Com.Scm.Yms.Qcs.Queue.Dvo;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Yms.Qcs.Queue
{
    /// <summary>
    /// 号码服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "qcs")]
    public class ScmQcsQueueService : ApiService
    {
        private readonly SugarRepository<ScmQcsQueueDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly JwtContextHolder _jwtContextHolder;
        private readonly SugarRepository<ScmQcsDetailDao> _detailRepository;

        public ScmQcsQueueService(SugarRepository<ScmQcsQueueDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            JwtContextHolder jwtContextHolder,
            SugarRepository<ScmQcsDetailDao> detailRepository)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _jwtContextHolder = jwtContextHolder;
            _detailRepository = detailRepository;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<ScmQcsQueueDvo>> GetPagesAsync(SearchRequest request)
        {
            var userId = _jwtContextHolder.GetToken().user_id;

            var result = await _thisRepository.AsQueryable()
                .Where(a => a.detail_id == request.detail_id && a.user_id == userId)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                //.WhereIF(IsValidId(request.id), a => a.detail_id == request.id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.lv, SqlSugar.OrderByType.Desc)
                .OrderBy(m => m.id)
                .Select<ScmQcsQueueDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ScmQcsQueueDvo>> GetListAsync(SearchRequest request)
        {
            var userId = _jwtContextHolder.GetToken().user_id;

            var result = await _thisRepository.AsQueryable()
                .Where(a => a.detail_id == request.detail_id && a.user_id == userId)
                .Where(a => a.row_status == Enums.ScmStatusEnum.Enabled)
                //.WhereIF(IsValidId(request.id), a => a.detail_id == request.id)
                //.WhereIF(!string.IsNullOrEmpty(request.key), a => a.text.Contains(request.key))
                .OrderBy(m => m.lv, SqlSugar.OrderByType.Desc)
                .OrderBy(m => m.id)
                .Select<ScmQcsQueueDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<ScmQcsQueueDvo> list)
        {
            foreach (var item in list)
            {
                item.update_names = GetUserNames(_userRepository, item.update_user);
                item.create_names = GetUserNames(_userRepository, item.create_user);
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ScmQcsQueueDvo> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<ScmQcsQueueDvo>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ScmQcsQueueDvo> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<ScmQcsQueueDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ScmQcsQueueDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<ScmQcsQueueDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(ScmQcsQueueDto model)
        {
            var dao = model.Adapt<ScmQcsQueueDao>();
            return await _thisRepository.InsertAsync(dao);
        }

        /// <summary>
        /// 取号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ScmQcsQueueDvo> QueuingAsync(QueuingRequest request)
        {
            var detailDao = await _detailRepository.GetByIdAsync(request.id);
            if (detailDao == null)
            {
                throw new BusinessException("无效的队列信息！");
            }

            // 累加取号
            detailDao.qty += 1;

            var dao = new ScmQcsQueueDao();
            dao.header_id = detailDao.header_id;
            dao.detail_id = detailDao.id;
            dao.idx = detailDao.qty;
            dao.codec = detailDao.GenCodec();
            dao.handle = QcsQueueHandleEnums.Todo;
            await _thisRepository.InsertAsync(dao);

            await _detailRepository.UpdateAsync(detailDao);

            return dao.Adapt<ScmQcsQueueDvo>();
        }

        /// <summary>
        /// 叫号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [HttpGet]
        public async Task<ScmQcsQueueDvo> CallingAsync(CallingRequest request)
        {
            var userId = _jwtContextHolder.GetToken().user_id;

            if (request.dir == CallingDirEnums.Repeat)
            {
                var dao = await _thisRepository.GetFirstAsync(a => a.detail_id == request.id
                    && a.user_id == userId
                    && a.handle == QcsQueueHandleEnums.Doing
                    && a.row_status == Enums.ScmStatusEnum.Enabled, a => a.idx);

                if (dao == null)
                {
                    throw new BusinessException("暂无待叫号信息！");
                }

                dao.qty += 1;
                await _thisRepository.UpdateAsync(dao);
                return dao.Adapt<ScmQcsQueueDvo>();
            }

            if (request.dir == CallingDirEnums.Forward)
            {
                var dao = await _thisRepository
                    .AsQueryable()
                    .Where(a => a.detail_id == request.id && a.handle == QcsQueueHandleEnums.Todo && a.row_status == Enums.ScmStatusEnum.Enabled)
                    .OrderBy(a => a.lv, SqlSugar.OrderByType.Desc)
                    .OrderBy(a => a.idx, SqlSugar.OrderByType.Asc)
                    .FirstAsync();

                if (dao == null)
                {
                    throw new BusinessException("暂无待叫号信息！");
                }

                dao.user_id = userId;
                dao.handle = QcsQueueHandleEnums.Doing;
                dao.qty += 1;
                await _thisRepository.UpdateAsync(dao);

                var detailDao = await _detailRepository.GetByIdAsync(request.id);
                detailDao.idx += 1;
                await _detailRepository.UpdateAsync(detailDao);

                return dao.Adapt<ScmQcsQueueDvo>();
            }

            throw new BusinessException("无效的队列信息！");
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ScmQcsQueueDto model)
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