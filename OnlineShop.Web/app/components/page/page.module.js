(function () {
    angular.module("onlineShop.page", ["onlineShop.common"]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('pageList', {
                url: '/page_list',
                parent: 'base',
                templateUrl: '/app/components/page/pageListView.html',
                controller: 'pageListCotroller'
            })

            .state('pageAdd', {
                url: '/page_add',
                parent: 'base',
                templateUrl: '/app/components/page/pageAddView.html',
                controller: 'pageAddController'
            })

            .state('pageEdit', {
                url: '/page_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/page/pageEditView.html',
                controller: 'pageEditController'
            })

    };
})();