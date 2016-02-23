var AddAdController = function ($scope, AdFactory, $routeParams, $location, SessionService) {


    $scope.AddAd = function () {

        alert('sending');
        AdFactory.addAd($scope.ad, addSuccess, addErr);
    };

    function addSuccess(data) {
        alert('addsuccess');
        $location.url('/Views/grid');
    };

    function addErr(data) {
        alert('error during adding: ' + data.statusText);
    };



}

AddAdController.$inject = ['$scope', "AdFactory", '$routeParams', '$location', 'SessionService'];