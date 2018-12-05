//查询用户个人中心控制器
app.controller('PersonCenterCtr', function ($scope, service, $routeParams) {
    $scope.init = function () {
        $scope.tag = $routeParams.UserGuid;
        if ($scope.tag) {
            service.invokeGet('api/pcenter/GetUserInfoCenterByUserGuid', {
                UserGuid: $scope.tag || ''
            },
       function (result) {

           if (result.IsSuccess && result.ResObject != null) {
               $scope.qData = result.ResObject;
           } else { $scope.qData = {}; }
       });
        }
    };

    // 编辑按钮点击事件
    $scope.onClickEdit = function (item) {
        service.saveDataInLocaleSession("Personalinfo", JSON.stringify(item));
        location.href = 'index.html#/Personalinfo/' + item.UserGuid;
    };

    // 个人中心返回事件[暂时没用]
    $scope.onClickBack = function () {
        location.href = 'index.html#/BackUrl/';
    };

    //邀请好友
    $scope.onClickInvitation = function () {
        location.href = 'index.html#/PersonalCenter/';
    };
    


 
    $scope.init();
});

//查询用户个人信息控制器
app.controller('PersonalinfoCtr', function ($scope, service, $routeParams) {
    $scope.init = function () {
        $scope.tag = $routeParams.UserTag;
        if ($scope.tag) {
            service.invokeGet('api/pcenter/GetUserInfoDetailByUserGuid', {
                UserGuid: $scope.tag || ''
            },
       function (result) {
 
           if (result.IsSuccess && result.ResObject != null) {
               $scope.qData = result.ResObject;
           } else { $scope.qData = {}; }
       });
        }
    };

    // 编辑按钮点击事件
    $scope.onClickEdit = function (item) {
        service.saveDataInLocaleSession("UserInfo", JSON.stringify(item));
        location.href = 'index.html#/UserInfo/Update/' + item.UserGuid;
    };
 

    $scope.init();
});


//更新用户个人信息跳转和传参（传值）
app.controller('PersonalinfoUpdateCtr', function ($scope, service, $routeParams) {
 
    if ($routeParams.UserGuid != null) {
        $scope.UserInfo = JSON.parse(service.GetDataInLocaleSession("UserInfo"));
        if ($scope.UserInfo == null || $scope.UserInfo.UserGuid != $routeParams.UserGuid) {
            service.showMessage("页面已过期!");
            return;
        }
    }
    // 提交按钮点击事件
    $scope.onUpdate = function () {
        //var userGuid = $scope.UserInfo.UserGuid;
        service.invokePost('api/pcenter/UpdateUserNickOrPhone', {
            UserGuid: $scope.UserInfo.UserGuid,
            NickName: $scope.UserInfo.UserNickName,
            Phone: $scope.UserInfo.Phone
        },
        function (result) {
            if (result.IsSuccess && result.ResObject == true) 
            {
                //swal("操作成功", "修改用户信息成功", "success");

                //swal("操作成功", "修改用户信息成功")
                location.href = 'index.html#/Personalinfo/' + $scope.UserInfo.UserGuid;

            }
            else {
 
            }
        });
    }



    $scope.init();
});




















