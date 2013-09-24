/*
 *名称：BaseController
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-06 14:42:01
 *修改时间：
 *备注：
 */

using System;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Crosscutting.Security.Model;

    using Web.Utility;

    [Authorize]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 验证用户是否有访问该地址的权限

             string requestUrl = filterContext.HttpContext.Request.Path;

            //todo:验证用户可以看到那些操作
            //EndRequest();



            #endregion
        }

        public void EndRequest()
        {
            Response.Redirect("/Error.html");
        }

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