var AdPortal = angular.module('AdPortal', ['ngRoute', 'ui.grid', 'ngResource', 'ngCookies', 'ui.grid.pagination']);

AdPortal.controller('BaseController', BaseController);
AdPortal.controller('AdController', AdController);
AdPortal.controller('AddAdController', AddAdController);
AdPortal.controller('GridController', GridController);
AdPortal.controller('LoginController', LoginController);
AdPortal.controller('RegisterController', RegisterController);

AdPortal.factory('AdFactory', AdFactory);
AdPortal.factory('LoginFactory', LoginFactory);
AdPortal.factory('RegisterFactory', RegisterFactory);

AdPortal.service('SessionService', SessionService);

var configFunction = function ($routeProvider) {
    $routeProvider
        .when('/Views', {
            templateUrl: 'Views/AdGrid.html',
            controller: "GridController",
            controllerAs: "gc"
        })
        .when('/Views/details', {
            templateUrl: 'Views/AddAd.html',
            controller: "AddAdController",
            controllerAs: "ac"
        })
        .when('/Views/details/:id', {
            templateUrl: function (params) { return 'Views/Ad.html?id=' + params.id; },
            controller: "AdController",
            controllerAs: "ac"
        })
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'views/register.html',
            controller: 'RegisterController'
        })
        .otherwise({
          redirectTo: '/Views'
        });
}
configFunction.$inject = ['$routeProvider'];

AdPortal.config(configFunction);











