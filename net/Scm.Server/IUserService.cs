using Com.Scm.Ur;

namespace Com.Scm
{
    public interface IUserService
    {
        UserDao GetUser(long id);

        string GetUserNames(long id);

        void Remove(long id);

        void Clear();
    }
}
