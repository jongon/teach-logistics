using System.Web.Optimization;

namespace TeachLogistics
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            // bundles code
            var cdnPath = "//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700&amp;lang=en";
            bundles.Add(new StyleBundle("~/fonts", cdnPath));

            //KendoUI
            bundles.Add(new ScriptBundle("~/bundles/kendo/kendoscripts").Include(
                "~/Scripts/kendo/2015.2.624/kendo.all.min.js",
                "~/Scripts/kendo/2015.2.624/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo/2015.2.624/cultures/kendo.culture.es-VE.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/2015.2.624/kendostyles").Include(
                "~/Content/kendo/2015.2.624/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2015.2.624/kendo.bootstrap.min.css"));

            //AngularJs
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angulajs/angular.js"));

            //Jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            //Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/plugins/validate/jquery.validate.js",
                        "~/Scripts/plugins/validate/localization/messages_es.js",
                        "~/Scripts/plugins/jquery-unobtrusive/jquery.validate.unobtrusive.js",
                        "~/Scripts/plugins/jquery-unobtrusive-bootstrap/jquery.validate.unobtrusive.bootstrap.js"));

            //Globalization
            bundles.Add(new ScriptBundle("~/bundles/globalization").Include(
                        "~/Scripts/cldr.js",
                        "~/Scripts/globalize.js",
                        "~/Scripts/globalize/date.js",
                        "~/Scripts/plugins/jquery-validate-globalize/jquery.validate.globalize.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr/modernizr-*"));

            //RespondJs
            bundles.Add(new ScriptBundle("~/bundles/respondJs").Include(
                "~/Scripts/plugins/respondjs/respond.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapStyles").Include(
                    "~/Content/bootstrap.css"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/animate.css",
                        "~/Content/style.css",
                        "~/Content/custom.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                        "~/Content/font-awesome.css"));

            //MetisMenu
            bundles.Add(new ScriptBundle("~/plugins/metisMenu").Include(
                        "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            //Pace
            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                        "~/Scripts/plugins/pace/pace.min.js"));

            //JqueryCountdown
            bundles.Add(new ScriptBundle("~/plugins/jqueryCountdown").Include(
                        "~/Scripts/plugins/jquery.countdown/jquery.countdown.min.js"));

            // Inspinia
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                        "~/Scripts/app/inspinia.js"));

            bundles.Add(new StyleBundle("~/Content/inspiniaStyle").Include(
                        "~/Content/style.css"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/bundles/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // FancyBox gallery css styles
            bundles.Add(new StyleBundle("~/plugins/fancyboxStyles").Include(
                      "~/Scripts/plugins/fancybox/jquery.fancybox.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/fancybox").Include(
                      "~/Scripts/plugins/fancybox/jquery.fancybox.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // ChartJs Legend
            bundles.Add(new ScriptBundle("~/plugins/chartJsLegend").Include(
                       "~/Scripts/plugins/chartJsLegend/legend.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Content/plugins/dataTables/dataTables.responsive.css",
                      "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.min.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/plugins/dataTables/plugins/type-detection/date-uk.js",
                      "~/Scripts/plugins/dataTables/plugins/sorting/date-uk.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            // jeditable 
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/plugins/jqGridStyles").Include(
                      "~/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/plugins/codemirror/codemirror.css",
                      "~/Content/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Scripts/plugins/codemirror/codemirror.js",
                      "~/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Scripts/plugins/nestable/jquery.nestable.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/plugins/ionRangeStyles").Include(
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/plugins/chosenStyles").Include(
                      "~/Content/plugins/chosen/chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Scripts/plugins/steps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/plugins/dropZoneStyles").Include(
                      "~/Content/plugins/dropzone/basic.css",
                      "~/Content/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/plugins/summernote/summernote.css",
                      "~/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            //multiselect styles
            bundles.Add(new StyleBundle("~/plugins/multiselectStyles").Include(
                      "~/Content/plugins/multiselect/multi-select.css"));

            //multiselect
            bundles.Add(new ScriptBundle("~/plugins/multiselect").Include(
                     "~/Scripts/plugins/multiselect/jquery.multi-select.js"));

            //quicksearch
            bundles.Add(new ScriptBundle("~/plugins/quicksearch").Include(
                    "~/Scripts/plugins/quicksearch/jquery.quicksearch.js"));

            //mathjax
            bundles.Add(new ScriptBundle("~/plugins/mathJax/mathJax").Include(
                    "~/Scripts/plugins/mathjax/MathJax.js"));

            //toastr
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                    "~/Scripts/plugins/toastr/toastr.min.js"));

            //toastr styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                    "~/Content/plugins/toastr/toastr.min.css"));

            //isotope
            bundles.Add(new ScriptBundle("~/plugins/isotope").Include(
                    "~/Scripts/plugins/isotope/isotope.pkgd.min.js"));

            //Application Scripts

            //Account
            bundles.Add(new ScriptBundle("~/bundles/Account/index").Include(
                    "~/Scripts/app/Account/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Account/edit").Include(
                    "~/Scripts/app/Account/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/Account/login").Include(
                    "~/Scripts/app/Account/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/Account/register").Include(
                    "~/Scripts/app/Account/register.js"));

            //Semesters
            bundles.Add(new ScriptBundle("~/bundles/Semesters/index").Include(
                    "~/Scripts/app/Semesters/index.js"));

            //Sections
            bundles.Add(new ScriptBundle("~/bundles/Sections/index").Include(
                    "~/Scripts/app/Sections/index.js"));

            //Groups
            bundles.Add(new ScriptBundle("~/bundles/Groups/create").Include(
                    "~/Scripts/app/Groups/create.js"));

            bundles.Add(new ScriptBundle("~/bundles/Groups/edit").Include(
                    "~/Scripts/app/Groups/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/Groups/index").Include(
                     "~/Scripts/app/Groups/index.js"));

            //Initial Charge
            bundles.Add(new ScriptBundle("~/bundles/CaseStudies/create").Include(
                "~/Scripts/app/CaseStudies/create.js"));

            bundles.Add(new ScriptBundle("~/bundles/CaseStudies/index").Include(
                "~/Scripts/app/CaseStudies/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/CaseStudies/asign_section").Include(
                "~/Scripts/app/CaseStudies/asign_section.js"));

            bundles.Add(new StyleBundle("~/app/CaseStudies").Include(
                "~/Content/app/Casestudies.css"));

            //Products
            bundles.Add(new ScriptBundle("~/bundles/Products/index").Include(
                "~/Scripts/app/Products/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Products/create").Include(
                "~/Scripts/app/Products/create.js"));

            bundles.Add(new ScriptBundle("~/bundles/Products/edit").Include(
                "~/Scripts/app/Products/edit.js"));

            //Questions
            bundles.Add(new ScriptBundle("~/bundles/Questions/index").Include(
                "~/Scripts/app/Questions/index.js"));

            //Evaluations
            bundles.Add(new ScriptBundle("~/bundles/Evaluations/index").Include(
                "~/Scripts/app/Evaluations/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/create").Include(
                "~/Scripts/app/Evaluations/create.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/edit").Include(
                "~/Scripts/app/Evaluations/edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/assign_section").Include(
                "~/Scripts/app/Evaluations/assign_section.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/evaluations").Include(
                "~/Scripts/app/Evaluations/evaluations.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/take_quiz").Include(
                "~/Scripts/app/Evaluations/take_quiz.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/califications").Include(
                "~/Scripts/app/Evaluations/califications.js"));

            bundles.Add(new ScriptBundle("~/bundles/Evaluations/review_quiz").Include(
                "~/Scripts/app/Evaluations/review_quiz.js"));

            //Documents
            bundles.Add(new ScriptBundle("~/bundles/Documents/index").Include(
                "~/Scripts/app/Documents/index.js"));

            //Simulations
            bundles.Add(new ScriptBundle("~/bundles/Simulations/index").Include(
                "~/Scripts/app/Simulations/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Simulations/register_demands").Include(
                "~/Scripts/app/Simulations/register_demands.js"));

            bundles.Add(new ScriptBundle("~/bundles/Simulations/orders").Include(
                "~/Scripts/app/Simulations/orders.js"));

            bundles.Add(new ScriptBundle("~/bundles/Simulations/groups").Include(
                "~/Scripts/app/Simulations/groups.js"));

            bundles.Add(new StyleBundle("~/app/Simulations").Include(
                "~/Content/app/Simulations.css"));

            //Results
            bundles.Add(new ScriptBundle("~/bundles/Results/index").Include(
                "~/Scripts/app/Results/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Results/indexStudents").Include(
                "~/Scripts/app/Results/indexStudents.js"));

            bundles.Add(new ScriptBundle("~/bundles/Results/details").Include(
                "~/Scripts/app/Results/details.js"));

            //Stadistics
            bundles.Add(new ScriptBundle("~/bundles/Stadistics/index").Include(
                "~/Scripts/app/Stadistics/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Stadistics/studentStadistics").Include(
                "~/Scripts/app/Stadistics/studentStadistics.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}