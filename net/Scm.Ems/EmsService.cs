using Com.Scm.Express.Dto;
using Com.Scm.Express.My;
using Com.Scm.Express.RouteSearch;
using Com.Scm.Service;
using Com.Scm.Ur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Ems
{
    [ApiExplorerSettings(GroupName = "Ems")]
    public class EmsService : ApiService
    {
        private readonly SugarRepository<UserDao> _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public EmsService(SugarRepository<UserDao> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public RouteSearchResponse GetListAsync(string shipper, List<string> codes, string phone)
        {
            var response = new RouteSearchResponse();

            var request = new RouteSearchRequest();
            foreach (var code in codes)
            {
                var header = new OrderHeader();
                header.mail_no = code;
                header.consigner = new Contact();
                header.consigner.cellphone = phone;

                request.Append(header);
            }

            var express = MyExpress.GetInstance(shipper);
            response = express.RouteSearch(request);

            return response;
        }
    }
}
