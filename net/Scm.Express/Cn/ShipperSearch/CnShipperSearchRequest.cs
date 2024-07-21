namespace Com.Scm.Express.CnExpress.ShipperSearch
{
    public class CnShipperSearchRequest : CnRequest
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
