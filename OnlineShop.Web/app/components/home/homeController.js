(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$location', 'authenticationService'];
    function homeController($location, authenticationService) {
        function checkLogin() {
            if (authenticationService.getTokenInfo() == undefined) {
                $location.path('login');
            }
        }
        checkLogin();
    }
})(angular.module('onlineShop'));