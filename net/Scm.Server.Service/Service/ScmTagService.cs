using Com.Scm.Dsa;
using Com.Scm.Res.Tag;

namespace Com.Scm.Service
{
    public class ScmTagService : ApiService, ITagService
    {
        private readonly SugarRepository<TagDao> _thisRepository;

        public ScmTagService(SugarRepository<TagDao> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        public TagDao GetById(long id)
        {
            return _thisRepository.GetFirst(a => a.id == id);
        }

        public async Task<TagDao> GetByIdAsync(long id)
        {
            return await _thisRepository.GetFirstAsync(a => a.id == id);
        }

        public TagDao GetByName(long appId, string name)
        {
            return _thisRepository.GetFirst(a => a.app == appId && a.label == name);
        }

        public async Task<TagDao> GetByNameAsync(long appId, string name)
        {
            return await _thisRepository.GetFirstAsync(a => a.app == appId && a.label == name);
        }
    }
}
