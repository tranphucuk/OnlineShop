(function (app) {
    app.controller('addSlideController', addSlideController);

    addSlideController.$inject = ['$scope', 'notificationService', 'apiService','$state'];

    function addSlideController($scope, notificationService, apiService, $state) {
        $scope.slide = {};
        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px',
            uiColor: '#A2AFBD'
        };

        $scope.addImg = function () {
            $scope.listImg = [];
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.listImg.push(fileUrl);
                    $scope.slide.Image = fileUrl;
                });
            }
            finder.popup();
        };

        $scope.AddNewSlide = function () {
            apiService.post('api/slide/create_slide', $scope.slide, function (success) {
                notificationService.DisplaySuccess('Add slide: ' + success.data.Name + ' succeeded.');
                $state.go('slideList');
            }, function (error) {
                notificationService.DisplayError('Error: ' + error.data.Message);
            });
        };
    }

})(angular.module('onlineShop.slides'));