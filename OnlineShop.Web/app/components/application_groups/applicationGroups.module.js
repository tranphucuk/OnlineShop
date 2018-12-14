(function () {
    angular.module('onlineShop.applicationGroups', ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('list_application_group', {
            url: '/list_app_group',
            templateUrl: '/app/components/application_groups/applicationGroupListView.html',
            controller: 'applicationGroupListController',
            parent: 'base'

        }).state('add_application_group', {
            url: '/add_app_group',
            templateUrl: '/app/components/application_groups/applicationGroupAddView.html',
            controller: 'applicationGroupAddController',
            parent: 'base'

        }).state('edit_application_group', {
            url: '/edit_app_group/{:groupId}',
            templateUrl: '/app/components/application_groups/applicationGroupsEditView.html',
            controller: 'applicationGroupsEditController',
            parent: 'base'
        });
    }
})();