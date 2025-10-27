using Com.Scm.Api;
using Com.Scm.Cfg.UserTheme;

namespace Com.Scm.Operator.Dvo;

/// <summary>
/// 
/// </summary>
public class LoginResponse : ScmApiResponse
{
    /// <summary>
    /// 
    /// </summary>
    public string accessToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public OperatorInfo userInfo { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public UserThemeDto userTheme { get; set; }

    /// <summary>
    /// ��Ч�ĵ�¼�û���Ϣ
    /// </summary>
    public const int ERROR_01 = 1;
    /// <summary>
    /// ��Ч�ĵ�¼������Ϣ
    /// </summary>
    public const int ERROR_02 = 1;
    /// <summary>
    /// ��¼�˻�������
    /// </summary>
    public const int ERROR_03 = 1;
    /// <summary>
    /// �˺ű����ᣬ����ϵ����Ա��
    /// </summary>
    public const int ERROR_04 = 2;
    /// <summary>
    /// ���Ƶ�¼ʱ��
    /// </summary>
    public const int ERROR_05 = 3;
    /// <summary>
    /// ��֧�ֵĵ�¼��ʽ
    /// </summary>
    public const int ERROR_06 = 6;

    #region �����¼
    /// <summary>
    /// ��֤�����
    /// </summary>
    public const int ERROR_11 = 11;
    /// <summary>
    /// ��Ч�ĵ�¼�û�
    /// </summary>
    public const int ERROR_12 = 12;
    /// <summary>
    /// ��Ч�ĵ�¼����
    /// </summary>
    public const int ERROR_13 = 13;
    /// <summary>
    /// �˺������������
    /// </summary>
    public const int ERROR_14 = 14;
    #endregion

    #region �ֻ���¼
    /// <summary>
    /// ��Ч���ֻ�����
    /// </summary>
    public const int ERROR_21 = 21;
    /// <summary>
    /// ��Ч����֤��
    /// </summary>
    public const int ERROR_22 = 22;
    /// <summary>
    /// 
    /// </summary>
    public const int ERROR_23 = 23;
    #endregion

    #region �ʼ���¼
    /// <summary>
    /// ��Ч�ĵ����ʼ�
    /// </summary>
    public const int ERROR_31 = 31;
    /// <summary>
    /// ��Ч����֤��
    /// </summary>
    public const int ERROR_32 = 32;
    #endregion

    #region ���ϵ�¼
    /// <summary>
    /// OIDC��������쳣�����Ժ����ԣ�
    /// </summary>
    public const int ERROR_41 = 41;
    /// <summary>
    /// ��Ч�����ϵ�¼��Ϣ��
    /// </summary>
    public const int ERROR_42 = 42;
    /// <summary>
    /// ���ϵ�¼��Ȩ�ѹ��ڣ���������Ȩ��
    /// </summary>
    public const int ERROR_43 = 43;
    /// <summary>
    /// ���ϵ�¼�쳣
    /// </summary>
    public const int ERROR_44 = 44;
    /// <summary>
    /// ���ϵ�¼�����ڹ����˻���
    /// </summary>
    public const int ERROR_45 = 45;
    /// <summary>
    /// ���ϵ�¼���ڶ�������˻���
    /// </summary>
    public const int ERROR_46 = 46;
    #endregion
}