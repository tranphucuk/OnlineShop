(function (app) {
    app.service("apiService", apiService);

    apiService.$inject = ["$http", "notificationService", "authenticationService", "authData","$location"];

    function apiService($http, notificationService, authenticationService, authData, $location) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        function post(url, data, success, failure) {
            if ((authData.authenticationData.IsAuthenticated == false) || (authData.authenticationData.accessToken == undefined) || (authData.authenticationData.accessToken == null) || (authData.authenticationData.accessToken == "")) {
                $location.path('login');
            } else {
                authenticationService.setHeader();
                $http.post(url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.DisplayError('Authentication is required');
                    } else if (failure != null) {
                        failure(error);
                    }
                });
            }
        }

        function put(url, data, success, failure) {
            if ((authData.authenticationData.IsAuthenticated == false) || (authData.authenticationData.accessToken == undefined) || (authData.authenticationData.accessToken == null) || (authData.authenticationData.accessToken == "")) {
                $location.path('login');
            } else {
                authenticationService.setHeader();
                $http.put(url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.DisplayError('Authentication is required');
                    } else if (failure != null) {
                        notificationService.DisplayError(failure(error));
                    }
                });
            }
        }

        function del(url, data, success, failure) {
            if ((authData.authenticationData.IsAuthenticated == false) || (authData.authenticationData.accessToken == undefined) || (authData.authenticationData.accessToken == null) || (authData.authenticationData.accessToken == "")) {
                $location.path('login');
            } else {
                authenticationService.setHeader();
                $http.delete(url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.DisplayError('Authentication is required');
                    } else if (failure != null) {
                        failure(error);
                    }
                });
            }
        }

        function get(url, params, success, failure) {
            if ((authData.authenticationData.IsAuthenticated == false) || (authData.authenticationData.accessToken == undefined) || (authData.authenticationData.accessToken == null) || (authData.authenticationData.accessToken == "")) {
                $location.path('login');
            } else {
                authenticationService.setHeader();
                $http.get(url, params).then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                });
            }
        }
    };
})(angular.module("onlineShop.common"));