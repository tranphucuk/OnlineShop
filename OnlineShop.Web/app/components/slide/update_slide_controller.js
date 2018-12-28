(function (app) {
    app.controller('updateSlideController', updateSlideController);

    updateSlideController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams','$state'];

    function updateSlideController($scope, apiService, notificationService, $stateParams, $state) {
        $scope.slide = {};
        $scope.listImg = [];

        function loadSlideDetail() {
            apiService.get('api/slide/load_details/' + $stateParams.id, null, function (success) {
                $scope.slide = success.data;
                $scope.listImg.push(success.data.Image);
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        }

        $scope.UpdateSlide = function () {
            apiService.put('api/slide/update_slide', $scope.slide, function (success) {
                notificationService.DisplaySuccess('Updated success: ' + success.data.Name);
                $state.go('slideList');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };

        loadSlideDetail();
    };
})(angular.module('onlineShop.slides'));