(function (app) {
    app.controller('update_email_Controller', update_email_Controller);

    update_email_Controller.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'unsignNameService', '$stateParams'];

    function update_email_Controller($scope, apiService, notificationService, $state, unsignNameService, $stateParams) {
        $scope.email = {};

        $scope.LoadEmailContent = function () {
            apiService.get('app/email/load_email/' + $stateParams.emailId, null, function (success) {
                $scope.email = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.SendEmail = function () {
            apiService.post('app/email/send_mail', $scope.email, function (success) {
                notificationService.DisplaySuccess('Sent email to ' + success.data.RecipientEmails.length + ' customers !!!');
                $state.go('list_email_Sent');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.LoadEmailContent();
    };
})(angular.module('onlineShop.emailManager'))