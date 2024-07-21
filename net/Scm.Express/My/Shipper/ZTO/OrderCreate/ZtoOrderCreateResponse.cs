using Com.Scm.Express.OrderCreate;

namespace Com.Scm.Express.My.Shipper.ZTO.OrderCreate
{
    public class ZtoOrderCreateResponse : ZtoResponse<ZtoOrderOutput>
    {
        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = this.result.orderCode;
            result.mail_no = this.result.billCode;
            if (this.result.bigMarkInfo != null)
            {
                result.opt_code = this.result.bigMarkInfo.mark;
            }
            response.Append(result);
        }
    }

    public class ZtoOrderOutput
    {
        /// <summary>
        /// 大头笔信息	
        /// </summary>
        public ZtoBigMarkInfoOutput bigMarkInfo { get; set; }
        /// <summary>
        /// 2小时取件码（当vasType增值信息中有twoHour的时候返回）	
        public string verifyCode { get; set; }
        /// <summary>
        /// 单号所属网点code（预约件）	
        public string siteCode { get; set; }
        /// <summary>
        /// 单号所属网点名称（预约件）	
        public string SiteName { get; set; }
        /// <summary>
        /// 签单返回信息	
        public ZtoSignBillInfo signBillInfo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 运单号		
        /// <summary>
        public string billCode { get; set; }
        /// <summary>
        /// 合作商订单号		
        /// </summary>
        public string partnerOrderCode { get; set; }
    }

    public class ZtoBigMarkInfoOutput
    {
        /// <summary>
        /// 集包地	
        /// </summary>
        public string bagAddr { get; set; }
        /// <summary>
        /// 大头笔	
        public string mark { get; set; }
    }

    public class ZtoSignBillInfo
    {
        /// <summary>
        /// 签单返回运单号	
        public string billCode { get; set; }
        /// <summary>
        /// 签单返回订单号	
        public string orderCode { get; set; }
    }
}
