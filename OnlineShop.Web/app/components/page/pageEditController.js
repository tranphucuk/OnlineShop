(function (app) {
    app.controller('pageEditController', pageEditController);

    pageEditController.$inject = ['$scope', 'apiService', '$stateParams', '$state', 'notificationService'];

    function pageEditController($scope, apiService, $stateParams, $state,notificationService) {
        $scope.pageInfo = [];

        function GetPageById() {
            apiService.get('api/page/pageid/' + $stateParams.id, null, function (success) {
                $scope.pageInfo = success.data;
            }, function (error) {
                console.log(error.data);
            });
        };

        $scope.EditPage = function () {
            apiService.put('/api/page/update', $scope.pageInfo, function (success) {
                $state.go('pageList');
                notificationService.DisplaySuccess('Edit page ' + success.data.Name + ' succeeded');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        GetPageById();
    }
})(angular.module('onlineShop.page'))