
(function () {
    angular.module("onlineShop.products", ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('productList', {
            url: '/product_list',
            templateUrl: '/app/components/products/productListView.html',
            controller: 'productListController',
        }).state('productAdd', {
            url: '/product_add',
            templateUrl: '/app/components/products/productAddView.html',
            controller: 'productAddController',
        });
    }
})();