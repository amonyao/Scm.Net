using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Adm.Dic;

/// <summary>
/// 字典分类信息
/// </summary>
public class AdmDicHeaderDto : ScmDataDto
{
    /// <summary>
    /// 父节点
    /// </summary>
    [Required]
    public long ParentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<string> ParentIdList { get; set; }

    /// <summary>
    /// 层级
    /// </summary>
    [Required]
    public int Layer { get; set; } = 1;

    /// <summary>
    /// 分类名称
    /// </summary>
    public string namec { get; set; }

    /// <summary>
    /// 分类标识
    /// </summary>
    public string codec { get; set; }

    /// <summary>
    /// 1=系统 2=商城
    /// </summary>
    [Required]
    public int types { get; set; } = 1;

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    public int od { get; set; } = 1;

    /// <summary>
    /// 是否系统内置集成
    /// </summary>
    [Required]
    public bool IsSystem { get; set; }
}