app.controller('loginCtr', function ($scope, service) {
    $scope.init = function () {
        $scope.user = {};
        $scope.errorMessage = '';
        $scope.onNewDCode();
        $("body").keydown(function () { 13 == window.event.keyCode && (event.returnValue = !1, event.cancel = !0, $scope.login()) });
    };

    $scope.login = function () {
        $scope.chkError = false;
        service.invokePost('AdminWebLogin/CheckAdminLogin', { username: $scope.user.username, password: $scope.user.password, validateCode: $scope.user.validateCode },
           function (result) {


               $scope.safeApply(function () {

                   if (result.MsgCode == '0') {

                       service.saveDataInLocaleSession(AccessCodeKeyName, result.ResObject.AccessCode);
                       service.saveDataInLocaleSession(AdminGuidKeyName, result.ResObject.AdminGuid);
                       service.saveDataInLocaleSession(UserNameKeyName, $scope.user.username);
                       service.saveDataInLocaleSession(RoleNameKeyName, result.ResObject.RoleName);
                       service.saveDataInLocaleSession(service.PermKeyName, result.ResObject.PermUrls);
                       service.saveDataInLocaleSession(service.MenuKeyName, result.ResObject.Menus);

 
                       location.href = '../index.html#/main';
                   }
                   else {
                       $scope.chkError = true;
                       $scope.errorMessage = result.Message;
                   }
               });
           });
    };

    //$scope.login = function () {

    //    $scope.chkError = false;
    //    service.invokePost('AdminWebLogin/CheckAdminLogin', { username: $scope.user.username, password: $scope.user.password, validateCode: $scope.user.validateCode },
    //       function (result) {
    //           var res = result;

    //           $scope.safeApply(function () {
    //               if (result.MsgCode == '0') {
    //                   service.saveDataInLocaleSession(AccessCodeKeyName, result.ResObject.AccessCode);
    //                   service.saveDataInLocaleSession(UserNameKeyName, $scope.user.username);
    //                   service.saveDataInLocaleSession(RoleNameKeyName, result.ResObject.RoleName);
    //                   service.saveDataInLocaleSession(AdminGuidKeyName, result.ResObject.AdminGuid);

    //                   location.href = '../index.html#/main';
    //               }
    //               else if (result.MsgCode == "146") {
    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //               else if (result.MsgCode == '108') {
    //                   alert(result.MsgCode);
    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //               else if (result.MsgCode == "104") {

    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //               else if (result.MsgCode == "104") {

    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //               else if (result.MsgCode == "105") {

    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //               else {
    //                   $scope.chkError = true;
    //                   $scope.errorMessage = result.Message;
    //               }
    //           });
    //       });
    //};

    $scope.onNewDCode = function () {

        var url = ULR + 'AdminWebLogin/VerifyImage?r=' + Math.random();
        $('#VerifyImage').attr("src", url);
    }

    $scope.resetData = function () {
        $scope.user.username = "";
        $scope.user.password = "";
        $scope.user.validateCode = "";
    };

    $scope.init();
});