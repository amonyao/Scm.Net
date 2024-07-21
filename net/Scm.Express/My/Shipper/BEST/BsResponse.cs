namespace Com.Scm.Express.My.Shipper.BEST
{
    public class BsResponse
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
