using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_res_tag")]
    public class TagDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}