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
                "~/Content/dist/css/adminlte.min.css",
                "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css"));

            bundles.Add(new StyleBundle("~/Style/SiginStyles").Include(
                "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                "~/Content/dist/css/adminlte.min.css"));

            bundles.Add(new ScriptBundle("~/Script/MainScripts").Include(
                "~/Scripts/jquery/jquery-{version}.js",
                "~/Scripts/bootstrap/bootstrap.bundle.js",
                "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                "~/Content/dist/js/adminlte.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Script/LayoutComponents").Include(
                "~/Scripts/build/components/Aside.js",
                "~/Scripts/build/components/SideBarManu.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Requirejs").Include(
                "~/node_modules/requirejs/require.js"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
