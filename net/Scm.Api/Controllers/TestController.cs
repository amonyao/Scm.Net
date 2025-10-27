﻿using Com.Scm.Controllers;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Api.Controllers
{
    [ApiExplorerSettings(GroupName = "Scm")]
    public class TestController : ApiController
    {
        private readonly ISmsService _SmsService;

        public TestController(ISmsService smsService)
        {
            _SmsService = smsService;
        }


        [HttpGet, AllowAnonymous]
        public object Demo()
        {
            var id = UidUtils.NextId();
            var code = UidUtils.NextCodes("samples_demo");
            return new { id, code };
        }
    }
}
