using System.ComponentModel;

namespace Com.Scm.Enums
{
    public enum ScmHandleEnum
    {
        None = 0,
        /// <summary>
        /// 初始化
        /// </summary>
        [Description("初始化")]
        Init = 10,
        /// <summary>
        /// 待执行
        /// </summary>
        [Description("待执行")]
        Todo = 20,
        /// <summary>
        /// 进行中
        /// </summary>
        [Description("执行中")]
        Doing = 30,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("执行失败")]
        Failure = 40,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("执行成功")]
        Done = 50
    }
}
