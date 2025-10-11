using Com.Scm.Config;
using Com.Scm.Dvo;
using Com.Scm.Exceptions;
using Com.Scm.Samples.Book.Dao;
using Com.Scm.Samples.Book.Dto;
using Com.Scm.Samples.Book.Dvo;
using Com.Scm.Samples.Book.Rnr;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

namespace Com.Scm.Samples.Book
{
    /// <summary>
    /// 示例代码服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Samples")]
    public class SamplesBookService : ApiService, IBookService
    {
        private readonly SugarRepository<BookDao> _thisRepository;
        private readonly EnvConfig _Config;
        private static readonly Dictionary<long, BookDao> _Dict = new Dictionary<long, BookDao>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="thisRepository">数据仓库</param>
        /// <param name="userService">用户服务</param>
        /// <param name="envConfig">环境配置</param>
        public SamplesBookService(SugarRepository<BookDao> thisRepository,
            IUserService userService,
            EnvConfig envConfig)
        {
            _thisRepository = thisRepository;
            _UserService = userService;
            _Config = envConfig;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request">分页查询请求</param>
        /// <returns>分页查询结果数据</returns>
        public async Task<ScmSearchPageResponse<BookDvo>> GetPageAsync(SearchRequest request)
        {
            var isEmpty = string.IsNullOrWhiteSpace(request.key);
            var isCodes = !isEmpty && SamplesUtils.IsDemoCodes(request.key);

            var result = await _thisRepository.AsQueryable()
                //.Where(a => a.row_delete != ScmDeleteEnum.Yes)
                .WhereIF(request.types != Enums.BookTypesEnum.None, a => a.types == request.types)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .WhereIF(isCodes, a => a.codes == request.key)
                .WhereIF(!isEmpty && !isCodes, a => a.codec.Contains(request.key) || a.names.Contains(request.key))
                .OrderByDescending(a => a.id)
                .Select<BookDvo>()
                .ToPageAsync(request.page, request.limit);

            Prepare(result.Items);
            return result;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>查询结果数据</returns>
        public async Task<List<BookDvo>> GetListAsync(SearchRequest request)
        {
            var items = await _thisRepository.AsQueryable()
                //.Where(a => a.row_delete != ScmDeleteEnum.Yes)
                .WhereIF(request.types != Enums.BookTypesEnum.None, a => a.types == request.types)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .WhereIF(!string.IsNullOrEmpty(request.key), a => a.codec.Contains(request.key) || a.names.Contains(request.key))
                .OrderByDescending(a => a.id)
                .Select<BookDvo>()
                .ToListAsync();

            Prepare(items);
            return items;
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="list"></param>
        private void Prepare(List<BookDvo> list)
        {
            foreach (var item in list)
            {
                Prepare(item);

                // Others TODO
            }
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>下拉列表</returns>
        public async Task<List<ResOptionDvo>> GetOptionAsync(SearchRequest request)
        {
            var items = await _thisRepository.AsQueryable()
                //.Where(a => a.row_delete != ScmDeleteEnum.Yes)
                .WhereIF(request.types != Enums.BookTypesEnum.None, a => a.types == request.types)
                .WhereIF(!request.IsAllStatus(), a => a.row_status == request.row_status)
                .OrderByDescending(a => a.id)
                .Select(a => new ResOptionDvo { id = a.id, label = a.namec, value = a.id })
                .ToListAsync();

            return items;
        }

        /// <summary>
        /// 编辑读取
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <returns>书籍编辑对象</returns>
        [HttpGet("{id}")]
        public async Task<BookDto> GetEditAsync(long id)
        {
            var dto = await _thisRepository.AsQueryable()
                .Select<BookDto>()
                .FirstAsync(m => m.id == id);

            // 其它处理
            // ...

            return dto;
        }

        /// <summary>
        /// 查看读取
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <returns>书籍查看对象</returns>
        [HttpGet("{id}")]
        public async Task<BookDvo> GetViewAsync(long id)
        {
            var dvo = await _thisRepository.AsQueryable()
                .Select<BookDvo>()
                .FirstAsync(m => m.id == id);

            // 其它处理
            // ...

            return dvo;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">书籍对象</param>
        /// <returns>是否添加成功</returns>
        public async Task<bool> AddAsync(BookDto model)
        {
            // 对象为空检测
            if (model == null)
            {
                throw new BusinessException("请求对象不能为空！");
            }

            // 编码重复检测
            var dao = await _thisRepository.GetFirstAsync(a => a.codec == model.codec);
            if (dao != null)
            {
                throw new BusinessException($"已存在编码为{model.codec}的书籍！");
            }

            // 浅复制，通常使用此方法即可
            dao = model.Adapt<BookDao>();
            // 深复制
            //dao = model.Clone<BookDao>();

            // 插入对象
            return await _thisRepository.InsertAsync(dao);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">书籍对象</param>
        /// <returns>是否更新成功</returns>
        public async Task<bool> UpdateAsync(BookDto model)
        {
            // 对象为空检测
            if (model == null)
            {
                throw new BusinessException("请求对象不能为空！");
            }

            // 编码重复检测
            var dao = await _thisRepository.GetFirstAsync(a => a.codec == model.codec && a.id != model.id);
            if (dao != null)
            {
                throw new BusinessException($"已存在编码为{model.codec}的书籍！");
            }

            // 获取书籍信息
            dao = await _thisRepository.GetByIdAsync(model.id);
            if (dao == null)
            {
                throw new BusinessException("无效的书籍！");
            }

            // 浅复制，通常使用此方法即可
            dao = model.Adapt(dao);
            // 深复制
            //dao = model.Clone(dao);

            // 清除缓存
            RemoveCacheById(dao.id);

            // 更新对象
            return await _thisRepository.UpdateAsync(dao);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="param">更新请求对象</param>
        /// <returns>更新成功数量</returns>
        public async Task<int> StatusAsync(ScmChangeStatusRequest param)
        {
            return await UpdateStatus(_thisRepository, param.ids, param.status);
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="ids">需要删除的ID列表，逗号分隔</param>
        /// <returns>删除成功数量</returns>
        [HttpDelete]
        public async Task<int> DeleteAsync(string ids)
        {
            return await DeleteRecord(_thisRepository, ids.ToListLong());
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="request">文件上传请求</param>
        /// <returns>文件上传结果</returns>
        [HttpPost]
        public async Task<UploadResult> UploadAsync([FromForm] UploadRequest request)
        {
            var result = new UploadResult();

            //判断是否上传了文件内容
            if (request.file == null)
            {
                result.SetFailure("上传内容为空！");
                return result;
            }

            #region 保存文件
            //var fileName = request.file.FileName;
            //var ext = Path.GetExtension(fileName);
            //fileName = System.DateTime.UtcNow.Ticks.ToString() + ext;

            //var path = _Config.GetUploadPath(fileName);
            //using (var stream = File.OpenWrite(path))
            //{
            //    //将文件内容复制到流中
            //    await request.file.CopyToAsync(stream);
            //}
            //response.file = fileName;
            //response.SetSuccess("文件上传成功！");
            #endregion

            #region 数据导入
            using (var stream = request.file.OpenReadStream())
            {
                var list = stream.Query<BookExcelDvo>();
                foreach (var item in list)
                {
                    var dao = item.Clone<BookDao>();
                    await _thisRepository.InsertAsync(dao);
                }
            }
            #endregion

            result.SetSuccess("文件导入成功！");
            return result;
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="file">文件名称</param>
        /// <returns>文件内容</returns>
        public async Task<FileResult> ViewFileAsync(string file)
        {
            // 获取真实物理路径
            var path = _Config.GetUploadPath(file);

            using (var stream = File.OpenRead(path))
            {
                var bytes = new byte[stream.Length];
                await stream.ReadExactlyAsync(bytes, 0, bytes.Length);
                return new FileContentResult(bytes, "image/png");
            }
        }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <param name="useCache">是否缓存对象，默认是</param>
        /// <returns>书籍对象</returns>
        public BookDao GetDaoById(long id, bool useCache = true)
        {
            // 检测缓存
            if (useCache && _Dict.ContainsKey(id))
            {
                return _Dict[id];
            }

            // 数据库读取
            var dao = _thisRepository.GetById(id);

            // 缓存数据
            if (useCache && dao != null)
            {
                _Dict[id] = dao;
            }

            return dao;
        }

        /// <summary>
        /// 缓存处理
        /// </summary>
        /// <param name="id"></param>
        public bool RemoveCacheById(long id)
        {
            if (_Dict.ContainsKey(id))
            {
                return _Dict.Remove(id);
            }
            return false;
        }
    }
}
