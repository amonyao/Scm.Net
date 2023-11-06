namespace Com.Scm.Yms.Fac
{
    public enum FacPortTypesEnums
    {
        None = 0,
        /// <summary>
        /// 入口
        /// </summary>
        Enter = 1,
        /// <summary>
        /// 出口
        /// </summary>
        Exit = 2,
        /// <summary>
        /// 出入口
        /// </summary>
        All = 3
    }

    public enum FacPortModesEnums
    {
        None = 0,
        /// <summary>
        /// 人行
        /// </summary>
        Human = 1,
        /// <summary>
        /// 货行
        /// </summary>
        Cargo = 2,
        /// <summary>
        /// 通行
        /// </summary>
        All = 3
    }
}