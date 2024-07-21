namespace Com.Scm.Express.JdExpress.ShipperSearch
{
    public class JdShipperSearchRequest : JdRequest
    {
        public string providerState { get; set; }

        public override string GetServicePath()
        {
            return "/query/logisticproviderlist";
        }

        public override string GetDomain()
        {
            return "ElectronicSingleSurface";
        }
    }
}
