using System.Web;
using System.Web.Optimization;

namespace ecrm
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var jqueryCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js";         
            var modernizrCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";
            var bootstrapjsCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/js/bootstrap.min.js";       
            var datetimepickerjsCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.4/build/jquery.datetimepicker.full.min.js";                                 
            var bootstrapcssCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/css/bootstrap.min.css";
            var respondjsCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js";        
            var datetimepickercssCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.4/build/jquery.datetimepicker.min.css";

            
            bundles.Add(new ScriptBundle("~/bundles/jquery", jqueryCdnPath).Include());

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(                      
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/ecrmscripts").Include(
                      "~/Scripts/ecrm.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs", bootstrapjsCdnPath).Include());
            bundles.Add(new ScriptBundle("~/bundles/respondjs", respondjsCdnPath).Include());  
            bundles.Add(new ScriptBundle("~/bundles/datetimepickerjs", datetimepickerjsCdnPath).Include());
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr", modernizrCdnPath).Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(                      
                      "~/Content/ECRM-V4.min.css"
                      ));          
            bundles.Add(new ScriptBundle("~/bundles/bootstrapcss", bootstrapcssCdnPath).Include());
            bundles.Add(new ScriptBundle("~/bundles/datetimepickercss", datetimepickercssCdnPath).Include());

            BundleTable.EnableOptimizations = true;
        }
    }
}
