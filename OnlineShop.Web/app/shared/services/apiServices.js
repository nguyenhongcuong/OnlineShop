/// <reference path="D:\Downloads\Study\Code\OnlineShopGit\OnlineShop.Web\Assets/Admin/libs/angular/angular.js" />
(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService'];


    function apiService($http, notificationService) {
        return {
            get: get,
            post: post
        }

        function get(url, params, success, failed) {
            $http.get(url, params).then(function (result) {
                success(result);
            },
                function (error) {
                    failed(error);
                });
        }

        function post(url, data, success, falied) {
            $http.post(url, data).then(function (result) {
                success(result);
            },
                function (error) {
                    if (error.status === 401) {
                        notificationService.displayError('Authenticate is required !');
                    } else if (falied != null) {
                        falied(error);
                    }
                });
        }
    }
})(angular.module('onlineShop.common'));