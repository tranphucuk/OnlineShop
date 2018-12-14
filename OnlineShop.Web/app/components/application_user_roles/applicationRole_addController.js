(function (app) {
    app.controller('applicationRole_addController', applicationRole_addController);

    applicationRole_addController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$state'];
    function applicationRole_addController($scope, apiService, $ngBootbox, notificationService, $state) {
        $scope.newRole = {};

        $scope.AddNewRole = function () {
            apiService.post('api/application_roles/add_role', $scope.newRole, function (success) {
                notificationService.DisplaySuccess('Add new role: ' + success.data.Name + ' succeeded.');
                $state.go('application_listRoles');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };
    };
})(angular.module('onlineShop.applicationRoles'));