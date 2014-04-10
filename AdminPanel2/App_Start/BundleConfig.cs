using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AdminPanel2.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/js/libs/jquery-2.0.2.min.js",
                        "~/js/libs/jquery-ui-1.10.3.min.js",
                        "~/js/bootstrap/bootstrap.min.js",
                        "~/js/notification/SmartNotification.min.js",
                        "~/js/smartwidgets/jarvis.widget.min.js",
                        "~/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                        "~/js/plugin/sparkline/jquery.sparkline.min.js",
                        "~/js/plugin/jquery-validate/jquery.validate.min.js",
                        "~/js/plugin/masked-input/jquery.maskedinput.min.js",
                        "~/js/plugin/select2/select2.min.js",
                        "~/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                        "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
                        "~/js/plugin/datatables/jquery.dataTables-cust.min.js",
                        "~/js/plugin/datatables/ColReorder.min.js",
                        "~/js/plugin/datatables/ColVis.min.js",
                        "~/js/plugin/datatables/DT_bootstrap.js",
                        "~/js/plugin/datatables/FixedColumns.min.js",
                        //"~/js/plugin/datatables/Scrollx.js",
                        "~/js/plugin/datatables/ZeroClipboard.js",
                        "~/js/plugin/datatables/jquery.dataTablesPlugins.js",
                        "~/js/introjs/intro.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/js/app.js"
                        ));
        }


    }
}