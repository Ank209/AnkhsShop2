/* main: startup script creates the 'todo' module */
(function() {

    // app module depends on "Breeze Angular Service"
    var app = angular.module('app', ['ngRoute', 'breeze.angular']);

    app.config(function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "app/login.html",
            controller: "LoginController"
        })
        .when("/view", {
            templateUrl: "app/view.html",
            controller: "ReqController"
        })
        .when("/newreq", {
            templateUrl: "app/newReq.html",
            controller: "NewReqController"
        })
        .when("/movies", {
            templateUrl: "app/movies.html",
            controller: "MovieController"
        })
    });

    app.run(['$rootScope',function($rootScope){
        $rootScope.userId = 1; // TODO: -1 for logged out
    }]);

})();