/*
 *名称：RestAuth
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-17 15:29:23
 *修改时间：
 *备注：
 */

using System;

namespace Web.Utility
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;

    public class MyAuth: AuthorizeAttribute
    { 
       /* protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var reqUrl = filterContext.RequestContext.HttpContext.Request.Url;
                
                filterContext.Result = new JsonResult
                                           {
                                               Data =
                                                   new
                                                       {
                                                           returnMessage = "Ajax_UnAuth",
                                                           url =
                                                   string.Format(
                                                       "{0}?returnUrl={1}",
                                                       reqUrl.Scheme + "://" + reqUrl.Authority + FormsAuthentication.LoginUrl, filterContext.RequestContext.HttpContext.Request.UrlReferrer
                                                       )
                                                       },
                                               JsonRequestBehavior = JsonRequestBehavior.AllowGet
                                           };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            } 
        }*/

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest()
                && !filterContext.HttpContext.User.Identity.IsAuthenticated
                && (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any()
                    || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(
                        typeof(AuthorizeAttribute),
                        true).Any()))
            {
                filterContext.HttpContext.SkipAuthorization = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                filterContext.Result = new HttpUnauthorizedResult("Unauthorized");
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                filterContext.HttpContext.Response.End();
            }
            else base.OnAuthorization(filterContext);
        }
 
    }
}