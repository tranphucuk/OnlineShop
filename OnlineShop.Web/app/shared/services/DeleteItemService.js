(function (app) {
    app.service('deleteService', deleteService)

    deleteService.$inject = ['apiService', 'notificationService', '$ngBootbox', '$stateParams'];

    function deleteService(apiService, notificationService, $ngBootbox, $stateParams) {

        return {
            singleItem: singleItem,
            multiItems: multiItems
        };

        function singleItem(url,callback) {
            $ngBootbox.confirm('Are you sure to delete this item ?').then(function () {
                apiService.del(url, null, function (result) {
                    notificationService.DisplaySuccess('Remove ' + result.data.Name + ' succeeded.')
                    callback();
                }, function (error) {
                    notificationService.DisplayError('Remove ' + error.data.Name + ' failed.')
                });
            });
        }

        function multiItems(url,callback) {

        }
    };
})(angular.module('onlineShop.common'));