(function (app) {
    app.controller('applicationRole_listController', applicationRole_listController);

    applicationRole_listController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$stateParams'];

    function applicationRole_listController($scope, apiService, $ngBootbox, notificationService, $stateParams) {
        $scope.roleList = [];
        var listIds = [];
        $scope.keyword = '';
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.isAll = false;
        $scope.CheckAll = function () {
            if ($scope.isAll == false) {
                listIds = [];
                angular.forEach($scope.roleList, function (item) {
                    listIds.push(item.Id);
                    $('#isAll').removeAttr('disabled');
                });
            } else {
                $('#isAll').attr('disabled', 'disabled');
            }
        };

        $scope.DeleteAllRoles = function () {
            $ngBootbox.confirm({ message: 'Are you sure to delete ' + listIds.length + ' roles ?', title: 'Warning' }).then(function () {
                var config = {
                    params: {
                        idString: JSON.stringify(listIds),
                    }
                }
                apiService.del('api/application_roles/delete_multi_roles', config, function (success) {
                    notificationService.DisplaySuccess('Removed ' + success.data + ' roles');
                    $scope.GetListRole();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        }

        $scope.DeleteRole = function (role) {
            $ngBootbox.confirm({ message: 'Are you sure to delete: ' + role.Name + ' ?', title: 'Warning' }).then(function () {
                apiService.del('api/application_roles/delete_role/' + role.Id, null, function (success) {
                    notificationService.DisplaySuccess('Removed role: ' + role.Name);
                    $scope.GetListRole();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        };

        $scope.GetListRole = function (page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    filter: $scope.keyword
                },
            };
            apiService.get('api/application_roles/get_all', config, function (success) {
                $scope.roleList = success.data.Items;
                $scope.page = success.data.Page;
                $scope.pagesCount = success.data.TotalPage;
                $scope.totalCount = success.data.TotalCount
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };
        $scope.GetListRole();
    };
})(angular.module('onlineShop.applicationRoles'))