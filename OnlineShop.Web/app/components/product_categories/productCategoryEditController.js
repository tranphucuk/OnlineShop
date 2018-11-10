/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'unsignNameService']
    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, unsignNameService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
        }

        $scope.GenerateAlias = GenerateAlias;
        function GenerateAlias() {
            $scope.productCategory.Alias = unsignNameService.Alias($scope.productCategory.Name);
        }

        function LoadProductCategoryDetails() {
            apiService.get('/api/product_category/getById/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.DisplayError(error.data);
            });
        }

        $scope.UpdateProductCategory = EditProductCategory;
        function EditProductCategory() {
            apiService.put('/api/product_category/update', $scope.productCategory, function (result) {
                notificationService.DisplaySuccess('Update ' + result.data.Name + ' succeeded.');
                $state.go('productCategories');
            }, function (error) {
                notificationService.DisplayError(error.data);
            });
        }

        function LoadParentCategory() {
            apiService.get('api/product_category/getallParentId', null, function (result) {
                $scope.parentCategoryList = result.data;
            }, function () {
                console.log('get ParentCategory Error');
            });
        }

        LoadProductCategoryDetails();
        LoadParentCategory();
    };

})(angular.module('onlineShop.common'));