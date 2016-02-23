var GridController = function ($scope, AdFactory, $location, SessionService) {
    var gc = this;
    gc.gridService = AdFactory;

    $scope.clickEdit = function (d) {
        // this never gets invoked ?!
        alert("edit.");
        $location.url('/Views/details/'+d);
    };

    $scope.clickDelete = function (d) {
        // this never gets invoked ?!
        alert("delete.");
        
        gc.gridService.deleteAd({id:d}, deleteSuccess, deleteErr);
    };

    function deleteSuccess(data) {
        alert('delete success');
        gc.gridService.getAdData({ pageNumber: gc.gridOptions.paginationCurrentPage, pageSize: gc.gridOptions.paginationPageSize }, setAds, setErr);
    };

    function deleteErr(data) {
        alert('error during delete: ' + data.statusText);
    };

    $scope.IsOwner = function (user) {
        return SessionService.getUser() == user;
    }

    $scope.SearchParams = {
        Title: '',
        Description: '',
        SellerID: '',
        PriceFrom: 0,
        PriceTo:0
    }

    gc.gridOptions = {
        paginationPageSizes: [3, 5],
        paginationPageSize: 3,
        paginationCurrentPage: 1,
        useExternalPagination: true,
        data: "gc.ads",
        totalItems: "gc.totalItems",
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEditOnFocus: true,
        enableColumnResize: true,
        multiSelect: false,
        noUnselect: true,
        enableRowHeaderSelection: false,
        enableCellEdit: false,
        enableColumnMenus: false,
        columnDefs: [
                    { name: ' ', cellTemplate: '<div><button class="btn btn-warning" ng-click="grid.appScope.clickEdit(row.entity.Id)"><i class="glyphicon glyphicon-edit"/>Edit</button></div>' },
                    { name: '  ', cellTemplate: '<div><button class="btn btn-danger" ng-disabled="!grid.appScope.IsOwner(row.entity.Seller)" ng-click="grid.appScope.clickDelete(row.entity.Id)"><i class="glyphicon glyphicon-remove"/>Delete</button></div>' },
                    { name: 'Id', field: 'Id' },
                    { name: 'Title', field: 'Title' },
                    { name: 'Description', field: 'Description' },
                    { name: 'Seller', field: 'Seller' },
                    { name: 'Price', field: 'Price' }],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                getPage();
            });
        }
    };

    $scope.Search = function () {
        gc.gridService.getAdData({ pageNumber: gc.gridOptions.paginationCurrentPage, pageSize: gc.gridOptions.paginationPageSize, Title: $scope.SearchParams.Title, Description: $scope.SearchParams.Description, SellerID: $scope.SearchParams.SellerID, PriceFrom: $scope.SearchParams.PriceFrom, PriceTo: $scope.SearchParams.PriceTo }, setAds, setErr);
    }

    var getPage = function () {
        gc.gridService.getAdData({ pageNumber: gc.gridOptions.paginationCurrentPage, pageSize: gc.gridOptions.paginationPageSize, Title: $scope.SearchParams.Title, Description: $scope.SearchParams.Description, SellerID: $scope.SearchParams.SellerID, PriceFrom: $scope.SearchParams.PriceFrom, PriceTo: $scope.SearchParams.PriceTo }, setAds, setErr);
    };

    activate();

    function activate() {
        gc.ads = [{ Id: 'Loading...', Title: 'Loading...', Description: 'Loading...', Seller: 'Loading...', Price: 'Loading...' }];
        gc.gridService.getAdData({ pageNumber: gc.gridOptions.paginationCurrentPage, pageSize: gc.gridOptions.paginationPageSize, Title: $scope.SearchParams.Title, Description: $scope.SearchParams.Description, SellerID: $scope.SearchParams.SellerID, PriceFrom: $scope.SearchParams.PriceFrom, PriceTo: $scope.SearchParams.PriceTo }, setAds, setErr);
        gc.gridService.getTotalItems(null, setTotalItems, setTotalItemsErr);
    };

    loadSellers();

    function loadSellers() {
        gc.gridService.getSellers(null, okGetSellers, errGetSellers);
    };

    function okGetSellers(data) {
        $scope.Sellers = $.merge([{ Id: '', UserName: '' }], data);
    };

    function errGetSellers(data) {
        alert('Error during getting sellers:' + data.statusText);
    };

    function setTotalItems(data) {
        gc.totalItems = data;
    };

    function setTotalItemsErr(data) {
        alert('Error during getting total items:' + data.statusText);
    };

    function setAds(data) {
        gc.ads = data;
    };

    function setErr(data) {
        alert('Error during getting data:' + data.statusText);
    };
}

GridController.$inject = ['$scope', "AdFactory", '$location', 'SessionService'];