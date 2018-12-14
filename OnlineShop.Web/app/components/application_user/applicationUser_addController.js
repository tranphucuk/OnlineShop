(function (app) {
    app.controller('applicationUser_addController', applicationUser_addController);

    applicationUser_addController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$state'];

    function applicationUser_addController($scope, apiService, $ngBootbox, notificationService, $state) {
        $scope.newUser = {
            Groups: []
        };

        function LoadGroups() {
            apiService.get('api/application_users/get_list_group', null, function (success) {
                $scope.groups = success.data;
            }, function (error) {
                notificationService.DisplayError('Load groups failed. Error: ' + error.data.Message);
            });
        };

        $scope.AddNewUser = function () {
            apiService.post('api/application_users/create_user', $scope.newUser, function (success) {
                notificationService.DisplaySuccess('Created user: ' + "'" + success.data.UserName + "' succeeded.");
                $state.go('application_listUser');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        LoadGroups();
    };

})(angular.module('onlineShop.applicationUsers'));