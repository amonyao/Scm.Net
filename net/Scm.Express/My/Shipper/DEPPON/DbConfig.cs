namespace Com.Scm.Express.My.Shipper.DEPPON
{
    public class DbConfig : MyConfig
    {
        public DbConfig()
        {

        }

        public string customerCode { get; set; }
        public string orderType { get; set; }
        public string payType { get; set; }
        public string transportType { get; set; }
        public string isOut { get; set; }
        public string deliveryType { get; set; }

        public override void LoadDef()
        {
            customerCode = "219401";
            orderType = "1";
            payType = "2";
            transportType = "PACKAGE";
            isOut = "N";
            deliveryType = "4";
        }
    }
}
