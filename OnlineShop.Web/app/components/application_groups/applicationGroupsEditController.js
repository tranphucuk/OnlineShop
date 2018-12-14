(function (app) {
    app.controller('applicationGroupsEditController', applicationGroupsEditController);

    applicationGroupsEditController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$state'];

    function applicationGroupsEditController($scope, apiService, notificationService, $stateParams, $state) {
        $scope.GroupInfo = {
            Roles: [],
        };

        function LoadListRoles() {
            apiService.get('app/application_group/get_list_roles', null, function (success) {
                $scope.roles = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data);
            });
        }

        $scope.GetGroupInfo = function () {
            apiService.get('app/application_group/get_group_id/' + $stateParams.groupId, null, function (success) {
                $scope.GroupInfo = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.UpdateGroupInfo = function () {
            apiService.put('app/application_group/update_group', $scope.GroupInfo, function (success) {
                notificationService.DisplaySuccess('Updated success: ' + success.data.Name);
                $state.go('list_application_group');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.GetGroupInfo();
        LoadListRoles();
    };
})(angular.module('onlineShop.applicationGroups'));