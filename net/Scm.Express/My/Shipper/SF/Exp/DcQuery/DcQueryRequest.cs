using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Exp.DcQuery
{
    public class DcQueryRequest : SfExpRequest
    {
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string x { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string y { get; set; }
        /// <summary>
        /// dq0：显示所有接口返回原始信息；dq1：显示格式化后信息（默认）
        /// </summary>
        public string opt { get; set; }
        /// <summary>
        /// 网点类型： 1自营服务点,2合作商家店,3嘿客店/顺丰优选 4顺丰站 5丰巢柜（允许多选，多个值之间用竖线隔开，如：1|2|5）
        /// </summary>
        public string deptType { get; set; }
        /// <summary>
        /// 服务类型：1、自寄服务2、自取服务 3、寄、取件服务4、个人地址服务 5、便民服务6自寄自取优惠服务（允许多选，多个值之间用竖线隔开，如：1|2|5）
        /// </summary>
        public string servType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 查询范围，默认为1000米，单位：米
        /// </summary>
        public int distance { get; set; }
        /// <summary>
        /// 城市：调用香港，需输入city=香港
        /// </summary>
        public string city { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://bspgw.sf-express.com/std/service";
            }

            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_QUERY_GIS_DEPARTMENT";
        }
    }
}
