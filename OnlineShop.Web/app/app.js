
(function () {
    angular.module("onlineShop",
        ['onlineShop.products',
            'onlineShop.productCategory',
            'onlineShop.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider

            .state('base', {
                url: '',
                templateUrl: '/app/shared/baseView/baseView.html',
                abstract: true
            })

            .state('login', {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'loginController',
            })

            .state('home', {
                url: '/admin',
                parent: 'base',
                templateUrl: '/app/components/home/homeView.html',
                controller: 'homeController',
            })
        $urlRouterProvider.otherwise('/admin');
    }
})();