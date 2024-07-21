namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderSearch
{
    public class SfOrderSearchRequest : SfExpRequest
    {
        public string orderId { get; set; }

        /// <summary>
        /// 1正向单 2退货单
        /// </summary>
        public string searchType { get; set; }

        public string language { get; set; }

        public override string GetServicePath()
        {
            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_SEARCH_ORDER_RESP";
        }
    }
}
