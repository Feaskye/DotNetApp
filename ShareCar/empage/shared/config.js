URL_DEV = 'http://localhost:5325/';
URL_PRO = 'http://192.168.2.153/';
ULR = URL_DEV;

app.constant('config', {
    serviceUrl: ULR
});

app.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|#|chrome-extension):/);
}]);

$.ajaxSetup({
    beforeSend: function (request) {
        //$('#loading').show();
    },
    complete: function (request, status) {
        //$('#loading').hide();
    }
});