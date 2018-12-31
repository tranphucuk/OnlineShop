using System;
using System.Web;
using System.Web.Optimization;

namespace OnlineShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/cssAdminCore").Include(
                "~/Assets/Admin/libs/bootstrap-3.3.7/dist/css/bootstrap.min.css",
                "~/Assets/Admin/css/ionicons.min.css",
                "~/Assets/Admin/css/AdminLTE.min.css",
                "~/Assets/Admin/css/skins/_all-skins.min.css",
                "~/Assets/Admin/libs/toastr/build/toastr.css",
                "~/Assets/Admin/css/custom.css",
                "~/Assets/Admin/libs/angular-loading-bar/build/loading-bar.css",
                "~/Assets/Admin/libs/ui-select/dist/select.min.css"
                ).Include("~/Assets/Client/font-awesome-4.7.0/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            BundleJsAdmin(bundles);

            bundles.Add(new StyleBundle("~/bundles/cssClientCore").Include(
                "~/Assets/Client/css/bootstrap.css",
                "~/Assets/Client/css/jquery.auto-complete.css",
                "~/Assets/Client/css/style.css",
                "~/Assets/Client/css/customStyle.css",
                "~/Assets/Client/css/fonts.googleapis.css",
                "~/Assets/Client/js/Ply-master/ply.css"
                ).Include("~/Assets/Client/font-awesome-4.7.0/css/font-awesome.css", new CssRewriteUrlTransform()));

            //bundles.Add(new ScriptBundle("~/bundles/jsClientCore").Include(
            //    "~/Assets/Client/js/jquery.min.js",
            //    "~/Assets/Client/js/jquery.auto-complete.min.js",
            //    "~/Assets/Admin/libs/jquery-validation-1.19.0/dist/jquery.validate.js",
            //    "~/Assets/Admin/libs/jquery-validation-1.19.0/dist/additional-methods.js",
            //    "~/Assets/Admin/libs/Numeral.js/numeral.js",
            //    "~/Assets/Admin/libs/mustache.js/mustache.js",
            //    "~/Assets/Client/js/controller/shoppingCart.js",
            //    "~/Assets/Client/js/autoComplete.js",
            //    "~/Assets/Client/js/signOut.js",
            //    "~/Assets/Client/js/controller/emailNewsLetter.js",
            //    "~/Assets/Client/js/notify.min.js",
            //    "~/Assets/Client/js/Ply-master/Ply.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jsCategoryCore").Include(
            //    "~/Assets/Client/js/jquery.min.js",
            //    "~/Assets/Client/js/controller/categoryController.js"));

            BundleTable.EnableOptimizations = true;
        }

        private static void BundleJsAdmin(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsAdminCore").Include(
               "~/Assets/Admin/libs/jquery/dist/jquery.min.js",
               "~/Assets/Admin/libs/bootstrap-3.3.7/dist/js/bootstrap.min.js",
               "~/Assets/Admin/libs/slimScroll/jquery.slimscroll.min.js",
               "~/Assets/Admin/libs/fastclick/fastclick.min.js", // Basic 
                "~/Assets/Admin/libs/angular/angular.js",
                "~/Assets/Admin/libs/angular-ui-router.js",
                "~/Assets/Admin/libs/toastr/toastr.js",
                "~/Assets/Admin/libs/bootbox/bootbox.js",
                "~/Assets/Admin/libs/ng-Bootbox/ngBootbox.js",
                "~/Assets/Admin/libs/ckfinder/ckfinder.js",
                "~/Assets/Admin/libs/angular-local-storage/dist/angular-local-storage.min.js",
                "~/Assets/Admin/libs/angular-loading-bar/build/loading-bar.min.js",
                "~/Assets/Admin/libs/angular-sanitize.js",
                "~/Assets/Admin/libs/checklist-model/checklist-model.js",
                "~/Assets/Admin/libs/ui-select/dist/select.min.js",

                "~/app/shared/modules/onlineshop.common.js",
                "~/app/app.js",

                "~/app/shared/services/notificationService.js",
                "~/app/shared/filter/appFilter.js",
                "~/app/shared/services/apiService.js",
                "~/app/shared/directives/pagerDirectives.js",
                "~/app/shared/services/unsignName.Service.js",
                "~/app/shared/services/DeleteItemService.js",
                "~/app/shared/services/authData.js",
                "~/app/shared/services/authenticationService.js",
                "~/app/shared/services/loginService.js",

                "~/app/components/product_categories/productCategory.module.js",
                "~/app/components/product_categories/productCategoryEditController.js",
                "~/app/components/product_categories/productCategoryAddController.js",
                "~/app/components/product_categories/productCategoryListController.js",

                "~/app/components/products/products.module.js",
                "~/app/components/products/productEditController.js",
                "~/app/components/products/productAddController.js",
                "~/app/components/products/productListController.js",

                "~/app/components/page/page.module.js",
                "~/app/components/page/pageEditController.js",
                "~/app/components/page/pageAddController.js",
                "~/app/components/page/pageListCotroller.js",

                "~/app/components/application_groups/applicationGroups.module.js",
                "~/app/components/application_groups/applicationGroupAddController.js",
                "~/app/components/application_groups/applicationGroupsEditController.js",
                "~/app/components/application_groups/applicationGroupListController.js",

                "~/app/components/application_user_roles/applicationRoles.module.js",
                "~/app/components/application_user_roles/applicationRole_editController.js",
                "~/app/components/application_user_roles/applicationRole_addController.js",
                "~/app/components/application_user_roles/applicationRole_listController.js",

                "~/app/components/application_user/applicationUser_module.js",
                "~/app/components/application_user/applicationUser_editController.js",
                "~/app/components/application_user/applicationUser_addController.js",
                "~/app/components/application_user/applicationUser_listController.js",

                "~/app/components/newsletter/list_customer_email/customerEmail.Module.js",
                "~/app/components/newsletter/list_customer_email/customer_email_list_Controller.js",

                "~/app/components/newsletter/manager_email/emailManager.Module.js",
                "~/app/components/newsletter/manager_email/send_email_Controller.js",
                "~/app/components/newsletter/manager_email/update_email_Controller.js",
                "~/app/components/newsletter/manager_email/list_email_sentController.js",

                "~/app/components/home/homeController.js",
                "~/app/components/home/rootController.js",

                "~/app/components/slide/slide.Module.js",
                "~/app/components/slide/add_slide_controller.js",
                "~/app/components/slide/update_slide_controller.js",
                "~/app/components/slide/list_slide_controller.js",

                "~/app/components/order/order.Module.js",
                "~/app/components/order/add_order_controller.js",
                "~/app/components/order/list_oder_controller.js",

                "~/app/components/logo/logo.Module.js",
                "~/app/components/logo/update_logo_controller.js",
                "~/app/components/logo/list_logo_controller.js",

                "~/app/components/login/loginController.js"
                ));
        }
    }
}
