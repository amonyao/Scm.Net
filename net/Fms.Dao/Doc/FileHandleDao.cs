using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_doc_file_handle")]
    public class FileHandleDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Enums.ScmHandleEnum handle { get; set; }
    }
}