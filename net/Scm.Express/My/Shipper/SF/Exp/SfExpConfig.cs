namespace Com.Scm.Express.My.Shipper.SF.Exp
{
    public class SfExpConfig : SfConfig
    {
        public string language { get; set; }

        public override void LoadDef()
        {
            language = "zh-CN";
        }
    }
}
