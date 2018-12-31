(function (app) {
    app.controller('listOrderController', listOrderController);

    listOrderController.$inject = ['$scope', 'apiService', 'notificationService'];

    function listOrderController($scope, apiService, notificationService) {
        $scope.ListOder = [];

        $scope.getCustomerOrder = function () {
            apiService.get('api/order/get_customer_orders', null, function (success) {
                $scope.ListOder = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.getCustomerOrder();
    }
})(angular.module('onlineShop.order'));