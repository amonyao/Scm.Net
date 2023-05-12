using Com.Scm;
using Com.Scm.Api.Controllers;
using Com.Scm.Config;
using Com.Scm.Filter;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Scm.Api.Controllers
{
    [Route("upload")]
    public class UploadController : ApiController
    {
        private EnvConfig _Config;

        public UploadController(EnvConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("byfile")]
        public async Task<ScmUploadResponse> ByFileAsync(ScmUploadRequest request)
        {
            var response = new ScmUploadResponse();

            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.SetFailure("请选择文件！");
                return response;
            }

            var qty = 0;
            foreach (var file in files)
            {
                var name = file.Name;

                var exts = Path.GetExtension(file.FileName).ToLower();
                if (!IsAcceptExts(exts))
                {
                    response.SetFailure("不支持的文件类型！");
                    return response;
                }

                var dstFile = "";
                using (var stream = System.IO.File.OpenWrite(dstFile))
                {
                    await file.CopyToAsync(stream);
                }
                qty += 1;
            }

            response.SetSuccess(qty, $"成功上传 {qty} 个文件！");
            return response;
        }

        /// <summary>
        /// 分段上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("bypart")]
        public async Task<ScmUploadResponse> ByPartAsync(ScmUploadRequest request)
        {
            var response = new ScmUploadResponse();

            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.SetFailure("请选择文件！");
                return response;
            }

            var qty = 0;
            foreach (var file in files)
            {
                var name = file.Name;

                var exts = Path.GetExtension(file.FileName).ToLower();
                if (!IsAcceptExts(exts))
                {
                    response.SetFailure("不支持的文件类型！");
                    return response;
                }

                var dstFile = "";
                using (var stream = System.IO.File.OpenWrite(dstFile))
                {
                    await file.CopyToAsync(stream);
                }
                qty += 1;
            }

            response.SetSuccess(qty, $"成功上传 {qty} 个文件！");
            return response;
        }

        /// <summary>
        /// 摘要上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("byhash")]
        public async Task<ScmUploadResponse> ByHashAsync(ScmUploadRequest request)
        {
            var response = new ScmUploadResponse();

            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.SetFailure("请选择文件！");
                return response;
            }

            var qty = 0;
            foreach (var file in files)
            {
                var name = file.Name;

                var exts = Path.GetExtension(file.FileName).ToLower();
                if (!IsAcceptExts(exts))
                {
                    response.SetFailure("不支持的文件类型！");
                    return response;
                }

                var dstFile = "";
                using (var stream = System.IO.File.OpenWrite(dstFile))
                {
                    await file.CopyToAsync(stream);
                }
                qty += 1;
            }

            response.SetSuccess(qty, $"成功上传 {qty} 个文件！");
            return response;
        }

        private bool IsAcceptExts(string exts)
        {
            if (_Config == null || _Config.Exts == null)
            {
                return false;
            }

            foreach (var ext in _Config.Exts)
            {
                if (ext == exts)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpGet("avatar/{file}"), AllowAnonymous, NoJsonResult, NoAuditLog]
        public async Task<IActionResult> AvatarAsync(string file)
        {
            var path = _Config.GetAvatarPath(file);
            if (!System.IO.File.Exists(path))
            {
                var bytes = ImageUtils.DefaultAvatar();
                return File(bytes, "image/png");
            }

            using (var stream = System.IO.File.OpenRead(path))
            {
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, bytes.Length);
                return File(bytes, "image/png");
            }
        }
    }
}
