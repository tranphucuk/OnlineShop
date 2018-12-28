(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService','$state'];

    function slideListController($scope, apiService, notificationService, $state) {
        $scope.slideList = [];

        $scope.GetlistSlide = function () {
            apiService.get('api/slide/get_all', null, function (success) {
                $scope.slideList = success.data;
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.DeleteSlide = function (slide) {
            apiService.del('api/slide/delete_slide/' + slide.ID, null, function (success) {
                notificationService.DisplaySuccess('Remove success: ' + success.data.Name);
                $scope.GetlistSlide();
            }, function () {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        $scope.GetlistSlide();
    }
})(angular.module('onlineShop.slides'));