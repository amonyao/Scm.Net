using Com.Scm.Dao;
using Com.Scm.Enums;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Adm.Dic;

/// <summary>
/// 字典分类信息
/// </summary>
[SugarTable("scm_sys_dic_header")]
public class AdmDicHeaderDao : ScmDataDao, ISystemDao, IDeleteDao, ISortableDao
{
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
    /// 父节点
    /// </summary>
    [Required]
    public long ParentId { get; set; }

    /// <summary>
    /// 层级
    /// </summary>
    [Required]
    public int Layer { get; set; } = 1;

    /// <summary>
    /// 是否系统内置集成
    /// </summary>
    [Required]
    public ScmSystemEnum row_system { get; set; } = ScmSystemEnum.No;

    /// <summary>
    /// 
    /// </summary>
    public ScmDeleteEnum row_delete { get; set; } = ScmDeleteEnum.No;

    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<AdmDicDetailDao> details { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codec"></param>
    /// <returns></returns>
    public AdmDicDetailDao GetDetailByCodec(string codec)
    {
        if (details != null)
        {
            foreach (var detail in details)
            {
                if (detail.codec == codec)
                {
                    return detail;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="namec"></param>
    /// <returns></returns>
    public AdmDicDetailDao GetDetailByNamec(string namec)
    {
        if (details != null)
        {
            foreach (var detail in details)
            {
                if (detail.namec == namec)
                {
                    return detail;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public AdmDicDetailDao GetDetail(int value)
    {
        if (details != null)
        {
            foreach (var detail in details)
            {
                if (detail.value == value)
                {
                    return detail;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cat"></param>
    /// <returns></returns>
    public List<AdmDicDetailDao> GetDetailByCat(int cat)
    {
        var list = new List<AdmDicDetailDao>();
        if (details != null)
        {
            foreach (var detail in details)
            {
                if (detail.cat == cat)
                {
                    list.Add(detail);
                }
            }
        }
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public AdmDicDetailDao GetDetailByTag(int tag)
    {
        if (details != null)
        {
            foreach (var detail in details)
            {
                if (detail.tag == tag)
                {
                    return detail;
                }
            }
        }

        return null;
    }
}