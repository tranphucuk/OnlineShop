(function (app) {
    app.controller('applicationUser_editController', applicationUser_editController);

    applicationUser_editController.$inject = ['$scope', 'apiService', '$stateParams', 'notificationService', '$state'];

    function applicationUser_editController($scope, apiService, $stateParams, notificationService, $state) {
        $scope.userDetail = {};

        function LoadGroups() {
            apiService.get('api/application_users/get_list_group', null, function (success) {
                $scope.groups = success.data;
            }, function (error) {
                notificationService.DisplayError('Load groups failed. Error: ' + error.data.Message);
            });
        };


        function GetUserDetail() {
            apiService.get('api/application_users/Get_single_user/' + $stateParams.userId, null, function (success) {
                success.data.Birthday = new Date(success.data.Birthday);
                $scope.userDetail = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.EditUserDetail = function () {
            apiService.put('api/application_users/update_user', $scope.userDetail, function (success) {
                notificationService.DisplaySuccess('Updated user: ' + "' " + success.data + ' succeeded');
                $state.go('application_listUser');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        GetUserDetail();
        LoadGroups();
    }
})(angular.module('onlineShop.applicationUsers'));