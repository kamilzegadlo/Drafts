var SessionService = function ($cookies) {
    //this.token = undefined;
    this.apiUrl = "http://localhost:50482//";

    this.getToken = function () {
        return $cookies.AdPortalToken;
    }

    this.getUser = function () {
        return $cookies.AdPortalUser;
    }

    this.setToken = function (token) {
        $cookies.AdPortalToken = token;
    }

    this.setUser = function (userName) {
        $cookies.AdPortalUser = userName;
    }

    this.clearToken = function () {
        $cookies.AdPortalToken = undefined;
    }
}

SessionService.$inject = ['$cookies'];