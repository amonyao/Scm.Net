using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Note.Dvo
{
    public class NoteSearchRequest : ScmSearchPageRequest
    {
        public long cat_id { get; set; }

        public NoteTypesEnum types { get; set; }
    }
}
