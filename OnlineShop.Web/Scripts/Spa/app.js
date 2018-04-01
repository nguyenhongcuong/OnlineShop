/// <reference path="../Plugins/angular/angular.js" />


var myApp = angular.module('myModule', []);

myApp.controller("teacherController", teacherController);
myApp.controller("studentController", studentController);
myApp.controller("schoolController", schoolController);

//myController.$inject = ['$scope'];

// delare
function teacherController($rootScope, $scope) {
    //$scope.message = "This is my message form TeacherController";
    //$rootScope.message = "This is my message form TeacherController";
}

function studentController($scope) {
    //$scope.message = "This is my message form StudentController";
}

function schoolController($scope) {
    $scope.message = "This is my message from School";
}