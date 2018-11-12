/// <reference path="../../shared/services/notificationservice.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.productCategoryList = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.searchFunc = search;

        function search() {
            $scope.GetProductCategoryList();
        }

        $scope.DeleteProductCategory = deleteProductCategory;
        function deleteProductCategory(id) {
            $ngBootbox.confirm('Are you sure to delete ?').then(function () {
                var config = {
                    param: {
                        id: id
                    }
                }

                apiService.del('/api/product_category/delete/' + config.param.id, null, function (result) {
                    notificationService.DisplaySuccess('Remove ' + result.data.Name + ' succeeded');
                    search();
                }, function (error) {
                    notificationService.DisplayError('Remove ' + error.data.Name + ' failed');
                });
            })
        };

        $scope.GetProductCategoryList = function GetProductCategoryList(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2,
                }
            };
            apiService.get('/api/product_category/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.DisplayInfo('not found.');
                }
                $scope.productCategoryList = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageSize = Math.ceil(result.data.TotalCount / result.data.TotalPage)
                $scope.totalCount = result.data.TotalCount;
                $scope.pagesCount = result.data.TotalPage;
            }, function () {
                console.log('get product category failed.');
            });
        };
        $scope.GetProductCategoryList();
    };
})(angular.module('onlineShop.productCategory'));