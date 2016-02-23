var BaseController = function ($scope, SessionService, $location) {
    //$scope.loggedIn;


    $scope.loggedIn = function () {
        return (SessionService.getToken() != undefined);
    }

    /*$scope.loggedIn1 = function () {
        $scope.loggedIn=(SessionService.getToken() != undefined);
    }*/

    $scope.logOut = function () {
        SessionService.clearToken();
        
        //$scope.loggedIn1();
        $location.path('/');
    }

    // $scope.loggedIn1();
}
BaseController.$inject = ['$scope', 'SessionService', '$location'];