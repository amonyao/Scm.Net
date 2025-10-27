using Com.Scm.Dao;
using Com.Scm.Enums;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Log;

/// <summary>
/// 登录日志
/// </summary>
[SugarTable("scm_log_api")]
public class LogApiDao : ScmDao
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public ScmLogLevelEnum level { get; set; }

    /// <summary>
    /// 日志类型  1=登录  2=操作
    /// </summary>
    [Required]
    public ScmLogTypesEnum types { get; set; }

    /// <summary>
    /// 操作模块
    /// </summary>
    public string module { get; set; }

    /// <summary>
    /// 提交类型：get/post/delete
    /// </summary>
    public string method { get; set; }

    /// <summary>
    /// 浏览器信息
    /// </summary>
    public string browser { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string agent { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    public string operate_user { get; set; }
    /// <summary>
    /// 操作日期
    /// </summary>
    public string operate_date { get; set; }
    /// <summary>
    /// 操作时间
    /// </summary>
    [Required]
    public long operate_time { get; set; }

    /// <summary>
    /// 操作类型:例如添加、修改
    /// </summary>
    public string operate_type { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string ip { get; set; }

    /// <summary>
    /// 操作地址
    /// </summary>
    public string url { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string parameters { get; set; }

    /// <summary>
    /// 详细信息
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// 返回结果
    /// </summary>
    public string content { get; set; }

    /// <summary>
    /// 执行时长
    /// </summary>
    [Required]
    public int duration { get; set; }

    /// <summary>
    /// 操作状态
    /// </summary>
    [Required]
    public int status { get; set; } = 1;
}