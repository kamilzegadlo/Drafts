var AdController = function ($scope, AdFactory, $routeParams, $location, SessionService) {
    

    AdFactory.getAd({ id: $routeParams.id }, getAd, getErr);

    $scope.IsOwner = false;

    function getAd(data) {
        debugger;
        $scope.ad = data;
        $scope.IsOwner = ($scope.ad.Seller == SessionService.getUser());

    };

    function getErr(data) {
        alert('Error during getting data:' + data.statusText);
    };

    $scope.Edit = function () {

        alert('sending');
        //$scope.ad.$save();
        AdFactory.editAd($scope.ad, editSuccess, editErr);
    };

    function editSuccess(data) {
        alert('editsuccess');
        $location.url('/Views/grid');
    };

    function editErr(data) {
        alert('error during edit: ' + data.statusText);
    };



}

AdController.$inject = ['$scope', "AdFactory", '$routeParams', '$location', 'SessionService'];