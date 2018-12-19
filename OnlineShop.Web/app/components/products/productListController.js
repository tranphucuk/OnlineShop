/// <reference path="../../shared/services/apiservice.js" />
(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', '$filter', 'notificationService', 'deleteService', '$ngBootbox'];

    function productListController($scope, apiService, $filter, notificationService, deleteService, $ngBootbox) {

        $scope.ProductList = [];
        $scope.keyword = '';
        $scope.searchFunc = searchFunc;

        function searchFunc() {
            $scope.GetProductList();
        }

        $scope.deleteSingleProduct = deleteSingleProduct;
        function deleteSingleProduct(id) {
            deleteService.singleItem('api/product/delete/' + id, function () {
                searchFunc();
            });
        };

        $scope.deleteMultiProduct = deleteMultiProduct;
        function deleteMultiProduct() {
            var listProducts = [];
            angular.forEach($scope.selected, function (item, i) {
                listProducts.push(item.ID);
            });

            var config = {
                params: {
                    idJsonArray: JSON.stringify(listProducts)
                }
            }

            $ngBootbox.confirm('Are you sure to delete ' + $scope.selected.length + ' products ?').then(function () {
                apiService.del('api/product/deleteMulti', config, function (success) {
                    notificationService.DisplaySuccess('Deleted ' + success.data + ' items succeeded.');
                    searchFunc();
                }, function (error) {
                    notificationService.DisplayError('Deleted ' + $scope.selected.length + ' items failed.');
                });
            });
        };

        $scope.isAll = false;
        $scope.SelectAll = SelectAll;
        function SelectAll() {
            if ($scope.isAll == false) {
                $.each($scope.ProductList, function (i, item) {
                    item.checked = true;
                });
            } else {
                $.each($scope.ProductList, function (i, item) {
                    item.checked = false;
                });
            }
        };

        $scope.$watch('ProductList', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        $scope.loading = true;
        $scope.GetProductList = GetProductList;
        function GetProductList(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 6,
                },
            };

            apiService.get('api/product/getall', config, function (success) {
                $scope.ProductList = success.data.Items;
                $scope.page = success.data.Page;
                $scope.pagesCount = success.data.TotalPage;
                $scope.totalCount = success.data.TotalCount;
                $scope.loading = false;
            }, function (error) {
                notificationService.DisplayError(error.data)
                $scope.loading = false;
            });
        }
        $scope.GetProductList();
    }
})(angular.module('onlineShop.products'));