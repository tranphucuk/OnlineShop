
(function () {
    angular.module("onlineShop.products", ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('productList', {
            url: '/product_list',
            parent: 'base',
            templateUrl: '/app/components/products/productListView.html',
            controller: 'productListController',

        }).state('productAdd', {
            url: '/product_add',
            parent: 'base',
            templateUrl: '/app/components/products/productAddView.html',
            controller: 'productAddController',

        }).state('productEdit', {
            url: '/product_edit/:id',
            parent: 'base',
            templateUrl: '/app/components/products/productEditView.html',
            controller: 'productEditController',
        });
    }
})();