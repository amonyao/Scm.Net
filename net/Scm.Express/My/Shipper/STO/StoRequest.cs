using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.STO
{
    public class StoRequest
    {
        public virtual string GetApiName()
        {
            return "";
        }

        public virtual string GetToAppKey()
        {
            return "";
        }

        public virtual string GetToCode()
        {
            return "";
        }

        public string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://cloudinter-linkgateway.sto.cn/gateway/link.do";
            }
            return "http://cloudinter-linkgatewaytest.sto.cn/gateway/link.do";
        }
    }
}
