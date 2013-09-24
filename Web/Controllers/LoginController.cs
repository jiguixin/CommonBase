using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [HttpPost]
        public JsonResult CheckUser(string userName, string password)
        {
            SysUser user;
            if ((user = userService.CheckUser(userName, password)) != null)
            { 
                //FormsAuthentication.SetAuthCookie(userName, true);
                MyFormsPrincipal<SysUser>.SignIn(user.UserName, user, 100);
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
