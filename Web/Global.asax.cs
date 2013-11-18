using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Infrastructure.Crosscutting.Security.Ioc;

namespace Web
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    using Web.Utility;

    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Logger.InitLogger();

            //Logger.Log.Debug("测试正在启动：Debug");
            //Logger.Log.Error("测试正在启动：Error");
            //Logger.Log.Fatal("测试正在启动：Fatal");
            //Logger.Log.Info("测试正在启动：Info");
            //Logger.Log.Warn("测试正在启动：Warn");
             
            InstanceLocator.SetLocator(
         new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
             
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            MyFormsPrincipal<SysUser>.TrySetUserInfo(app.Context);
        }
    }
}