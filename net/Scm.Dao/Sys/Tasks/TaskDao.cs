using Com.Scm.Dao;
using Com.Scm.Enums;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Sys.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_sys_task")]
    public class TaskDao : ScmDataDao, IDeleteDao
    {
        /// <summary>
        /// ��������
        /// </summary>
        public TaskTypesEnum types { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(128)]
        public string names { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(256)]
        public string clazz { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(1024)]
        public string json { get; set; }
        /// <summary>
        /// �ļ�
        /// </summary>
        [StringLength(256)]
        public string file { get; set; }

        /// <summary>
        /// ��ҵ״̬
        /// </summary>
        public ScmHandleEnum handle { get; set; }
        /// <summary>
        /// ��ҵ���
        /// </summary>
        public ScmResultEnum result { get; set; }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        [StringLength(256)]
        public string message { get; set; }

        /// <summary>
        /// Ԥ��ִ��ʱ�䣨��ʼ��
        /// </summary>
        public long need_time_f { get; set; }
        /// <summary>
        /// Ԥ��ִ��ʱ�䣨������
        /// </summary>
        public long need_time_t { get; set; }
        /// <summary>
        /// ʵ��ִ��ʱ�䣨ʵ�ʣ�
        /// </summary>
        public long exec_time_f { get; set; }
        /// <summary>
        /// ʵ��ִ��ʱ�䣨������
        /// </summary>
        public long exec_time_t { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public ScmDeleteEnum row_delete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public override void PrepareCreate(long userId)
        {
            base.PrepareCreate(userId);

            codes = UidUtils.NextCodes("scm_sys_task");
        }
    }
}