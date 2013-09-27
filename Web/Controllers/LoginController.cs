using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Services;

namespace Web.Controllers
{
    using System.Web.Security;

    using Infrastructure.Crosscutting.Security.Model;

    using Web.Models;
    using Web.Utility;

    public class LoginController : Controller
    {
        private ISysUserService userService ;

        public LoginController()
        {
            userService = new SysUserService();
        }
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }



        

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
