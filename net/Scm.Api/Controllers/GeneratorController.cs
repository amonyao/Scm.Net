using Com.Scm.Filter;
using Com.Scm.Generator;
using Com.Scm.Generator.Dvo;
using Com.Scm.Result;
using Com.Scm.Swagger;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Com.Scm.Api.Controllers
{
    /// <summary>
    /// 代码生成
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiVersionInfo.V1)]
    public class GeneratorController : ApiController
    {
        private readonly IGeneratorService _generatorService;

        public GeneratorController(IGeneratorService generatorService)
        {
            _generatorService = generatorService;
        }

        [HttpGet("option")]
        public Dictionary<string, List<string>> OptionAsync()
        {
            return _generatorService.GetOptions();
        }

        [HttpGet("table")]
        public List<DbTableInfo> TableAsync(string key)
        {
            return _generatorService.GetTable(key);
        }


        [HttpGet("column")]
        public PageResult<DbColumnInfo> ColumnAsync(string table)
        {
            return new PageResult<DbColumnInfo>()
            {
                Items = _generatorService.GetColumn(table)
            };
        }

        [HttpPost, NoJsonResult]
        public IActionResult Post([FromBody] GeneratorTableRequest param)
        {
            if (_generatorService.CreateCode(param))
            {
                var path = "/" + _generatorService.FileName.Replace(@"\", "/");
                return File(path, "application/zip", "code.zip");
            }
            return BadRequest(_generatorService.Message);
        }
    }
}