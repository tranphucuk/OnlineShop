/// <reference path="../../shared/services/apiservice.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategoryList = [];

        $scope.GetProductCategoryList = function GetProductCategoryList() {
            apiService.get('api/product_category/getall', null, function (result) {
                $scope.productCategoryList = result.data;
            }, function () {
                console.log('get product category failed.')
            });
        };
        $scope.GetProductCategoryList();
    };
})(angular.module('onlineShop.productCategory'));