/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state']
    function productCategoryAddController(apiService, $scope, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
            Name: 'Product Category 1',
        }

        $scope.ChooseImage = function () {
            var ckfinder = new CKFinder();
            ckfinder.selectActionFunction = function (fileUrl) {
                $scope.productCategory.Image = fileUrl;
                $scope.$apply();
            }
            ckfinder.popup();
        };

        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('/api/product_category/create', $scope.productCategory, function (result) {
                notificationService.DisplaySuccess('Add ' + result.data.Name + ' succeeded.');
                $state.go('productCategories');
            }, function (error) {
                notificationService.DisplayError('Create failed, try again.');
            });
        }

        function LoadParentCategory() {
            apiService.get('api/product_category/getallParentId', null, function (result) {
                $scope.parentCategoryList = result.data;
            }, function () {
                console.log('get ParentCategory Error');
            });
        }

        LoadParentCategory();
    };

})(angular.module('onlineShop.common'));