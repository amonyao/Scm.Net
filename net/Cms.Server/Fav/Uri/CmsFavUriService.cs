using Com.Scm.Cms.Fav.Uri;
using Com.Scm.Cms.Fav.Uri.Dvo;
using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Exceptions;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Fav.Uri
{
    /// <summary>
    /// 服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Cms")]
    public class CmsFavUriService : ApiService
    {
        private readonly SugarRepository<CmsFavUriDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;

        public CmsFavUriService(SugarRepository<CmsFavUriDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            EnvConfig envConfig)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _EnvConfig = envConfig;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResult<CmsFavUriDvo>> GetPagesAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .WhereIF(IsValidId(request.cat_id), a => a.cat_id == request.cat_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.title.Contains(request.key) || a.uri.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsFavUriDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CmsFavUriDvo>> GetListAsync(SearchRequest request)
        {
            var result = await _thisRepository.AsQueryable()
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .WhereIF(IsValidId(request.cat_id), a => a.cat_id == request.cat_id)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.title.Contains(request.key) || a.uri.Contains(request.key))
                .OrderBy(m => m.id)
                .Select<CmsFavUriDvo>()
                .ToListAsync();

            Prepare(result);
            return result;
        }

        private void Prepare(List<CmsFavUriDvo> list)
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
        public async Task<CmsFavUriDvo> GetAsync(long id)
        {
            var model = await _thisRepository.GetByIdAsync(id);
            return model.Adapt<CmsFavUriDvo>();
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsFavUriDvo> GetEditAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsFavUriDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CmsFavUriDvo> GetViewAsync(long id)
        {
            return await _thisRepository
                .AsQueryable()
                .Select<CmsFavUriDvo>()
                .FirstAsync(m => m.id == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(CmsFavUriDto model)
        {
            model.TrimUri();
            var dao = await _thisRepository.GetFirstAsync(a => a.uri == model.uri);
            if (dao != null)
            {
                return true;
            }

            dao = model.Adapt<CmsFavUriDao>();
            var result = await _thisRepository.InsertAsync(dao);
            if (!result)
            {
                throw new BusinessException("数据添加异常，请重试！");
            }

            try
            {
                var reader = new FaviconReader();
                result = await reader.Read(_EnvConfig.GetImagesPath("favicon"), dao.uri);
                if (result)
                {
                    dao.icon = reader.FileName;
                    await _thisRepository.UpdateAsync(dao);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsFavUriDto model)
        {
            model.TrimUri();
            var dao = await _thisRepository.GetFirstAsync(a => a.uri == model.uri && a.id != model.id);
            if (dao != null)
            {
                return;
            }

            dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                return;
            }

            dao = CommonUtils.Adapt(model, dao);
            try
            {
                var reader = new FaviconReader();
                var result = await reader.Read(_EnvConfig.GetImagesPath("favicon"), dao.uri);
                if (result)
                {
                    dao.icon = reader.FileName;
                    await _thisRepository.UpdateAsync(dao);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

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

        /// <summary>
        /// 访问链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<bool> OpenAsync(long id)
        {
            var dao = await _thisRepository.GetByIdAsync(id);
            if (dao == null)
            {
                return false;
            }

            dao.qty += 1;
            await _thisRepository.UpdateAsync(dao);
            return true;
        }
    }
}