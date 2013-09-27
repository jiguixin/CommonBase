using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using Infrastructure.Crosscutting.Security.Common;
using Web.Models;

namespace Web.Controllers
{
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Services;

    public class MenuSettingController : BaseController
    {
        //
        // GET: /MenuSetting/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }
        
    }
}

