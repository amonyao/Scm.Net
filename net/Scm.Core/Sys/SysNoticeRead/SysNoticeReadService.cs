using Com.Scm.Dsa;
using Com.Scm.Msg.Notice;
using Com.Scm.Sys.SysNoticeRead.Dto;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Sys.SysNoticeRead;

/// <summary>
/// 通知已读模块服务接口
/// </summary>
[ApiExplorerSettings(GroupName = "Sys")]
public class SysNoticeReadService : IApiService
{
    private readonly SugarRepository<NoticeReaderDao> _thisRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="thisRepository"></param>
    public SysNoticeReadService(SugarRepository<NoticeReaderDao> thisRepository)
    {
        _thisRepository = thisRepository;
    }

    /// <summary>
    /// 查询所有——分页
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<ScmSearchPageResponse<SysNoticeReadDto>> GetPagesAsync(ScmSearchPageRequest param)
    {
        var query = await _thisRepository.AsQueryable()
            .Select<SysNoticeReadDto>()
            .ToPageAsync(param.page, param.limit);
        return query;
    }

    /// <summary>
    /// 根据主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<SysNoticeReadDto> GetAsync(long id)
    {
        var model = await _thisRepository.GetByIdAsync(id);
        return model.Adapt<SysNoticeReadDto>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(SysNoticeReadDto model) =>
        await _thisRepository.InsertAsync(model.Adapt<NoticeReaderDao>());

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(SysNoticeReadDto model) =>
        await _thisRepository.UpdateAsync(model.Adapt<NoticeReaderDao>());

    /// <summary>
    /// 删除,支持批量
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<bool> DeleteAsync([FromBody] List<long> ids) =>
        await _thisRepository.DeleteAsync(m => ids.Contains(m.id));
}
