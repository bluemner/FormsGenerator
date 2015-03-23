using System.Web;
using System.Web.Optimization;
using System.Collections.Generic;

namespace FormsGeneratorWebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        public static void RegisterScriptBundles(BundleCollection bundles) {
            var venderBundle = new ScriptBundle("~/Scripts/vender/vender-bundle") { Orderer = new AsIsBundleOrderer() { } };
            venderBundle.Include(
                "~/Scripts/vender/jquery.unobtrusive*",
                "~/Scripts/vender/jquery.validate*",
                "~/Scripts/vender/jquery-ui-{version}.custom.js",
                "~/Scripts/vender/jquery-ui-timepicker-addon.js");
            bundles.Add(venderBundle);

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //    "~/Scripts/vender/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/vender/jquery.validate*"));

      
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/vender/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/vender/bootstrap.js",
                "~/Scripts/vender/respond.js"));  
        }

        public static void RegisterStyleBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/Content/css").Include(
             "~/Content/bootstrap*",
             "~/Content/Site.css",
             "~/Styles/vender/jquery-ui.css",
             "~/Styles/vender/jquery-ui-timepicker-addon.css"));

            bundles.Add(new StyleBundle("~/Styles/vender/main-bundle").Include("~/Styles/vender/jquery-ui.css"));
        }
    }

    public class AsIsBundleOrderer : IBundleOrderer{
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}

