namespace Com.Scm.Express.My.Shipper.JDL
{
    public class JdlResponse
    {
        public virtual bool IsSuccess()
        {
            return true;
        }

        public virtual string GetMessage()
        {
            return "";
        }
    }
}
