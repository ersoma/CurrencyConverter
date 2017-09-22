using System.Web;
using System.Web.Optimization;

namespace CurrencyConverter
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/requirements").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/foundation/foundation.js",
                        "~/Scripts/d3.v3.js",
                        "~/Scripts/rickshaw.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/custom/convert.js",
                        "~/Scripts/custom/history.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/foundation/normalize.css",
                      "~/Content/foundation/foundation.css",
                      "~/Content/rickshaw.css",
                      "~/Content/site.css"));
        }
    }
}
