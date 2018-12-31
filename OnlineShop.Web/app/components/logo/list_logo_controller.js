(function (app) {
    app.controller('ListLogoController', ListLogoController);

    ListLogoController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function ListLogoController($scope, apiService, notificationService, $ngBootbox) {
        $scope.listLogo = [];

        $scope.GetListLogo = function () {
            apiService.get('api/logo/get_all', null, function (success) {
                $scope.listLogo = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.GetListLogo();
    };
})(angular.module('onlineShop.logo'));