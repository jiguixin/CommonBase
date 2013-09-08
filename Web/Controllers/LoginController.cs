using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Crosscutting.Security.Services;

namespace Web.Controllers
{
    using System.Web.Security;

    using Web.Models;

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

        [HttpPost]
        public JsonResult CheckUser(string userName, string password)
        {
            if (userService.CheckUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, true);
                return Json(new ResultModel() {Result = true, ResultInfo = "登录成功"});
            }
            return Json(new ResultModel() {Result = false, ResultInfo = "用户名或密码错误请重新输入"});
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
