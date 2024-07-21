using System.Collections.Generic;

namespace Com.Scm.Express.JdExpress.ShipperSearch
{
    public class JdShipperSearchResponse : JdResponse<List<ProviderDTO>>
    {
    }

    public class ProviderDTO
    {
        public string contactMobile { get; set; }
        public bool supportCod { get; set; }
        public string cloudProviderCode { get; set; }
        public string providerCode { get; set; }
        public long autoRecoverTime { get; set; }
        public byte rangeType { get; set; }
        public string contactName { get; set; }
        public bool inPlatform { get; set; }
        public bool supportPaperless { get; set; }
        public bool autoRecover { get; set; }
        public int supportSyncSignContract { get; set; }
        public byte providerType { get; set; }
        public bool supportPickUp { get; set; }
        public bool supportMultiPackage { get; set; }
        public string approveComment { get; set; }
        public bool openCheckCode { get; set; }
        public byte operationType { get; set; }
        public int id { get; set; }
        public string contactPhone { get; set; }
        public string providerName { get; set; }
        public byte providerState { get; set; }
        public byte approveState { get; set; }
    }
}
