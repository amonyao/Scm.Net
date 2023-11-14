using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Qcs;
using Com.Scm.Utils;
using Microsoft.AspNetCore.SignalR;

namespace Scm.Api.Hubs
{
    /// <summary>
    /// 排队叫号系统
    /// </summary>
    public class QcsHub : Hub
    {
        private readonly SugarRepository<ScmQcsDetailDao> _detailRepository;

        public QcsHub(SugarRepository<ScmQcsDetailDao> detailRepository)
        {
            _detailRepository = detailRepository;
        }

        [HubMethodName("SendMessage")]
        public async Task SendMessage(string id)
        {
            if (!ScmUtils.IsValidId(id))
            {
                return;
            }

            var detailId = long.Parse(id);
            //var detailDao = await _detailRepository.GetByIdAsync(detailId);
            var detailDao = await _detailRepository.AsQueryable()
                .ClearFilter()
                .Where(a => a.id == detailId)
                .FirstAsync();
            if (detailDao != null)
            {
                await Clients.All.SendAsync("ReceiveMessage", id, detailDao.qty, detailDao.idx);
            }
        }
    }
}
