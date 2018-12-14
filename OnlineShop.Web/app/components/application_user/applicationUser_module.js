(function () {
    angular.module('onlineShop.applicationUsers', ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_listUser', {
                url: '/application_list_user',
                templateUrl: 'app/components/application_user/applicationUser_listView.html',
                parent: 'base',
                controller: 'applicationUser_listController',
            })
            .state('application_addUser', {
                url: '/application_add_user',
                templateUrl: 'app/components/application_user/applicationUser_addView.html',
                parent: 'base',
                controller: 'applicationUser_addController',
            })
            .state('application_editUser', {
                url: '/application_edit_user/:userId',
                templateUrl: 'app/components/application_user/applicationUser_editView.html',
                parent: 'base',
                controller: 'applicationUser_editController',
            })
    }
})();