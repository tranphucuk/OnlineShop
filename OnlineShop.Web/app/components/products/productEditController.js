(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'unsignNameService', '$stateParams'];

    function productEditController($scope, apiService, notificationService, $state, unsignNameService, $stateParams) {

        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px',
            uiColor: '#A2AFBD'
        };

        $scope.ProductInfo = {
            UpdatedDate: new Date,
        };

        $scope.AutogenAlias = function () {
            $scope.ProductInfo.Alias = unsignNameService.Alias($scope.ProductInfo.Name);
        }

        function GetProductInfo() {
            var userId = $stateParams.id;
            apiService.get('api/product/getProductId/' + userId, null, function (success) {
                $scope.ProductInfo = success.data;
            }, function (error) {
                console.log(error.data);
            });
        }

        $scope.UpdateProductInfo = EditProductInfo;
        function EditProductInfo() {
            apiService.put('api/product/update', $scope.ProductInfo, function (result) {
                notificationService.DisplaySuccess('Update ' + result.data.Name + ' succeeded.');
                $state.go('productList');
            }, function (error) {
                notificationService.DisplayError('Update ' + result.data.Name + ' failed.');
            });
        }

        GetProductInfo();
    };

})(angular.module('onlineShop.products'))