using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Cfg
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("iam_cfg_app_osp")]
    public class IamCfgAppOspDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long app_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long osp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(100)]
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(100)]
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}