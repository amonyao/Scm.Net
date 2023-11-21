using Com.Scm.Cms.Fav.Uri.Dvo;
using Com.Scm.Dao.Ur;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Dvo;
using Com.Scm.Result;
using Com.Scm.Service;
using Com.Scm.Utils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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

        public CmsFavUriService(SugarRepository<CmsFavUriDao> thisRepository, SugarRepository<UserDao> userRepository)
        {
            _thisRepository = thisRepository;
            _userRepository = userRepository;
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
                .Where(a => a.row_status == Com.Scm.Enums.ScmStatusEnum.Enabled)
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
        public async Task<bool> AddAsync(CmsFavUriDto model) =>
            await _thisRepository.InsertAsync(model.Adapt<CmsFavUriDao>());

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CmsFavUriDto model)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        [HttpGet]
        [AllowAnonymous]
        public async void ReadIcoAsync(string address)
        {
            string url = GetHost(address);

            ReadIcon(url, "/favicon.ico");

            var html = await ReadHtml(url);
            if (string.IsNullOrEmpty(html))
            {
                return;
            }

            var regex = new Regex("<link\\b[^>]*/?>", RegexOptions.IgnoreCase);
            var match = regex.Match(html);
            while (match.Success)
            {
                var tag = match.Value;
                if (tag.EndsWith("/>"))
                {
                    tag = tag.Substring(0, tag.Length - 1) + "/>";
                }
                //tag = tag.Replace("link ", "");

                var link = TextUtils.AsXmlObject<LinkTag>(tag);
                if (link == null)
                {
                    continue;
                }
                var rel = link.rel ?? "";
                if (rel.ToLower().IndexOf("icon") < 0)
                {
                    continue;
                }

                var path = link.href;
                ReadIcon(url, path);
                break;
            }
        }

        private string GetHost(string url)
        {
            var idx = url.IndexOf("://");
            if (idx < 0)
            {
                url = "http://" + url;
                idx = "http://".Length;
            }

            idx = url.IndexOf('/', idx + 1);
            if (idx > 0)
            {
                return url.Substring(0, idx);
            }

            return url;
        }

        private bool IsPath2Host(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            return url.StartsWith("//") || url.IndexOf("://") > 0;
        }

        private async Task<bool> ReadIcon(string url, string path)
        {
            if (!IsPath2Host(path))
            {
                url = url + path;
            }

            var client = new HttpClient();
            try
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    byte[] buff = new byte[1024];
                    int length = 0;
                    using (MemoryStream memory = new MemoryStream())
                    {
                        while ((length = stream.Read(buff, 0, buff.Length)) > 0)
                        {
                            memory.Write(buff, 0, length);
                        }

                        var arr = memory.ToArray();
                        //info.functionIcon = Convert.ToBase64String(arr).Trim();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<string> ReadHtml(string url)
        {
            try
            {
                return await new HttpClient().GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }

    [XmlRoot("link")]
    public class LinkTag
    {
        [XmlAttribute("rel")]
        public string rel { get; set; }
        [XmlAttribute("href")]
        public string href { get; set; }
        [XmlAttribute("type")]
        public string type { get; set; }
    }
}