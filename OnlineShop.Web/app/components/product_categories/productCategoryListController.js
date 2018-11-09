(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategoryList = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.searchFunc = search;

        function search() {
            $scope.GetProductCategoryList();
        }

        $scope.GetProductCategoryList = function GetProductCategoryList(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 1,
                }
            };
            apiService.get('/api/product_category/getall', config, function (result) {
                $scope.productCategoryList = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalCount = result.data.TotalCount;
                $scope.pagesCount = result.data.TotalPage;
            }, function () {
                console.log('get product category failed.');
            });
        };
        $scope.GetProductCategoryList();
    };
})(angular.module('onlineShop.productCategory'));