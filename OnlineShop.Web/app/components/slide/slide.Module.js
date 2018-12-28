(function () {
    angular.module('onlineShop.slides', ['onlineShop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('slideList', {
                url: '/slide_list',
                parent: 'base',
                templateUrl: '/app/components/slide/list_slide_View.html',
                controller: 'slideListController',
            })
            .state('addSlide', {
                url: '/add_slide',
                parent: 'base',
                templateUrl: '/app/components/slide/add_slide_View.html',
                controller: 'addSlideController',
            })
            .state('updateSlide', {
                url: '/update_slide/:id',
                parent: 'base',
                templateUrl: '/app/components/slide/update_slide_View.html',
                controller: 'updateSlideController',
            })
    };
})();