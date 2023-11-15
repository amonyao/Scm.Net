using Com.Scm.Dao.Unit;
using Com.Scm.Yms;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Qcs
{
    /// <summary>
    /// 队列
    /// </summary>
    [SugarTable("scm_qcs_detail")]
    public class ScmQcsDetailDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 队列编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 队列说明
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 当前号码
        /// </summary>
        public int idx { get; set; }

        /// <summary>
        /// 号码前缀
        /// </summary>
        [StringLength(4)]
        public string prefix { get; set; }

        /// <summary>
        /// 数字长度
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// 号码周期
        /// </summary>
        public QcsDetailCycleEnums cycle { get; set; }

        /// <summary>
        /// 上次标记
        /// </summary>
        public int last { get; set; } = -1;

        /// <summary>
        /// 取号
        /// </summary>
        /// <param name="step"></param>
        public void Add(int step)
        {
            this.qty += step;
        }

        /// <summary>
        /// 叫号
        /// </summary>
        public void Next()
        {
            this.idx += 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenCodec()
        {
            var now = DateTime.Now;
            var value = 0;
            switch (cycle)
            {
                case QcsDetailCycleEnums.Hour:
                    value = now.Hour;
                    break;
                case QcsDetailCycleEnums.Day:
                    value = now.Day;
                    break;
                case QcsDetailCycleEnums.Week:
                    value = (int)now.DayOfWeek;
                    break;
                case QcsDetailCycleEnums.Month:
                    value = now.Month;
                    break;
                case QcsDetailCycleEnums.Season:
                    value = now.Month / 3;
                    break;
                case QcsDetailCycleEnums.Year:
                    value = now.Year;
                    break;
            }
            if (last != value)
            {
                last = value;
                qty = 0;
            }

            var codec = prefix;
            codec += length > 0 ? qty.ToString("D" + length) : qty;
            return codec;
        }
    }
}