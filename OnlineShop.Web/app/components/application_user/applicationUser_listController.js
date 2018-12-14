(function (app) {
    app.controller('applicationUser_listController', applicationUser_listController);

    applicationUser_listController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$stateParams'];

    function applicationUser_listController($scope, apiService, $ngBootbox, notificationService, $stateParams) {
        $scope.userList = [];

        $scope.DeleteUser = function (user) {
            $ngBootbox.confirm('Are you sure to remove user ' + user.UserName + ' ?').then(function () {
                apiService.del('api/application_users/delete_user/' + user.Id, null, function (success) {
                    notificationService.DisplaySuccess('Removed user ' + success.data + ' succeeded.')
                    $scope.GetListUser();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        };

        $scope.GetListUser = function () {
            apiService.get('api/application_users/get_all_user', null, function (success) {
                $scope.userList = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
               
            });
        };
        $scope.GetListUser();
    };
})(angular.module('onlineShop.applicationUsers'));