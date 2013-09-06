using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    using System.Web.Security;

    using Web.Models;

    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckUser(string userName, string password)
        {
            if (userName == "jim" && password == "123")
            {
                FormsAuthentication.SetAuthCookie(userName,true);
                return Json(new ResultModel() { Result = true, ResultInfo = "登录成功" });
            }
            return Json(new ResultModel() { Result = false, ResultInfo = "登录失败" });
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
