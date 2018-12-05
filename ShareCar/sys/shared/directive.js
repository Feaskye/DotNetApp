app.directive('sidebar', function () {
    return {
        restrict: 'AE',
        templateUrl: 'shared/template/sidebar.html',
        replace: true
    };
});

app.directive('headerx', function () {
    return {
        restrict: 'AE',
        templateUrl: 'shared/template/head.html',
        replace: true      
    };
});

app.directive('footerx', function () {
    return {
        restrict: 'AE',
        templateUrl: 'shared/template/foot.html',
        replace: true
    };
});

app.directive('msgpanel', function () {
    return {
        restrict: 'AE',      
        replace: true,
        template: '<div ng-class="{true:\'alert-danger msg-div\', false: \'alert-success msg-div\'}[tipMsg!=\'操作已成功\']" ng-init="tipMsg=\'\'" ng-show="tipMsg !=null && tipMsg.length>0"><i ng-class="{true:\'icon-remove-sign\',false:\'icon-ok-sign\'}[tipMsg!=\'操作已成功\']"></i><span>{{tipMsg}}</span></div>'
    };
});

