using System.Web;
using System.Web.Optimization;

namespace webTemplate
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Style/MainStyles").Include(
                "~/Content/Home.css",
                "~/node_modules/animate.css/animate.css",
                "~/node_modules/bootstrap-vue/dist/bootstrap-vue.css",
                "~/Content/dist/css/adminlte.css",
                "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.css",
                "~/Content/plugins/daterangepicker/daterangepicker.css",
                "~/Content/plugins/bootstrap-switch/css/bootstrap3/bootstrap-switch.css"));

             bundles.Add(new StyleBundle("~/Style/SiginStyles").Include(
                "~/Content/Home.css",
                "~/Content/font-family.css",
                "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.css",
                "~/Content/dist/css/adminlte.css"));

            bundles.Add(new ScriptBundle("~/Script/MainScripts").Include(
                "~/Scripts/jquery/jquery-{version}.js",
                "~/Scripts/bootstrap/bootstrap.bundle.js",
                "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.js",
                "~/Content/dist/js/adminlte.js",
                "~/Content/plugins/daterangepicker/daterangepicker.js",
                "~/Content/plugins/bootstrap-switch/js/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/Script/LayoutComponents").Include(
                "~/Scripts/build/components/Aside.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Requirejs").Include(
                "~/node_modules/requirejs/require.js"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
