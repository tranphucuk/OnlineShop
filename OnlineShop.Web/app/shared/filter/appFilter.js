(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            return input == true ? "Active" : "Blocked";
        };
    });
})(angular.module('onlineShop.common'))