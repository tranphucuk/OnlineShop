(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService', '$location'];
    function rootController($state, authData, loginService, $scope, authenticationService, $location) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        };
        $scope.authentication = authData.authenticationData;
        //authenticationService.validateRequest();

        function checkLogin() {
            if (authenticationService.getTokenInfo() == undefined) {
                $location.path('login');
            }
        }

        checkLogin();
    }

})(angular.module('onlineShop'));