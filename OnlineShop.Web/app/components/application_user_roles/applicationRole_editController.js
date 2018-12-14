(function (app) {
    app.controller('applicationRole_editController', applicationRole_editController);

    applicationRole_editController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$state', '$stateParams'];

    function applicationRole_editController($scope, apiService, $ngBootbox, notificationService, $state, $stateParams) {
        $scope.RoleVm = {};

        $scope.GetRoleInfo = function () {
            apiService.get('api/application_roles/get_role_detail/' + $stateParams.roleId, null, function (success) {
                $scope.RoleVm = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.UpdateRoleInfo = function () {
            apiService.put('api/application_roles/update_role', $scope.RoleVm, function (success) {
                notificationService.DisplaySuccess('Update succeeded: ' + success.data.Name);
                $state.go('application_listRoles');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };
        $scope.GetRoleInfo();
    };
})(angular.module('onlineShop.applicationRoles'));