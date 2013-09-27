using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Crosscutting.Security.Model;

    using Web.Utility;

    public class AppController : Controller
    {
        public SysUser UserData
        {
            get
            {
                var us = HttpContext.User as MyFormsPrincipal<SysUser>;

                return us != null ? us.UserData : null;
            }
        }
    }
}
