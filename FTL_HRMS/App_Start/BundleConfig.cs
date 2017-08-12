using System.Web.Optimization;

namespace FTL_HRMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/AdminTamplate").Include(
                      "~/Scripts/jquery-3.1.1.min.js",
                      "~/Scripts/jquery-3.1.1.min.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/Others").Include(
                      "~/Scripts/d3.v3.js",
                      "~/Scripts/jquery-3.1.1.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/AdminTamplate").Include(
                      "~/Content/pace-theme-flash.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/animate.css",
                      "~/Content/perfect-scrollbar.css"));
            bundles.Add(new StyleBundle("~/Content/Others").Include(
                      "~/Content/_all.css",
                      "~/Content/morris.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-jvectormap-2.0.1.css",
                      "~/Content/white.css",
                      "~/Content/datepicker.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css"));
        }
    }
}
