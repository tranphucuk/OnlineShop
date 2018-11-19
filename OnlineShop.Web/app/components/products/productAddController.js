(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'unsignNameService']

    function productAddController($scope, apiService, notificationService, $state, unsignNameService) {

        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px',
            uiColor: '#A2AFBD'
        };

        $scope.ChooseImage = function () {
            var ckfinder = new CKFinder();
            ckfinder.selectActionFunction = function (fileUrl) {
                $scope.ProductInfo.Image = fileUrl;
                $scope.$apply();
            }
            ckfinder.popup();
        };

        $scope.ProductInfo = {
            CreatedDate: new Date,
            Status: true,
            Name: 'Product 1',
            ViewCount: 0,
            Image: '',
        }

        $scope.AutogenAlias = function () {
            $scope.ProductInfo.Alias = unsignNameService.Alias($scope.ProductInfo.Name);
        }

        $scope.AddNewProduct = AddNewProduct;
        function AddNewProduct() {
            apiService.post('api/product/create', $scope.ProductInfo, function (result) {
                notificationService.DisplaySuccess('Add ' + result.data.Name + ' succeeded.')
                $state.go('productList');
            }, function (error) {
                notificationService.DisplayError('Add ' + error.data.Name + 'failed.')
            });
        }

        function ListProductCategory() {
            apiService.get('api/product_category/getallParentId', null, function (result) {
                $scope.ListProductCategory = result.data;
            }, function (error) {
                console.log(error.data);
            });
        }
        ListProductCategory();
    }
})(angular.module('onlineShop.products'));