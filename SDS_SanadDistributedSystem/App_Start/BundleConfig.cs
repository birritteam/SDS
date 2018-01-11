using System.Web;
using System.Web.Optimization;

namespace SDS_SanadDistributedSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/ajaxFunctions.js", 
                        "~/Scripts/ajaxFunctionsAmr.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.rtl.min.js",
                      "~/Scripts/mdb.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/Layout.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/DataTables/jquery.dataTables.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.rtl.min.css",
                      "~/Content/mdb.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/site.css",
                      "~/Content/DataTables/css/jquery.dataTables.css"));
        }
    }
}
