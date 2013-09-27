using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Web.Controllers
{
    using Infrastructure.Crosscutting.Security.Services;

    public class HomeController : BaseController
    {
        private ISysUserService userService = ServiceFactory.UserService;
        private ISysMenuService menuService = ServiceFactory.MenuService;

        private RestApiController restApiController = new RestApiController();
        //
        // GET: /Home/ 
        public ActionResult Index()
        {
            return View();
        } 
    }
}
