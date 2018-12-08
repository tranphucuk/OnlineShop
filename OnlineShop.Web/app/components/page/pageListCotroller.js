(function (app) {
    app.controller('pageListCotroller', pageListCotroller);

    pageListCotroller.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'authData', '$location'];

    function pageListCotroller($scope, apiService, notificationService, $ngBootbox, $filter, authData, $location) {

        $scope.keyword = '';
        $scope.searchFunc = search;
        $scope.deleteSinglePage = deleteSinglePage;
        $scope.pageList = [];
        $scope.listRemove = [];

        function search() {
            $scope.GetPageList();
        }

        function deleteSinglePage(p) {
            $ngBootbox.confirm('Are you sure to delete ' + " '" + p.Name + "'" + ' ?').then(function () {
                var config = {
                    params: {
                        pageId: p.ID
                    }
                }
                apiService.del('api/page/delete', config, function (success) {
                    notificationService.DisplaySuccess('Remove ' + success.data.Name + ' succeeded.');
                    search();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            });
        };

        $scope.GetPageList = function () {
            var config = {
                params: {
                    keyword: $scope.keyword,
                }
            };

            apiService.get('api/page/getall', config, function (success) {
                $scope.pageList = success.data;
            }, function (error) {
                console.log(error.data)
            });
        }

        $scope.SelectAll = function () {
            angular.forEach($scope.pageList, function (page) {
                page.select = !$scope.isAll;
            });
        }

        $scope.$watch('pageList', function (n, o) {
            var checked = $filter('filter')(n, { select: true });
            if (checked.length) {
                angular.forEach(checked, function (item) {
                    $scope.listRemove.push(item.ID);
                });
                $('#pageDelete').removeAttr('disabled');
            } else {
                $('#pageDelete').attr('disabled', 'disabled');
            }
        }, true);


        $scope.deleteMultiPages = function () {
            $ngBootbox.confirm('Are you sure to delete ' + $scope.listRemove.length + ' page ?').then(function () {
                var config = {
                    params: {
                        listId: JSON.stringify($scope.listRemove)
                    }
                }
                apiService.del('api/page/deleteMulti', config, function (success) {
                    notificationService.DisplaySuccess('Remove ' + success.data + ' page succeeded.');
                    search();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            });
        }
        $scope.GetPageList();
    }
})(angular.module('onlineShop.page'));