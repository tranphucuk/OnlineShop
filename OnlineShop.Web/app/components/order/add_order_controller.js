(function (app) {
    app.controller('createOrderController', createOrderController);

    createOrderController.$inject = ['$scope', '$state', 'apiService', 'notificationService', '$stateParams', '$filter', '$ngBootbox'];

    function createOrderController($scope, $state, apiService, notificationService, $stateParams, $ngBootbox, $filter) {
        $scope.order = {};
        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px',
            uiColor: '#A2AFBD'
        };
        function LoadOrder() {
            apiService.get('api/order/load_order/' + $stateParams.id, null, function (success) {
                $scope.order = success.data;
                $scope.order.CreatedDate = new Date(success.data.CreatedDate).toString('MM/dd/yyyy - hh:mm:ss a');
                $scope.orderDetails = success.data.OrderDetails;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.PrintOrder = function () {
            $ngBootbox.confirm('Confirm print this order ?').then(function () {
                apiService.post('api/order/print_order', $scope.order, function (success) {
                    notificationService.DisplaySuccess('Printed order number: ' + $scope.order.ID + ' successfully.');
                    $state.go('list_order');
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        }

        LoadOrder();
    }
})(angular.module('onlineShop.order'));