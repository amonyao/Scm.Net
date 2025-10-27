using Com.Scm.Dvo;
using Com.Scm.Enums;

namespace Com.Scm.Sys.Tasks.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDvo : ScmDataDvo
    {
        /// <summary>
        /// ��������
        /// </summary>
        public TaskTypesEnum types { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string codec { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string names { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string clazz { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string json { get; set; }
        /// <summary>
        /// �ļ�
        /// </summary>
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
    }
}