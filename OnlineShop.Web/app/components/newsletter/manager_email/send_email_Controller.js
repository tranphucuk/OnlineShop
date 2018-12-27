(function (app) {
    app.controller('send_email_Controller', send_email_Controller);

    send_email_Controller.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'authData', '$location'];

    function send_email_Controller($scope, apiService, notificationService, $ngBootbox, $filter, authData, $location) {
        $scope.TotalEmailCount = 0;
        $scope.EmailAddress = '';
        $scope.EmailManager = {};

        $scope.ckEditorOptions = {
            language: 'en',
            height: '400px',
            uiColor: '#A2AFBD'
        };

        $scope.SendEmail = function () {
            apiService.post('app/email/send_mail', $scope.EmailManager, function (success) {
                notificationService.DisplaySuccess('Sent email to ' + success.data.RecipientEmails.length + ' customers !!!');
                $state.go('list_email_Sent');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        function GetAllEmail() {
            apiService.get('app/email/total_email_count', null, function (success) {
                $scope.TotalEmailCount = success.data.RecipientEmails.length;
                $scope.EmailManager.RecipientEmails = success.data.RecipientEmails;
                $scope.EmailManager.EmailUser = success.data.EmailUser;
                $scope.EmailAddress = success.data.EmailUser;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        GetAllEmail();
    }
})(angular.module('onlineShop.emailManager'))