(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'unsignNameService', '$stateParams'];

    function productEditController($scope, apiService, notificationService, $state, unsignNameService, $stateParams) {
        //$scope.moreImges = [];
        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px',
            uiColor: '#A2AFBD'
        };

        $scope.ProductInfo = {
            UpdatedDate: new Date,
        };

        $scope.ChooseImage = function () {
            var ckfinder = new CKFinder();
            ckfinder.selectActionFunction = function (fileUrl) {
                $scope.ProductInfo.Image = fileUrl;
            }
            ckfinder.popup();
        };

        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImges.push(fileUrl);
                });
            }
            finder.popup();
        };

        $scope.AutogenAlias = function () {
            $scope.ProductInfo.Alias = unsignNameService.Alias($scope.ProductInfo.Name);
        }

        function ListProductCategory() {
            apiService.get('api/product_category/getallParentId', null, function (result) {
                $scope.ListProductCategory = result.data;
            }, function (error) {
                console.log(error.data);
            });
        }

        function GetProductInfo() {
            var userId = $stateParams.id;
            apiService.get('api/product/getProductId/' + userId, null, function (success) {
                $scope.moreImges = JSON.parse(success.data.MoreImages);
                $scope.ProductInfo = success.data;
            }, function (error) {
                console.log(error.data);
            });
        }

        $scope.UpdateProductInfo = EditProductInfo;
        function EditProductInfo() {
            $scope.ProductInfo.MoreImages = JSON.stringify($scope.moreImges);
            apiService.put('api/product/update', $scope.ProductInfo, function (result) {
                notificationService.DisplaySuccess('Update ' + result.data.Name + ' succeeded.');
                $state.go('productList');
            }, function (error) {
                notificationService.DisplayError('Update ' + result.data.Name + ' failed.');
            });
        }

        GetProductInfo();
        ListProductCategory();
    };

})(angular.module('onlineShop.products'))