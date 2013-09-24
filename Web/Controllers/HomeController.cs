using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    using Infrastructure.Crosscutting.Security.Services;

    public class HomeController : BaseController
    {

        private ISysMenuService menuService = ServiceFactory.MenuService;

        //
        // GET: /Home/ 
        public ActionResult Index()
        { 
            return View();
        }
         
        public JsonResult GetMenusByUser()
        { 
            var us = UserData.SysId;
             
            var lstMenus = menuService.MenuRepository.GetList().ToArray() ;

            //todo 实现该功能
            lstMenus[0].SysId = us;

            return Json(lstMenus, JsonRequestBehavior.AllowGet);
        }
    }
}
