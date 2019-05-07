using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public enum ResponseCode
    {
        操作失败 = 00000,
        成功 = 10200,
    }
}