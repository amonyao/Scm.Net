namespace Com.Scm.Express.My
{
    public class MyResponse<T> : ScmResponse
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public override bool IsSuccess()
        {
            return success;
        }

        public override string GetMessage()
        {
            return message;
        }

        public override void SetSuccess()
        {
            success = true;
            code = 0;
        }

        public override void SetSuccess(int code)
        {
            success = true;
            this.code = code;
        }

        public override void SetFailure(int code)
        {
            success = false;
            this.code = code;
        }

        public override void SetFailure(string message)
        {
            success = false;
            this.message = message;
        }
    }
}
