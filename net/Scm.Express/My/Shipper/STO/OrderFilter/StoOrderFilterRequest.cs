namespace Com.Scm.Express.My.Shipper.STO.OrderFilter
{
    public class StoOrderFilterRequest : StoRequest
    {
        /// <summary>
        /// 请求唯一标识	
        /// </summary>
        public string requestUUID { get; set; }
        /// <summary>
        /// 用于记录客户来源（填写客户品牌加业务如：申通-开放平台 提交流程时填写这个标识）	
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// 发件地址	
        /// </summary>
        public SendAddress sendAddress { get; set; }
        /// <summary>
        /// 收件地址	
        /// </summary>
        public ConsigneeAddress consigneeAddress { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public string cpSellerId { get; set; }
        /// <summary>
        /// 发货网站code
        /// </summary>
        public string sendBranchCode { get; set; }

        public override string GetApiName()
        {
            return "PLATFORM_SORTATION_ADDRESS_OUT_OF_DELIVERY";
        }

        public override string GetToAppKey()
        {
            return "sto_sortation";
        }

        public override string GetToCode()
        {
            return "sto_sortation";
        }
    }

    public class SendAddress
    {
        /// <summary>
        /// 寄件省	
        /// </summary>
        public string provinceName { get; set; }
        /// <summary>
        /// 寄件市	
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        /// 寄件区	
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// 寄件镇	
        /// </summary>
        public string townName { get; set; }
        /// <summary>
        /// 寄件详细地址	
        /// </summary>
        public string detailAddress { get; set; }
    }

    public class ConsigneeAddress
    {
        /// <summary>
        /// 收件省	
        /// </summary>
        public string provinceName { get; set; }
        /// <summary>
        /// 收件市	
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        /// 收件区	
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// 收件镇	
        /// </summary>
        public string townName { get; set; }
        /// <summary>
        /// 收件详细地址		
        /// </summary>
        public string detailAddress { get; set; }
    }
}
