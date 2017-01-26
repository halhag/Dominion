using System.Web;
using System.Web.Optimization;

namespace Dominion
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.dynatable.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/assets/css/layoutnewstyle").Include(
                    "~/assets/css/assets.css",
                    "~/assets/css/style.css"
                    ));

            bundles.Add(new StyleBundle("~/assets/css/layoutnewrevolutionstyle").Include(
                "~/assets/revolution/fonts/pe-icon-7-stroke/css/pe-icon-7-stroke.css",
                "~/assets/revolution/css/settings.css",
                "~/assets/revolution/css/layers.css",
                "~/assets/revolution/css/navigation.css",
                "~/assets/css/skins/default.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/layoutnewscripts").Include(
                    "~/assets/js/assets.min.js",
                    "~/assets/revolution/js/jquery.themepunch.revolution.min.js",
                    "~/assets/revolution/js/jquery.themepunch.tools.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/layoutnewmainscripts").Include(
                   "~/assets/js/script.js"
                   ));

            bundles.Add(new StyleBundle("~/assets/css/sitelayoutsstyle").Include(
                "~/Scripts/bower_components/chartist/dist/chartist.min.css", "~/Scripts/jquery-ui-1.11.4.custom/jquery-ui.min.css",
                "~/assets/css/assets.css", "~/assets/css/style.css", "~/assets/css/light.css"
                //,"~/assets/css/skins/default.css"
                ));
        }
    }
}
