using Com.Scm.Res.Tag;

namespace Com.Scm
{
    public interface ITagService
    {
        TagDao GetById(long id);

        Task<TagDao> GetByIdAsync(long id);

        TagDao GetByName(long appId, string name);

        Task<TagDao> GetByNameAsync(long appId, string name);
    }
}
