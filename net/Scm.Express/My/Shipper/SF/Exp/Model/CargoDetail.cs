namespace Com.Scm.Express.My.Shipper.SF.Exp.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CargoDetail
    {
        /// <summary>
        /// 货物名称，如果需要生成电子 运单，则为必填
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 货物数量 跨境件报关需要填写
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 货物单位，如：个、台、本， 跨境件报关需要填写
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 订单货物单位重量，包含子母件， 单位千克，精确到小数点后3位 跨境件报关需要填写
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 货物单价，精确到小数点后3位， 跨境件报关需要填写
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 货物单价的币别： 参照附录《国际件币别表》
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 原产地国别， 跨境件报关需要填写
        /// </summary>
        public string sourceArea { get; set; }
        /// <summary>
        /// 货物产品国检备案编号
        /// </summary>
        public string productRecordNo { get; set; }
        /// <summary>
        /// 商品海关备案号
        /// </summary>
        public string goodPrepardNo { get; set; }
        /// <summary>
        /// 商品行邮税号
        /// </summary>
        public string taxNo { get; set; }
        /// <summary>
        /// 海关编码
        /// </summary>
        public string hsCode { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string goodsCode { get; set; }
        /// <summary>
        /// 货物品牌
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 货物规格型号
        /// </summary>
        public string specifications { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string manufacturer { get; set; }
        /// <summary>
        /// 托寄物毛重
        /// </summary>
        public string shipmentWeight { get; set; }
        /// <summary>
        /// 托寄物长
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 托寄物宽
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 托寄物高
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 托寄物体积
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 托寄物声明价值（杭州口岸必填）
        /// </summary>
        public string cargoDeclaredValue { get; set; }
        /// <summary>
        /// 托寄物声明价值币别（杭州口岸必填）
        /// </summary>
        public string declaredValueDeclaredCurrency { get; set; }
        /// <summary>
        /// 货物id（逆向物流）
        /// </summary>
        public string cargoId { get; set; }
        /// <summary>
        /// 智能验货标识(1-是,0-否) （逆向物流）
        /// </summary>
        public string intelligentInspection { get; set; }
        /// <summary>
        /// 货物标识码（逆向物流）
        /// </summary>
        public string snCode { get; set; }
        /// <summary>
        /// 国条码
        /// </summary>
        public string stateBarCode { get; set; }
    }
}
