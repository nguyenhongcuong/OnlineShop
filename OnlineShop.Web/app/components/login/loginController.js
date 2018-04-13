(function (app) {
    app.controller('loginController', loginController);
    loginController.$inject = ['$scope', '$state'];
    function loginController($scope, $state) {
        $scope.loginSubmit = loginSubmit;

        function loginSubmit() {
            $state.go('home');
        }
    }


})(angular.module('onlineShop'));