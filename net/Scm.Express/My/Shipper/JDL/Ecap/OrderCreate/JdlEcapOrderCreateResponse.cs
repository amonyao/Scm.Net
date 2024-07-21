namespace Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCreate
{
    public class JdlEcapOrderCreateResponse : JdlEcapResponse<JdlEcapCreateOrderResult>
    {

    }

    public class JdlEcapCreateOrderResult
    {
        /// <summary>
        /// 京东物流订单号，可用此单号作为条件查询订单详情、费用等	
        /// </summary>
        public string orderCode { get; set; }

        /// <summary>
        /// 京东物流运单号，可用此单号作为条件查询订单详情、费用等	
        /// </summary>
        public string waybillCode { get; set; }

        /// <summary>
        /// 预计揽收时间，时间戳，ms 备注：以小哥实际上门时间为准	
        /// </summary>
        public long pickupPromiseTime { get; set; }

        /// <summary>
        /// 预计送达时间，时间戳，ms 备注：以实际送达时间为准	
        /// </summary>
        public long deliveryPromiseTime { get; set; }

        /// <summary>
        /// 预估运费；根据请求参数返回的预估的运费；揽收完成后使用运费查询接口查询相关费用	
        /// </summary>
        public double freightPre { get; set; }

        /// <summary>
        /// B2C下单，若下单的 起始站点预分拣结果类型 或者 目的站点预分拣结果类型是0：待人工预分拣的话，是true，其他是false	
        /// </summary>
        public bool needRetry { get; set; }

        /// <summary>
        /// 揽配信息，可用于面单打印
        /// </summary>
        public FaceSheetResponse faceSheetResponse { get; set; }
    }

    public class FaceSheetResponse
    {
        public FaceSheetInfo faceSheetInfo { get; set; }
    }

    public class FaceSheetInfo
    {
        /// <summary>
        /// 揽收站点名称	
        /// </summary>
        public string startSiteName { get; set; }
        public long startSiteId { get; set; }

        /// <summary>
        /// 配送站点名称	
        /// </summary>
        public string endSiteName { get; set; }
        public long endSiteId { get; set; }

        /// <summary>
        /// 路区	
        /// </summary>
        public string road { get; set; }

        /// <summary>
        /// 始发分拣中心名字	
        /// </summary>
        public string sourceSortCenterName { get; set; }

        /// <summary>
        /// 始发道口号	
        /// </summary>
        public string originalCrossCode { get; set; }

        /// <summary>
        /// 始发笼车号	
        /// </summary>
        public string originalTabletrolleyCode { get; set; }

        /// <summary>
        /// 目的分拣中心名称	
        /// </summary>
        public string targetSortCenterName { get; set; }

        /// <summary>
        /// 目的道口号	
        /// </summary>
        public string destinationCrossCode { get; set; }

        /// <summary>
        /// 目的笼车号	
        /// </summary>
        public string destinationTabletrolleyCode { get; set; }

        /// <summary>
        /// 时效（0:无时效，1：当日达，2：次日达，3：隔日达，4：4日达及以上）	
        /// </summary>
        public int aging { get; set; }

        /// <summary>
        /// 时效名称	
        /// </summary>
        public string agingName { get; set; }

        /// <summary>
        /// 站点类型	
        /// </summary>
        public int siteType { get; set; }

        /// <summary>
        /// 是否隐藏收件人姓名 (0,否 1,是 )	
        /// </summary>
        public int isHideName { get; set; }

        /// <summary>
        /// 是否隐藏收件人联系方式 (0,否 1,是 )	
        /// </summary>
        public int isHideContractNumbers { get; set; }

        /// <summary>
        /// 运输方式： 0、公路， 1、航空， 2、高铁	
        /// </summary>
        public int transType { get; set; }

        /// <summary>
        /// 预分拣状态值	
        /// </summary>
        public int preSortCode { get; set; }

        /// <summary>
        /// 物流模式：0、快递承载, 1、快运B承载	
        /// </summary>
        public int deliveryType { get; set; }

        /// <summary>
        /// 快运卡位号 对应快递路区	
        /// </summary>
        public string truckSpot { get; set; }

        /// <summary>
        /// 主产品类型:1-特惠送,2-特快送,4-特瞬送城际,5-同城当日达,6-特快次晨,7-微小件,8-生鲜专送,9-尊享专送,10-京准达2H,11-京准达5H,12-O2O-1小时,13-O2O-2小时,14-特瞬送同城,15-同城速配,16-生鲜速达,17-生鲜惠达,18-京准达0.5H,19-京准达1H,20-函速达,21-特惠小包,22-医药冷链,23-全城送,24-特惠小件,25-智享送,26-冷链专送	
        /// </summary>
        public int mainProductCode { get; set; }

        /// <summary>
        /// 主产品是否降级	
        /// </summary>
        public bool mainProductDownGrade { get; set; }

        /// <summary>
        /// 商家订单号	
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 运营模式 1-特快送 2-同城当日达 3-特快次晨 4-特快陆运	
        /// </summary>
        public int expressOperationMode { get; set; }

        /// <summary>
        /// 温层 （1:普通，3:填仓，4:特配，5:鲜活，6:控温，7:冷藏，8:冷冻，9:深冷）默认是1	
        /// </summary>
        public int goodsType { get; set; }

        /// <summary>
        /// 面单集货地名称（面单集包地显示值）	
        /// </summary>
        public string collectionAddress { get; set; }

        /// <summary>
        /// 分拣中心代码	
        /// </summary>
        public string distributeCode { get; set; }

        /// <summary>
        /// 代表时效产品简码 T水印	
        /// </summary>
        public string coverCode { get; set; }

        /// <summary>
        /// 二维码转换链接	
        /// </summary>
        public string qrcodeUrl { get; set; }
    }
}
