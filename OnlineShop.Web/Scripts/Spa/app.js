/// <reference path="../Plugins/angular/angular.js" />


var myApp = angular.module('myModule', []);

myApp.controller("schoolController", schoolController);
myApp.service('validater', validater);

schoolController.$inject = ['$scope', 'validater'];

function schoolController($scope, validater) {
    //validater.checkNumber(12);
    //$scope.message = "This is my message from School";
    $scope.checkNumber= function() {
        $scope.message = validater.check($scope.numb);
    }
   
    $scope.numb = 1;
}


function validater($window) {
    return {
        check: checkNumber
    }
    function checkNumber(input) {
        //input % 2 === 0 ? $window.alert('This is even') : $window.alert('This is odd');
        return input % 2 === 0 ? 'This is even' : 'This is odd';
    }
}