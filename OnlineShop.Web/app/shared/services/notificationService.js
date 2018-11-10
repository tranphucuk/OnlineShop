(function (app) {
    app.factory("notificationService", notificationService);

    function notificationService() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        function displaySuccess(message) {
            return toastr.success(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(error);
                })
            } else {
                toastr.error(error);
            }
        }

        function displayWarning(message) {
            return toastr.warning(message);
        }

        function displayInfo(message) {
            return toastr.info(message);
        }

        return {
            DisplaySuccess: displaySuccess,
            DisplayError: displayError,
            DisplayWarning: displayWarning,
            DisplayInfo: displayInfo,
        }
    };

})(angular.module("onlineShop.common"));