(function () {
    angular.module("onlineShop.productCategory", ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('productCategories', {
            url: '/product_categories',
            parent: 'base',
            templateUrl: '/app/components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        })
            .state('AddProductCategories', {
                url: '/add_product_categories',
                parent: 'base',
                templateUrl: '/app/components/product_categories/productCategoryAddView.html',
                controller: 'productCategoryAddController'
            })

            .state('EditProductCategories', {
                url: '/edit_product_categories/:id',
                parent: 'base',
                templateUrl: '/app/components/product_categories/ProductCategoryEditView.html',
                controller: 'productCategoryEditController'
            });
    }
})()