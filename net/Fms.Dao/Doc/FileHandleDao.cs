using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fes.Doc
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_doc_file_handle")]
    public class FileHandleDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Enums.ScmHandleEnum handle { get; set; }
    }
}