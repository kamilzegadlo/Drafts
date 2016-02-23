var AdFactory = function ($q, $resource, SessionService) {
    
    function getAdData(params, success, error) {
        return gridDataApi.GetAdData(
            params,
            function (data) {
                if ($.isFunction(success)) 
                    success(data);
            },
            error
        );
    }

    function deleteAd(params, success, error) {
        return gridDataApi.DeleteAd(
            params,
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    function getAd(params, success, error) {
        return gridDataApi.GetAd(
            params,
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    function editAd(params, success, error) {
        return gridDataApi.EditAd(
            JSON.stringify(params),
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    function getTotalItems(params, success, error) {
        return gridDataApi.GetTotalItems(
            params,
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    function addAd(params, success, error) {
        return gridDataApi.AddAd(
            JSON.stringify(params),
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    function getSellers(params, success, error) {
        return gridDataApi.GetSellers(
            params,
            function (data) {
                if ($.isFunction(success))
                    success(data);
            },
            error
        );
    }

    var gridDataApi = $resource("http://localhost:50482//api/ad/", {}, {
        GetAdData: { method: "GET", url: "http://localhost:50482//api/ad/GetAds/:pageNumber/:pageSize", params: {pageNumber: '@pageNumber', pageSize: '@pageSize'}, cache: false, isArray: true },
        DeleteAd: { method: "DELETE", url: "http://localhost:50482//api/ad/DeleteAd/:id", params: { id: '@id' }, cache: false, isArray: true, headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + SessionService.getToken() } },
        GetAd: { method: "GET", url: "http://localhost:50482//api/ad/GetAd/:id", params: { id: '@id' }, cache: false, isArray: false },
        EditAd: { method: "PUT", url: "http://localhost:50482//api/ad/EditAd/:id", headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + SessionService.getToken() } },
        GetTotalItems: { method: "GET", url: "http://localhost:50482//api/ad/GetTotalItems" },
        AddAd: { method: "POST", url: "http://localhost:50482//api/ad/AddAd", headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + SessionService.getToken() } },
        GetSellers: { method: "GET", url: "http://localhost:50482//api/ad/GetSellers", isArray: true }
    });

    var service = {
        getAdData: getAdData,
        deleteAd: deleteAd,
        getAd: getAd,
        editAd: editAd,
        getTotalItems: getTotalItems,
        addAd: addAd,
        getSellers: getSellers
    };

    return service;
}

AdFactory.$inject = ['$q', '$resource', 'SessionService'];




