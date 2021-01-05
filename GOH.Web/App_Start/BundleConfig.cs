using System.Web;
using System.Web.Optimization;

namespace GOH.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui")
                    .Include("~/Content/themes/base/all.css"));

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

            bundles.Add(new StyleBundle("~/Content/summernote").Include(
                   "~/Scripts/summernote/summernote.css",
                   "~/Content/font-awesome.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
                     "~/Scripts/summernote/summernote.js",
                     "~/Scripts/sitejs/summernoteInit.js"));

            bundles.Add(new ScriptBundle("~/bundles/blog-sln").Include(
                    "~/Scripts/sitejs/blog-solution.js",
                    "~/Scripts/mustache.js"));
        }
    }
}
