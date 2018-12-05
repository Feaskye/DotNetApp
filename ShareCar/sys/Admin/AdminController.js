// /admin/password页面
app.controller('adminPasswordCtr', function ($scope, service, $filter) {
    $scope.checkLength = service.CheckLength;

    // 保存按钮点击事件
    $scope.onSubmit = function () {

        service.invokePost('AdminAdmin/ChangePassword', {
            OldPassword: $scope.oldPassword,
            NewPassword: $scope.newPassword,
            OperatorGuid: $scope.adminGuid
        },
        function (result) {
            if (result.IsSuccess && result.ResObject == true) {
                service.showSuccessAlert('修改密码成功!', '');
                $scope.onReset();
            }
        });

    }

    // 重置按钮点击事件
    $scope.onReset = function () {
        $scope.oldPassword = "";
        $scope.newPassword = "";
        $scope.repeatPassword = "";
    };
    $scope.init();
});

// /admin/role页面
app.controller('adminRoleCtr', function ($scope, service, $filter, ngTableParams) {

    // 获取管理员角色列表
    service.getAdminRoleList(function (result) {

        $scope.adminRoles = result.ResObject;
    }, { accessCode: $scope.accessCode, IsIncludeSuperAdmin: false });

    $scope.init = function () {

        // 权限列表初始化
        // ********************
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 99999
        }, {
            total: 0

        });
        // ********************
    };

    // 角色改变事件
    $scope.changeDs = function () {

        if ($scope.adminRole == null) {
            $("#permissionList").hide();
        }
        else {
            $("#permissionList").show();
        }


        $scope.tableParams.settings({
            counts: [],
            getData: function ($defer, params) {
                service.invokePost('Admin/GetAllAdminPermission', {
                    roleGuid: $scope.adminRole,
                    accessCode: $scope.accessCode
                },
                function (result) {
                    if (result.IsSuccess) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        service.processFailedCode(result.MsgCode);
                        params.total(0);
                        $defer.resolve([]);
                    }
                })
            }
        });

        $scope.onQuery();
    }

    // 设置权限事件
    $scope.setPermission = function (item) {

        service.invokePost('Admin/SetAdminPermission', {
            roleGuid: $scope.adminRole,
            permissionGuid: item.Guid,
            hasPermission: item.HasPermission,
            accessCode: $scope.accessCode
        },
        function (result) {
            if (result.IsSuccess) {

            } else {
                service.processFailedCode(result.ResultCode);
            }
        });

    }

    // 列表查询
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.init();
});


app.controller('adminInfoCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.selRegion = {};
    $scope.cusPwd = service.curPwd;
    $scope.checkLength = service.CheckLength;

    service.getProvinceCityList(function (st) {
        if (st && st.ResObject != null) {
            $scope.selRegion = st.ResObject;
        }
    });

    service.getAdminRoleList(function (result) {
        $scope.adminRoles = result.ResObject;
    });

    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.init = function () {
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                service.invokeGet('AdminAdmin/GetAdminPageList', {
                    SearchUserName: $scope.adminName || '',
                    RoleName: $scope.adminRole || '',
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        service.processFailedCode(result.MsgCode, result.Message, '');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };

    $scope.onDel = function (s) {
        if (s) {
            if (confirm('是否确认删除用户？')) {
                service.invokePost('AdminAdmin/DeleteAdmin', { AdminGuid: s.AdminGuid, OperatorGuid: $scope.adminGuid }, function (result) {
                    if (result.IsSuccess && result.ResObject == true) {
                        service.showMessage("删除用户成功!");
                        $('#myModal').modal('hide');
                        $scope.tableParams.reload();
                    }
                });
            }
        }
    };

    $scope.onUpdate = function () {
        service.invokePost('AdminAdmin/AddOrUpdateAdmin', {
            AdminGuid: $scope.adminItem.AdminGuid,
            UserName: $scope.adminItem.UserName,
            Password: $scope.adminItem.Password,
            RoleName: $scope.adminItem.RoleName,
            AdminStatus: $scope.adminItem.AdminSt ? 1 : 0,
            Province: $scope.adminItem.Province || null,
            City: $scope.adminItem.City || null,
            Area: $scope.adminItem.Area || null,
            OperatorGuid: $scope.adminGuid
        },
               function (result) {
                   $scope.tableParams.reload();
                   if (result.IsSuccess && result.ResObject == true) {
                       $('#myModal').modal('hide');
                       service.showSuccessAlert('保存成功!', '');
                   } else {
                       service.showWarningAlert(result.Message, '1');
                   }
               });
    }

    $scope.onAdd = function () {
        $scope.adminItem = {};
        $('#myModal').modal('show');
    }

    $scope.onClickEdit = function (s) {
        if (s) {
            $scope.adminItem = JSON.parse(JSON.stringify(s));
            $scope.adminItem.Password = $scope.cusPwd;
            $scope.adminItem.AdminSt = s.Status == 1 ? true : false;
            $scope.onChgProvince();
            $scope.onChgCity();
            $('#myModal').modal('show');
        }
    }

    $scope.onChgProvince = function () {
        if ($scope.adminItem.Province) {
            var cityData = $scope.selRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.adminItem.Province) {
                        arrCity.push(s);
                    }
                });
                $scope.cityData = arrCity;
            }
        }
        else {
            $scope.cityData = [];
        }
    };

    $scope.onChgCity = function () {
        if ($scope.adminItem.City) {
            var districtData = $scope.selRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.adminItem.City) {
                        district.push(s);
                    }
                });
                $scope.districtData = district;
            }
        } else {
            $scope.districtData = [];
        }
    };

    $scope.init();
});

app.controller('permCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.preCnt = 0, $scope.isSelected = false;
        service.invokeGet("AdminAdmin/GetAdminRoleList", {}, function (a) { a && ($scope.adminRoles = a.ResObject) });
        $scope.onQuery();
    };

    $scope.onChkCol = function () {
        if (!$scope.adminRole) return;
        $scope.isSelected = !$scope.isSelected;
        angular.forEach($scope.qData, function (sk, t) {
            sk.Checked = $scope.isSelected;
        });
    };

    $scope.onQuery = function () {
        $scope.tableParams = new ngTableParams({
            count: -1,
        }, {
            counts: [],
            getData: function ($defer, params) {
                if (!$scope.adminRole) return;
                service.invokePost('AdminAdmin/QueryAdminRolePerm', {
                    RoleGuid: $scope.adminRole
                },
                 function (result) {
                     if (result.IsSuccess && result.ResObject != null) {
                         data = result.ResObject;
                         $scope.qData = data.List;
                         params.total(data.Total);
                         $defer.resolve(data.List);
                     } else {
                         service.processFailedCode(result.MsgCode, result.Message, '');
                         params.total(0);
                         $defer.resolve([]);
                     }
                 });
            }
        });
    };

    $scope.onChange = function () { $scope.tableParams.page(1), $scope.tableParams.reload() };

    $scope.onSave = function () { $scope.qData && service.invokePost("AdminAdmin/ModifyAdminRolePerm", { RoleGuid: $scope.adminRole, RolePerms: $scope.qData }, function (a) { a.IsSuccess ? service.showSuccessAlert("操作已成功", "") : service.processFailedCode(a.MsgCode, a.Message, "") }) };

    $scope.onSelCol = function (s) {
        if (s) {
            $scope.safeApply(function () {
                if (s.IsNode && s.ExistNode && !s.TMenuGuid && !s.Checked) angular.forEach($scope.qData, function (a) { s.MenuGuid == a.TMenuGuid && (a.Checked = s.Checked) }); else if (s.TMenuGuid && !s.IsNode) if (s.Checked) angular.forEach($scope.qData, function (a) { a.MenuGuid != s.TMenuGuid || a.Checked || (a.Checked = s.Checked) }); else { var tq = !1, col = null; angular.forEach($scope.qData, function (a) { a.TMenuGuid == s.TMenuGuid && a.Checked && (tq = a.Checked), a.MenuGuid == s.TMenuGuid && (col = a) }), col.Checked = tq }
            });
        }
    };

    $scope.init();
});

app.controller('roleCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.preCnt = 0;
        $scope.queryRoleData = { RoleName: '' };
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('AdminAdmin/GetAdminRolePageList', {
                    RoleName: $scope.queryRoleData.RoleName || '',
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        service.processFailedCode(result.MsgCode, result.Message, '');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };

    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.onAdd = function () {
        $scope.roleDetails = {};
        $('#roleModal').modal('show');
    };
    $scope.onEdit = function (s) {
        if (s) {
            $scope.roleDetails = s;
            $('#roleModal').modal('show');
        }
    };

    $scope.onAddorEdit = function () {
        service.invokePost('AdminAdmin/AddOrUpdateAdminRole', {
            AdminRoleGuid: $scope.roleDetails.AdminRoleGuid || null, RoleName: $scope.roleDetails.RoleName || '',OperatorGuid: $scope.adminGuid
        },
           function (result) {
               if (result.IsSuccess) {
                   service.showSuccessAlert('操作已成功', '');
                   $('#roleModal').modal('hide');
                   $scope.tableParams.reload();
               }
               else {
                   service.processFailedCode(result.MsgCode, result.Message, '');
               }
           });
    };

    $scope.onDel = function (s) {
        if (confirm('是否确认删除此角色？')) {
            service.invokePost('AdminAdmin/DeleteAdminRole', { AdminRoleGuid: s.AdminRoleGuid,OperatorGuid: $scope.adminGuid }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { service.processFailedCode(result.MsgCode, result.Message, '') }
            })
        }
    };

    $scope.init();
});