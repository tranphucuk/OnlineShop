(function (app) {
    app.controller('customer_email_list_Controller', customer_email_list_Controller);

    customer_email_list_Controller.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'authData', '$location'];

    function customer_email_list_Controller($scope, apiService, notificationService, $ngBootbox, $filter, authData, $location) {
        $scope.emailList = [];
        $scope.keyword = '';
        $scope.listSelectedCheckbox = [];
        $scope.searchFunc = function () {
            $scope.getAllEmails();
        }

        $scope.loading = true;
        $scope.getAllEmails = function (page) {
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            };
            apiService.get('app/email/getall', config, function (success) {
                $scope.emailList = success.data;
                $scope.loading = false;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data);
            });
        };
        $scope.deleteSingleEmail = function (email) {
            $ngBootbox.confirm('Are you sure to delete email: ' + email.EmailAddress + ' ?').then(function () {
                var config = {
                    params: {
                        emailId: email.ID
                    }
                }
                apiService.del('app/email/delete', config, function (success) {
                    notificationService.DisplaySuccess('Removed success: ' + success.data.EmailAddress);
                    $scope.getAllEmails();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function (error) {
                notificationService.DisplayError("Error: " + error.data);
            });
        };

        var isAll = false;
        $scope.SelectAll = function () {
            isAll = !isAll;
            angular.forEach($scope.emailList, function (item) {
                item.select = isAll;
            });
        }
        $scope.$watch('emailList', function (n, o) {
            $scope.listSelectedCheckbox = [];
            var checkedList = $filter('filter')(n, { select: true });
            if (checkedList.length) {
                angular.forEach(checkedList, function (item) {
                    $scope.listSelectedCheckbox.push(item.ID);
                });
                $('#emailDelete').removeAttr('disabled');
            } else {
                $('#emailDelete').attr('disabled', 'disabled');
            }
        }, true);

        $scope.deleteMultiEmails = function () {
            $ngBootbox.confirm('Are you sure to delete ' + $scope.listSelectedCheckbox.length + ' emails?').then(function () {
                var config = {
                    params: {
                        ids: JSON.stringify($scope.listSelectedCheckbox),
                    }
                };
                apiService.del('app/email/deleteMulti', config, function (success) {
                    notificationService.DisplaySuccess('Removed succeeded: ' + success.data + ' emails.');
                    $scope.getAllEmails();
                }, function (error) {
                    notificationService.DisplayError('Error: ' + error.data.Message);
                });
            }, function () {

            });
        };

        $scope.getAllEmails();
    };
})(angular.module('onlineShop.customerEmail'));