var app = angular.module('mint-app', ['ngRoute', 'ngStorage', 'ngTable']);

app.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
    .when('/login', {
        templateUrl: 'login/login.html',
        controller: 'loginCtr'
    })
    .when('/main', {
        templateUrl: 'main/welcome.html',
        controller: 'welcomeCtr'
    })

     //电子围栏
    .when('/rail', {
        templateUrl: 'electricfence/rail_info.html',
        controller: 'railCtr'
    })

    //车辆
    .when('/bicycle', {
        templateUrl: 'bicycle/bicycle_Info.html',
        controller: 'BicycleCtr'

    })


    //车辆编号生成管理
    .when('/bicycle/ProduceBicycleNumberList', {
        templateUrl: 'bicycle/ProduceBicycleNumberList.html',
        controller: 'BicycleNumberCtr'

    })

    //还车异常处理
    .when('/bicycle/ReturnCarError', {
        templateUrl: 'bicycle/ReturnCarError.html',
        controller: 'BicycleCtr'

    })


       //地图
    .when('/BaiduMap', {
        templateUrl: 'BaiduMap/BaiduMapSearch.html',
        controller: 'BaiduMapCtr'
    })


    .when('/bicycle/Update/:BicycleBaseGuid', {
        templateUrl: 'bicycle/bicycle_Edit.html',
        controller: 'BicycleCtr'
    })


        .when('/wallet', {
            templateUrl: 'wallet/wallet_info.html',
            controller: 'WalletCtr'
        })


    .when('/admin', {
        templateUrl: 'Admin/Admin_Info.html',
        controller: 'adminInfoCtr'
    })
    .when('/admin/password', {
        templateUrl: 'Admin/Admin_Password.html',
        controller: 'adminPasswordCtr'
    })


        //用户管理
        .when('/user', {
            templateUrl: 'customer/userinfo_query.html',
            controller: 'userCtr'
        })

        //交易记录
         .when('/curtrans', {
             templateUrl: 'transaction/transaction_current.html',
             controller: 'curtransCtr'
         })
         .when('/oldtrans', {
             templateUrl: 'transaction/historical_transaction.html',
             controller: 'oldtransCtr'
         })
         .when('/utrans', {
             templateUrl: 'transaction/usertransaction_detail.html',
             controller: 'utransCtr'
         })

         //车辆租借费用
         .when('/rentcharge', {
             templateUrl: 'transaction/rental_charge.html',
             controller: 'rentchargeCtr'
         })
        //供应商管理
        .when('/supplier', {
            templateUrl: 'customer/supplier_query.html',
            controller: 'supplierCtr'
        })
        //生产锁管理
        .when('/productionlock', {
            templateUrl: 'electricfence/devicelock_query.html',
            controller: 'productionlockCtr'
        })
        //客户管理
        .when('/consumer', {
            templateUrl: 'customer/consumer_query.html',
            controller: 'consumerCtr'
        })
        //角色管理
        .when('/role', {
            templateUrl: 'Admin/AdminRole_Info.html',
            controller: 'roleCtr'
        })
        //权限管理
         .when('/perm', {
             templateUrl: 'Admin/privilege.html',
             controller: 'permCtr'
         })
         .when('/serviceman', {
             templateUrl: 'customer/serviceman_query.html',
             controller: 'servicemanCtr'
         })
        //引导页管理
        .when('/gpage', {
            templateUrl: 'main/guidepage.html',
            controller: 'gpageCtr'
        })
        //维修记录管理
        .when('/replog', {
            templateUrl: 'breakdown/repairlog.html',
            controller: 'replogCtr'
        })
        //广告弹窗管理
        .when('/commercial', {
            templateUrl: 'main/advertpop.html',
            controller: 'commercialCtr'
        })
         //客户车辆绑定
        .when('/cusbicyclerig', {
            templateUrl: 'customer/cusbicyclebound.html',
            controller: 'cusbicyclerigCtr'
        })



      //故障维护
    .when('/breakdown', {
        templateUrl: 'breakdown/breakdown_Info.html',
        controller: 'BreakdownCtr'
    })



    .otherwise({ redirectTo: '/main' });
    $httpProvider.interceptors.push('httprequestInterceptor');
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

app.factory('httprequestInterceptor', ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {
    var httpInterceptor = {
        'request': function (config) {
            var loc = $location.path();
            if (loc && loc != '/main')
            {
                var sk = window.sessionStorage.getItem("PermKeyName");
                var deferred = $q.defer();
                if (sk) {
                    sk = decodeURIComponent(sk);
                    if (sk.indexOf(loc) == -1) {
                        return deferred.promise;
                    }
                }
                else {
                    return deferred.promise;
                }
            }
            return config;
        }
    }
    return httpInterceptor;
}]);

app.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {

        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));

            return href;
        }
    };
});






















