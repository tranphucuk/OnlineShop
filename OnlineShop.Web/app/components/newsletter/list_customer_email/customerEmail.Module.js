(function () {
    angular.module('onlineShop.customerEmail', ["onlineShop.common"]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('customer_emailList', {
                url: '/customer_email_list',
                parent: 'base',
                templateUrl: '/app/components/newsletter/list_customer_email/customer_email_list_View.html',
                controller: 'customer_email_list_Controller'
            })
    };
})();