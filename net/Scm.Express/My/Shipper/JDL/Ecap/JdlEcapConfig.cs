namespace Com.Scm.Express.My.Shipper.JDL.Ecap
{
    public class JdlEcapConfig : JdlConfig
    {
        /// <summary>
        /// 下单来源：0-c2c；1-b2c；2-c2b；与下单接口入参保持一致
        /// </summary>
        public int orderOrigin { get; set; }
        /// <summary>
        /// 付款方式；
        /// orderOrigin= 0时，枚举值：1-寄付；2-到付；
        /// orderOrigin= 1 时，枚举值：1-寄付；2-到付；3-月结；
        /// orderOrigin为2时，枚举值：5-多方收费，多方收费指下单费用由多个模块或主体来支付，逆向取件时运费、包装费以及保险费用可能由商家以及C消费者分别承担，
        /// 具体参考c2bAddedSettleTypeInfo枚举传入	
        /// </summary>
        public int settleType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 揽收方式：1-上门取件(默认) ；2-自送，默认1-上门取件
        /// </summary>
        public int pickupType { get; set; }

        public override void LoadDef()
        {
            orderOrigin = 0;
            settleType = 1;
            productCode = "ed-m-0001";
            pickupType = 1;
        }
    }
}
