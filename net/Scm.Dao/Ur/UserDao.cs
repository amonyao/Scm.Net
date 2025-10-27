using Com.Scm.Dao;
using Com.Scm.Enums;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Ur
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("scm_ur_user")]
    public class UserDao : ScmDataDao, IDeleteDao
    {
        /// <summary>
        /// 
        /// </summary>
        public ScmUserTypesEnum types { get; set; }
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }
        /// <summary>
        /// 用户编码（对应客户系统编码）
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统姓名，如真实姓名
        /// </summary>
        [Required]
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 展示姓名，如用户昵称
        /// </summary>
        [Required]
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        [StringLength(32)]
        public string user { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        [StringLength(256)]
        public string pass { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        public string avatar { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(32)]
        public string cellphone { get; set; }
        /// <summary>
        /// 固话
        /// </summary>
        [StringLength(32)]
        public string telephone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(128)]
        public string email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ScmSexEnum sex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public string remark { get; set; }

        #region 登录记录
        /// <summary>
        /// 登录次数
        /// </summary>
        [Required]
        public int login_count { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public long login_time { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public long last_time { get; set; }
        #endregion

        #region 安全检测
        /// <summary>
        /// 错误次数
        /// </summary>
        public int error_count { get; set; }

        /// <summary>
        /// 下次登录时间
        /// </summary>
        public long next_time { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ScmDeleteEnum row_delete { get; set; }

        #region 数据权限
        /// <summary>
        /// 数据权限
        /// </summary>
        public ScmUserDataEnum data { get; set; }
        /// <summary>
        /// 首页展示
        /// </summary>
        public ScmUserHomeTypesEnum home { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public override void PrepareCreate(long userId)
        {
            base.PrepareCreate(userId);

            codes = UidUtils.NextCodes("scm_ur_user", (int)types);
            row_delete = ScmDeleteEnum.No;
            if (string.IsNullOrEmpty(names))
            {
                names = namec;
            }
            login_time = 0;
            last_time = 0;
            next_time = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public override void PrepareUpdate(long userId)
        {
            base.PrepareUpdate(userId);
            if (string.IsNullOrEmpty(names))
            {
                names = namec;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void EncodePass()
        {
            pass = TextUtils.EncodePass(pass);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DecodePass()
        {
            pass = TextUtils.DecodePass(pass);
        }

        public void UseDefaultPass()
        {
            this.pass = "";
        }

        public void UseDefaultAvatar()
        {
            this.avatar = "0.png";
        }
    }
}