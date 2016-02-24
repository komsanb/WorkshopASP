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

app.controller('StudentController', ['$scope', '$http', function ($scope, $http) {

    $scope.login = function (username, password) {
        var login = {
            'username': username,
            'password': password
        }

        $http.post('api/student/login', login)
            .success(function (data) {

                localStorage.setItem('studentName', data.studentName);
                $scope.studentName = localStorage.getItem('studentName');

                if (data.statusLogin == 'true')
                    parent.location = 'index.html';
                else
                    parent.location = 'login.html';
            })
    }

    $http.get('api/student/students')
        .success(function (response) {
            $scope.students = response
        })

    $scope.register = function (studentCode, firstname, lastname, major) {
        var register = {
            'studentCode': studentCode,
            'studentFirstName': firstname,
            'studentLastname': lastname,
            'major': major
        }
        $http.post('api/student/register', register)
            .success(function (response) {
                parent.location = 'login.html'
            })
    }
}])