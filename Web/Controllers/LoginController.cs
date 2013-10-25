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
        private ISysUserService userService = ServiceFactory.UserService;

        public LoginController()
        { 
        }
        //
        // GET: /Login/

        public ActionResult Index()
        { 
            ViewBag.AJAX_Login = Request.QueryString["AJAX_Login"];
             
            //todo:返回请求的URL还没有实现
            ViewBag.returnUrl = Request.QueryString["returnUrl"];
            return View();
        }

        [HttpPost]
        //检查用户名和密码匹配
        public JsonResult CheckUser(string userName, string password)
        {
            SysUser user;
            if ((user = userService.CheckUser(userName, password)) != null)
            {
                //FormsAuthentication.SetAuthCookie(userName, true);
                MyFormsPrincipal<SysUser>.SignIn(user.UserName, user, 15);
                return Json(new ResultModel() { Result = true, ResultInfo = "登录成功" });
            }

            return Json(new ResultModel() { Result = false, ResultInfo = "用户名或密码错误请重新输入" });
        }


        

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
