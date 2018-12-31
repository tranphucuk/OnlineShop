(function (app) {
    app.controller('UpdateLogoController', UpdateLogoController);

    UpdateLogoController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$state'];

    function UpdateLogoController($scope, apiService, notificationService, $stateParams, $state) {
        $scope.Logo = {};
        $scope.addImg = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.Logo.ImagePath = fileUrl;
                });
            }
            finder.popup();
        };

        function LoadLogo() {
            apiService.get('api/logo/load_logo/' + $stateParams.id, null, function (success) {
                $scope.Logo = success.data;
                $scope.Logo.CreatedDate = new Date(success.data.CreatedDate);
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.UpdateLogo = function () {
            apiService.put('api/logo/update_logo', $scope.Logo, function (success) {
                notificationService.DisplaySuccess('Update logo: ' + success.data.Name + ' succeeded.');
                $state.go('listLogo');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        LoadLogo();
    };
})(angular.module('onlineShop.logo'))