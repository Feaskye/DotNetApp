URL_DEV = 'http://localhost:24623/api/';
URL_PRO = 'http://162.10.161.122/mtadminservice/api/';         //测试服务器API地址
ULR = URL_DEV;
WEBURL = 'http://localhost:6561/';

var AccessCodeKeyName = "AccessCode";
var UserNameKeyName = "UserName";
var RoleNameKeyName = "RoleName";
var AdminGuidKeyName = "AdminGuid";


app.constant('config', {
    serviceUrl: ULR
});

app.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|#|chrome-extension):/);
}]);

//app.config(function ($httpProvider) {
//    $httpProvider.defaults.headers.common = { 'AccessCode': window.sessionStorage.getItem(AccessCodeKeyName) };
//})
// 在ajax访问中加上头部参数AccessCode
app.config(function ($httpProvider) {
    $httpProvider.defaults.headers.common = { 'AccessCode': window.sessionStorage.getItem(AccessCodeKeyName) };
})


//$.ajaxSetup({


//    beforeSend: function (request) {
//        try {
//            var accessCode = window.localStorage.getItem(AccessCodeKeyName) || '';
//                request.setRequestHeader('AccessCode', accessCode);
//        }
//        catch (e) { }
//        $('#loading').show();
//    },
//    complete: function (request, status) {
//        $('#loading').hide();
//        if (request.status == 401) {
//            alert('您无权访问此页面！'); location.href = 'login/login.html';
//        }
//        if (request.status == 402) {
//            alert('登录超时,请重新登录再操作！');
//            location.href = 'login/login.html';
//        }
//    }
//});