//var PersonApp = angular.module("PersonApp", []);
//PersonApp.controller("PersonController", function ($scope, PersonService) {
//    getPersons();
//    function getPersons() {
//        PersonService.getPersons()
//            .success(function (pers) {
//                $scope.persons = pers;
//                console.log($scope.persons);
//            })
//            .error(function (error) {
//                $scope.status = 'Unable to load customer data:' + error.message;
//                console.log($scope.status);
//            })
//    }
//});

//PersonApp.factory('PersonService', ['$http', function ($http) {
//    var PersonService = {};
//    PersonService.getPersons = function () {
//        return $http.get("/Home/GetPersons");
//    };
//    return PersonService;
//}]);


var StudentApp = angular.module('StudentApp', []);
StudentApp.factory('StudentService', ['$http', function ($http) {

    var StudentService = {};
    StudentService.getStudents = function () {
        return $http.get('/Home/GetPersons');
    };
    return StudentService;

}]);
StudentApp.controller('StudentController', function ($scope, StudentService) {

      getStudents();
    function getStudents() {
        return StudentService.getStudents().then(function (stus) {
            $scope.students = stus.data;
            console.log(stus.data);
        });
            //.then(function (studs) {
            //    //$scope.students = studs;
            //    console.log($scope.students);
            //},function (error) {
            //    $scope.status = 'Unable to load customer data: ' + error.message;
            //    console.log($scope.status);
            //});
    }
});

