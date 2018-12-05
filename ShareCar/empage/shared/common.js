var app = angular.module('em-app', ['ngRoute', 'ngStorage']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/main', {
            templateUrl: 'main/welcome.html',
            controller: 'welcomeCtr'
        })

            .when('/admin', {
                templateUrl: 'Admin/Admin_Info.html',
                controller: 'adminInfoCtr'
            })

       .when('/Personalinfo/:UserTag', {
          templateUrl: 'UserInfo/Personal_info.html',
          controller: 'PersonalinfoCtr'
       })

        .when('/UserInfo/Update/:UserGuid', {
            templateUrl: 'UserInfo/info_update.html',
            controller: 'PersonalinfoUpdateCtr'
        })

        .when('/PersonalCenter/:UserGuid', {
            templateUrl: 'UserInfo/Personal_center.html',
            controller: 'PersonCenterCtr'
        })
        .when('/PersonalCenter', {
            templateUrl: 'UserInfo/share.html',
            controller: 'PersonCenterCtr'
        })

 
    

    .otherwise({ redirectTo: '/main' });
}]);

app.run(['$rootScope', '$localStorage', '$sessionStorage', function ($rootScope, $localStorage, $sessionStorage) {
    $rootScope.userInfo = $localStorage.$default({
        userName: '',
        roleId: 0,
        userId: 0,
        userToken: ''
    });
}]);

app.run(function ($rootScope, $location, service) {
    $rootScope.dropdownSet = '0';
    $rootScope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };

    $rootScope.param = function (name) {
        var regexS = "[\\?&]" + name + "=([^&#]*)",
        regex = new RegExp(regexS),
        results = regex.exec(window.location.hash);
        if (results == null) {
            return "";
        } else {
            return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    }

    $rootScope.isNullOrEmpty = function (value) {
        return value == undefined || value == null || value == '' || value == "";
    }

    $rootScope.queryString = function (value) {
        var reg = new RegExp("(^|&)" + value + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return '';
    }
});