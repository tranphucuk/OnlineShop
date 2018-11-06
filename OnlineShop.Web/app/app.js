
(function () {
    angular.module("onlineShop", ['onlineShop.products', 'onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: '/admin',
            templateUrl: '/app/components/home/homeView.html',
            controller: 'homeController',
        })
        $urlRouterProvider.otherwise('/admin');
    }
})();