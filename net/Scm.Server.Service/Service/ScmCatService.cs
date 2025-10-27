using Com.Scm.Dsa;
using Com.Scm.Res.Cat;

namespace Com.Scm.Service
{
    public class ScmCatService : ApiService, ICatService
    {
        private readonly SugarRepository<CatDao> _thisRepository;

        public ScmCatService(SugarRepository<CatDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

    }
}
