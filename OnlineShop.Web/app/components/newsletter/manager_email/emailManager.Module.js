(function () {
    angular.module('onlineShop.emailManager', ["onlineShop.common"]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('list_email_Sent', {
                url: '/list_email',
                parent: 'base',
                templateUrl: '/app/components/newsletter/manager_email/list_email_sentView.html',
                controller: 'list_email_sentController'
            })
            .state('send_email_to_customers', {
                url: '/send_email',
                parent: 'base',
                templateUrl: '/app/components/newsletter/manager_email/send_email_View.html',
                controller: 'send_email_Controller'
            })
            .state('load_email_content', {
                url: '/load_email/:emailId',
                parent: 'base',
                templateUrl: '/app/components/newsletter/manager_email/update_email_View.html',
                controller: 'update_email_Controller'
            })
    };
})();