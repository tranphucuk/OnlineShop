(function (app) {
    app.controller('pageAddController', pageAddController);

    pageAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function pageAddController($scope, apiService, notificationService, $state) {

        $scope.PageViewModel = {
            Status: true,
            Name: 'Contact',
            Alias: 'contact',
            CreatedDate: new Date,
        }

        $scope.AddNewPage = function () {
            apiService.post('api/page/create', $scope.PageViewModel, function (success) {
                notificationService.DisplaySuccess('Add page ' + success.data.Name + ' succeeded');
                $state.go('pageList');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

    };
})(angular.module('onlineShop.page'));