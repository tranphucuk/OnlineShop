(function (app) {
    app.controller('applicationGroupAddController', applicationGroupAddController);

    applicationGroupAddController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$state'];

    function applicationGroupAddController($scope, apiService, $ngBootbox, notificationService, $state) {
        $scope.GroupUser = {
            Name: '',
            Description: '',
            Roles: [],
        };

        function LoadListRoles() {
            apiService.get('app/application_group/get_list_roles', null, function (success) {
                $scope.roles = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data);
            });
        }

        $scope.AddNewGroup = function () {
            apiService.post('app/application_group/create_group', $scope.GroupUser, function (success) {
                notificationService.DisplaySuccess('Add group ' + success.data.Name + ' succeeded.');
                $state.go('list_application_group');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data);
            });
        };

        LoadListRoles();
    };
})(angular.module('onlineShop.applicationGroups'));