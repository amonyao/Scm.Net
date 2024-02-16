namespace Com.Scm.Cms.Doc.Notes.Dvo
{
    public class NotesUploadResponse
    {
        public int errno { get; set; }
        public string message { get; set; }
        public NotesUploadData data { get; set; }

        public void SetFailure(string msg)
        {
            errno = 1;
            this.message = msg;
        }

        public void SetSuccess(string file)
        {
            this.data = new NotesUploadData();
            this.data.url = file;
        }
    }

    public class NotesUploadData
    {
        /// <summary>
        /// 图片 src ，必须
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图片描述文字，非必须
        /// </summary>
        public string alt { get; set; }
        /// <summary>
        /// 图片的链接，非必须
        /// </summary>
        public string href { get; set; }
    }
}
