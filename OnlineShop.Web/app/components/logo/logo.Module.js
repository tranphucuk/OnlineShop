(function () {
    angular.module('onlineShop.logo', ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('listLogo', {
                url: '/list_logo',
                parent: 'base',
                templateUrl: '/app/components/logo/list_logo_View.html',
                controller: 'ListLogoController',
            })
            .state('updateLogo', {
                url: '/update_logo/:id',
                parent: 'base',
                templateUrl: '/app/components/logo/update_logo_View.html',
                controller: 'UpdateLogoController',
            })
    };
})();