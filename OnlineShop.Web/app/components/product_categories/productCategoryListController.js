/// <reference path="../../shared/services/notificationservice.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategoryList = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.searchFunc = search;

        function search() {
            $scope.GetProductCategoryList();
        }

        $scope.DeleteMultiProductCategory = DeleteMultiProductCategory;
        function DeleteMultiProductCategory() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });

            $ngBootbox.confirm('Are you sure to delete ' + listId.length + ' items ?').then(function () {
                var config = {
                    params: {
                        listId: JSON.stringify(listId),
                    }
                };
                apiService.del('/api/product_category/deletemulti', config, function (success) {
                    notificationService.DisplaySuccess('Remove ' + success.data + ' items succeeded');
                    search();
                }, function (error) {
                    notificationService.DisplayError('Remove ' + error.data + ' items failed');
                });
            });
        }

        $scope.SelectAll = SelectAll;
        $scope.isAll = false;
        function SelectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.productCategoryList, function (item, i) {
                    item.checked = true;
                })
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategoryList, function (item, i) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        };

        $scope.$watch('productCategoryList', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

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

        $scope.loading = true;
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
                $scope.loading = false;
            }, function () {
                console.log('get product category failed.');
                $scope.loading = false;
            });
        };
        $scope.GetProductCategoryList();
    };
})(angular.module('onlineShop.productCategory'));