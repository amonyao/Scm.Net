using Com.Scm.Express.My;

namespace Com.Scm.Express.OrderFilter
{
    public class OrderFilterResponse : MyResponse<OrderFilterResult>
    {
    }

    public class OrderFilterResult
    {
        public UsableEnum usable { get; set; }
        public string reason { get; set; }
    }

    public enum UsableEnum
    {
        None,
        Success,
        Failure
    }
}
