(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService', '$location'];
    function rootController($state, authData, loginService, $scope, authenticationService, $location) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        };
        $scope.authentication = authData.authenticationData;

        function checkLogin() {
            if (authenticationService.getTokenInfo() == undefined) {
                $location.path('login');
            }
        }

        $scope.mainSidebar = "/app/shared/view_include/main_sidebar.html";
        $scope.topSideBar = "/app/shared/view_include/top_SideBar.html";
        checkLogin();
    }

})(angular.module('onlineShop'));