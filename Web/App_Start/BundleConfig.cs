using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Content/easyui/jquery-easyui-min.js",
                        "~/Content/easyui/locale/easyui-lang-zh_CN.js", 
                        "~/Content/easyui/easyloader.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
              
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(
                new StyleBundle("~/Content/easyui/css").Include(
                    "~/Content/easyui/themes/default/easyui.css",
                    "~/Content/easyui/themes/icon.css"));

            /* "~/Content/easyui/themes/default/accordion.css",
                    "~/Content/easyui/themes/default/calendar.css",
                    "~/Content/easyui/themes/default/combo.css",
                    "~/Content/easyui/themes/default/combobox.css",
                    "~/Content/easyui/themes/default/datagrid.css",
                    "~/Content/easyui/themes/default/datebox.css",
                    "~/Content/easyui/themes/default/dialog.css",
                    "~/Content/easyui/themes/default/layout.css",
                    "~/Content/easyui/themes/default/linkbutton.css",
                    "~/Content/easyui/themes/default/menu.css",
                    "~/Content/easyui/themes/default/menubutton.css",
                    "~/Content/easyui/themes/default/messager.css",
                    "~/Content/easyui/themes/default/pagination.css",
                    "~/Content/easyui/themes/default/panel.css",
                    "~/Content/easyui/themes/default/progressbar.css",
                    "~/Content/easyui/themes/default/propertygrid.css",
                    "~/Content/easyui/themes/default/searchbox.css",
                    "~/Content/easyui/themes/default/slider.css",
                    "~/Content/easyui/themes/default/spinner.css",
                    "~/Content/easyui/themes/default/splitbutton.css",
                    "~/Content/easyui/themes/default/tabs.css",
                    "~/Content/easyui/themes/default/tooltip.css",
                    "~/Content/easyui/themes/default/tree.css",
                    "~/Content/easyui/themes/default/validatebox.css",
                    "~/Content/easyui/themes/default/window.css"*/

             bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}