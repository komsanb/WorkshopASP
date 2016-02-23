var app = angular.module('myApp', ['ngRoute', 'ngSanitize']);

app.config(['$routeProvider',
       function ($routeProvider) {
           $routeProvider.
               when('/', {
                   templateUrl: 'Views/home.html',
               }).
               when('/data', {
                   templateUrl: 'Views/tables.html'
               }).
              otherwise({
                  redirectTo: '/login.html'
              });
       }]);

