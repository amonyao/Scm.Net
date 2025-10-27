using Com.Scm.Dsa;
using Com.Scm.Ur;

namespace Com.Scm.Service
{
    public class ScmUserService : IUserService
    {
        private readonly SugarRepository<UserDao> _thisRepository;
        private static Dictionary<long, UserDao> _UserDict = new Dictionary<long, UserDao>();

        public ScmUserService(SugarRepository<UserDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        public UserDao GetUser(long id)
        {
            if (_UserDict.ContainsKey(id))
            {
                return _UserDict[id];
            }

            var dao = _thisRepository.GetById(id);
            _UserDict[id] = dao;
            return dao;
        }

        public string GetUserNames(long id)
        {
            return GetUser(id)?.names;
        }

        public void Remove(long id)
        {
            if (_UserDict.ContainsKey(id))
            {
                _UserDict.Remove(id);
            }
        }

        public void Clear()
        {
            _UserDict.Clear();
        }
    }
}
