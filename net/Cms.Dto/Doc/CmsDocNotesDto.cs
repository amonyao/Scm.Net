using Com.Scm.Cms.Enums;
using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    public class CmsDocNotesDto : ScmDataDto
    {
        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NoteTypesEnum types { get; set; }

        /// <summary>
        /// 主标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 子标题
        /// </summary>
        public string sub_title { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string content { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public int ver { get; set; }

        public bool IsTooLong()
        {
            var tmp = this.content ?? "";
            return tmp.Length > CmsDocNoteDto.CONTENT_SIZE;
        }

        public string ToDbSummary()
        {
            var tmp = this.content ?? "";
            if (tmp.Length > CmsDocNoteDto.SUMMARY_SIZE)
            {
                tmp = tmp.Substring(0, CmsDocNoteDto.SUMMARY_SIZE);
            }
            return tmp;
        }

        public string ToDbContent()
        {
            var tmp = this.content ?? "";
            if (tmp.Length > CmsDocNoteDto.CONTENT_SIZE)
            {
                tmp = tmp.Substring(0, CmsDocNoteDto.CONTENT_SIZE);
            }
            return tmp;
        }
    }
}
