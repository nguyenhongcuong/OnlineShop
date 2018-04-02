(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        

        function displaySuccess(msg) {
            toastr.success(msg);
        }


        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(error);
            }
        }


        function displayWarning(msg) {
            debugger;
            toastr.warning(msg);
        }


        function displayInfo(msg) {
            toastr.info(msg);
        }


        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }

})(angular.module('onlineShop.common'));