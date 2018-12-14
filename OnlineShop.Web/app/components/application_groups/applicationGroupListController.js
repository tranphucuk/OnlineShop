(function (app) {
    app.controller('applicationGroupListController', applicationGroupListController);

    applicationGroupListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$stateParams'];

    function applicationGroupListController($scope, apiService, $ngBootbox, notificationService, $stateParams) {
        $scope.groupUserList = [];
        var listIds = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.GetGroupUser = function (page) {
            page = page || 0;
            var config = {
                params: {
                    filter: $scope.keyword,
                    page: page,
                }
            }
            apiService.get('app/application_group/get_all', config, function (success) {
                $scope.groupUserList = success.data.Items;
                $scope.MaxPage = success.data.MaxPage;
                $scope.page = success.data.Page;
                $scope.totalCount = success.data.TotalCount;
                $scope.pagesCount = success.data.TotalPage;
            }, function (error) {
                notificationService.DisplayError(error.data.Message);
            });
        }

        $scope.DeleteGroup = function (group) {
            $ngBootbox.confirm({ message: 'Are you sure to delete ' + group.Name + ' ?', title: 'Alert' }).then(function () {
                var config = {
                    params: {
                        groupId: group.ID
                    }
                };
                apiService.del('app/application_group/delete_group', config, function (success) {
                    notificationService.DisplaySuccess('Delete succeeded: ' + success.data.Name);
                    $scope.GetGroupUser();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {
                $scope.GetGroupUser();
            });
        }

        $scope.isAll = false;
        $scope.CheckAll = function () {
            if ($scope.isAll == false) {
                listIds = [];
                $('#isAll').removeAttr('disabled');
                angular.forEach($scope.groupUserList, function (item) {
                    listIds.push(item.ID);
                });
            } else {
                $('#isAll').attr('disabled', 'disabled');
            }
        }

        $scope.DeleteAllGroups = function () {
            $ngBootbox.confirm({ message: 'Are you sure to delete ' + listIds.length + ' groups ?', title: 'Warning' }).then(function () {
                var config = {
                    params: {
                        groupIds: JSON.stringify(listIds),
                    }
                };
                apiService.del('app/application_group/delete_multi_groups', config, function (success) {
                    notificationService.DisplaySuccess('Delete succeeded ' + success.data + ' groups.');
                    $scope.GetGroupUser();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            });
        }
        $scope.GetGroupUser();
    }
})(angular.module('onlineShop.applicationGroups'));