(function () {
    angular.module('onlineShop.order', ["onlineShop.common"]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('list_order', {
                url: '/list_order',
                parent: 'base',
                templateUrl: '/app/components/order/list_order_View.html',
                controller: 'listOrderController',
            })
            .state('create_order', {
                url: '/create_order/:id',
                parent: 'base',
                templateUrl: '/app/components/order/add_order_View.html',
                controller: 'createOrderController',
            })
    };
})();