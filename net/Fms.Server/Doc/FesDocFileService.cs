using Com.Scm.Config;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Fms;
using Com.Scm.Fms.Doc.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 服务接口
    /// </summary>
    [ApiExplorerSettings(GroupName = "Fes")]
    public class FesDocFileService : ApiService
    {
        private readonly SugarRepository<FileDao> _thisRepository;
        private readonly SugarRepository<UserDao> _userRepository;
        private readonly EnvConfig _envConfig;
        private readonly ISqlSugarClient _Client;
        private ImageAnylise _anyliser;

        public FesDocFileService(SugarRepository<FileDao> thisRepository,
            SugarRepository<UserDao> userRepository,
            EnvConfig envConfig,
            ISqlSugarClient client)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
            _envConfig = envConfig;
            _Client = client;
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
        /// 文件上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ScmUploadResponse> UploadAsync([FromForm] ScmUploadRequest request)
        {
            var response = new ScmUploadResponse();

            //判断是否上传了文件内容
            if (request.file == null)
            {
                response.SetFailure("上传内容为空！");
                return response;
            }

            var memory = new MemoryStream();
            request.file.CopyTo(memory);

            var hash = request.hash;
            if (hash == null)
            {
                hash = SecUtils.Md5(memory.ToArray());
            }
            request.hash = hash;

            var dao = await _thisRepository.GetFirstAsync(a => a.hash == hash);
            if (dao != null)
            {
                response.AddResult(new ScmUploadResult { file = dao.namec, path = dao.path });
                response.SetSuccess("文件上传成功！");
                return response;
            }

            await SaveFile(request, response, memory);
            return response;
        }

        private async Task SaveFile(ScmUploadRequest request, ScmUploadResponse response, Stream memory)
        {
            #region 保存文件
            var path = request.path ?? "/";
            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            var namec = request.file.FileName;
            var exts = Path.GetExtension(namec);
            if (!string.IsNullOrEmpty(exts))
            {
                exts = exts.ToLower();
            }

            var names = FesUtils.NextFileId().ToString() + exts;
            path += names;

            var filePath = _envConfig.GetUploadPath(path);

            using (var stream = File.OpenWrite(filePath))
            {
                memory.Position = 0;
                //将文件内容复制到流中
                await memory.CopyToAsync(stream);
            }
            response.AddResult(new ScmUploadResult { file = names, path = path });

            var fileDao = new FileDao();
            fileDao.codec = "";
            fileDao.namec = namec;
            fileDao.names = names;
            fileDao.path = path;
            fileDao.exts = exts;
            fileDao.hash = request.hash;
            fileDao.size = request.file_size;
            fileDao.file_create_time = TimeUtils.GetUnixTime();
            fileDao.file_modify_time = fileDao.file_create_time;
            await _thisRepository.InsertAsync(fileDao);

            var handleDao = new FileHandleDao();
            handleDao.id = fileDao.id;
            handleDao.handle = Enums.ScmHandleEnum.Todo;
            await _thisRepository.Change<FileHandleDao>().InsertAsync(handleDao);
            #endregion

            _anyliser = new ImageAnylise(_Client, _envConfig);
            await Task.Run(_anyliser.Run);

            response.SetSuccess("文件上传成功！");
        }
    }
}