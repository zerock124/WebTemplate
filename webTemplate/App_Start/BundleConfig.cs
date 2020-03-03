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
                "~/node_modules/animate.css/animate.min.css",
                "~/node_modules/bootstrap-vue/dist/bootstrap-vue.min.css",
                "~/Content/dist/css/adminlte.css",
                "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                "~/Content/plugins/daterangepicker/daterangepicker.css",
                "~/Content/plugins/bootstrap-switch/css/bootstrap3/bootstrap-switch.min.css"));

            bundles.Add(new ScriptBundle("~/Script/MainScripts").Include(
                "~/Scripts/jquery/jquery-{version}.js",
                "~/Scripts/bootstrap/bootstrap.bundle.min.js",
                "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                "~/Content/dist/js/adminlte.min.js",
                "~/Content/plugins/daterangepicker/daterangepicker.js",
                "~/Content/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

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
