using Com.Scm.Api.Service;
using Com.Scm.Dsa.Cache;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Com.Scm.Api.Hubs
{
    [EnableCors("ScmCors")]
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ICacheService _cacheService;

        public ChatHub(IHttpContextAccessor accessor, ICacheService cacheService)
        {
            _accessor = accessor;
            _cacheService = cacheService;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="user"></param>
        [HubMethodName("SendKickOut")]
        public async Task SendKickOut(string user)
        {
            var list = _cacheService.GetCache<List<ClientUser>>(KeyUtils.ONLINEUSERS);
            if (list != null)
            {
                var userId = long.Parse(user);
                var now = list.FirstOrDefault(m => m.Id == userId);
                if (now != null)
                {
                    Context.Items.Remove(now.ConnectionId);
                    list.Remove(now);
                }
                _cacheService.SetCache(KeyUtils.ONLINEUSERS, list);
            }
            await Clients.All.SendAsync("ReceiveMessage", "out", user);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            if (_accessor.HttpContext != null)
            {
                var token = _accessor.HttpContext.Request.Query["access_token"];
                var jwtToken = JwtAuthService.SerializeJwt(token);
                var userList = _cacheService.GetCache<List<ClientUser>>(KeyUtils.ONLINEUSERS);
                if (userList == null)
                {
                    userList = new List<ClientUser>();
                    userList.Add(new ClientUser()
                    {
                        Id = jwtToken.id,
                        ConnectionId = Context.ConnectionId,
                        Name = jwtToken.user_name
                    });
                    _cacheService.SetCache(KeyUtils.ONLINEUSERS, userList);
                }
                else
                {
                    var now = userList.FirstOrDefault(m => m.Id == jwtToken.id);
                    if (now != null)
                    {
                        Context.Items.Remove(now.ConnectionId);
                        userList.Remove(now);
                    }
                    userList.Add(new ClientUser()
                    {
                        Id = jwtToken.id,
                        ConnectionId = Context.ConnectionId,
                        Name = jwtToken.user_name,
                        Time = DateTime.Now
                    });
                    _cacheService.SetCache(KeyUtils.ONLINEUSERS, userList);
                }
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connid = Context.ConnectionId;
            var userList = _cacheService.GetCache<List<ClientUser>>(KeyUtils.ONLINEUSERS);
            if (userList == null)
            {
                return base.OnDisconnectedAsync(exception);
            }

            var now = userList.FirstOrDefault(m => m.ConnectionId == connid);
            if (now != null)
            {
                userList.Remove(now);
            }
            _cacheService.SetCache(KeyUtils.ONLINEUSERS, userList);
            return base.OnDisconnectedAsync(exception);
        }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class ClientUser
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 连接编号
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 应用编号
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 登录人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}