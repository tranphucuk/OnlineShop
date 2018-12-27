(function (app) {
    app.controller('list_email_sentController', list_email_sentController);

    list_email_sentController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'authData', '$location'];

    function list_email_sentController($scope, apiService, notificationService, $ngBootbox, $filter, authData, $location) {
        $scope.emailList = [];

        $scope.GetEmailList = function () {
            apiService.get('app/email/list_email_sent', null, function (success) {
                $scope.emailList = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.DeleteSingleEmail = function (email) {
            $ngBootbox.confirm('Are you sure to remove email: ' + email.MailTitle + ' ?').then(function () {
                var config = {
                    params: {
                        emailId: email.ID
                    }
                }
                apiService.del('app/email/delete_email', config, function (success) {
                    notificationService.DisplaySuccess('Remove succeeded: ' + success.data.MailTitle);
                    $scope.GetEmailList();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        }

        $scope.GetEmailList();
    }
})(angular.module('onlineShop.emailManager'));