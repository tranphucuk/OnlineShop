(function () {
    angular.module('onlineShop.applicationRoles', ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_listRoles', {
                url: '/application_role_list',
                templateUrl: 'app/components/application_user_roles/applicationRole_listView.html',
                parent: 'base',
                controller: 'applicationRole_listController',
            })
            .state('application_addRoles', {
                url: '/application_add_role',
                templateUrl: 'app/components/application_user_roles/applicationRole_addView.html',
                parent: 'base',
                controller: 'applicationRole_addController',
            })
            .state('application_editRoles', {
                url: '/application_edit_role/{:roleId}',
                templateUrl: 'app/components/application_user_roles/applicationRole_editView.html',
                parent: 'base',
                controller: 'applicationRole_editController',
            })
    };
})();