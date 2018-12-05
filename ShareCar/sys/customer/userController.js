app.controller('userCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.defaultSetting();
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('UserInfo/GetUserInfoByCondition', {
                    Phone: $scope.queryUserData.phone || '',
                    UserNickName: $scope.queryUserData.userNickName || '',
                    StartTime: jQuery('.userSTime input').val() || null,
                    EndTime: jQuery('.userETime input').val() || null,
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        service.showWarningAlert(result.Message ? result.Message : '未查询到相关结果', '');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };

    $scope.defaultSetting = function () {
        var dtSet = $filter('parseDtToChar')(false);
        jQuery('.userSTime,.userETime').datetimepicker(dtSet.option);
        $scope.queryUserData = { userSTime: dtSet.start, userETime: dtSet.curDt };
    };

    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };
    $scope.onToDetail = function (s) {
        if (s) {
            service.invokeGet('UserInfo/GetUserByUserGuid', { UserGuid: s.UserGuid },
            function (result) {
                if (result.ResObject != null) {
                    $scope.userDetails = result.ResObject;
                    $('#userInfoModal').modal('show');
                }
                else {
                    $scope.userDetails = {};
                }
            });
        }
    };
    $scope.onConfirm = function () {
        service.invokePost('UserInfo/EditUserInfoByUserGuid', {
            UserGuid: $scope.userDetails.UserGuid, UserNickName: $scope.userDetails.UserNickName || '',
            UserName: $scope.userDetails.UserName || '', Phone: $scope.userDetails.Phone || ''
        },
           function (result) {
               if (result.IsSuccess) {
                   service.showSuccessAlert('操作已成功', '');
                   $('#userInfoModal').modal('hide');
                   $scope.tableParams.reload();
               }
               else {
                   service.processFailedCode(result.MsgCode, result.Message, '');
               }
           });
    };

    $scope.onClickLocking = function (s) {
        s && service.invokePost("UserInfo/LockUserStatusByUserGuid", { UserGuid: s.UserGuid }, function (a) {
            a.IsSuccess ? (service.showSuccessAlert("操作已成功", ""),
                $scope.tableParams.reload()) : service.processFailedCode(a.MsgCode, a.Message, "")
        });
    };
    $scope.init();
});

app.controller('supplierCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.preCnt = 0;
        $scope.querySupplierData = { SupplierNo: '', SupplierName: '' };
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('Supplier/GetSupplierInfoListByCondition', {
                    SupplierNumber: $scope.querySupplierData.SupplierNo || '',
                    SupplierName: $scope.querySupplierData.SupplierName || '',
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
        $scope.supplierDetails = {};
        $('#supplierModal').modal('show');
    };
    $scope.onEdit = function (s) {
        if (s) {
            $scope.supplierDetails = s;
            $('#supplierModal').modal('show');
        }
    };
    $scope.onAddorEdit = function () {
        service.invokePost('Supplier/AddOrUpdateSupplier', {
            SupplierID: $scope.supplierDetails.SupplierID || null, SupplierNumber: $scope.supplierDetails.SupplierNumber || '',
            SupplierName: $scope.supplierDetails.SupplierName || '', Remark: $scope.supplierDetails.Remark || ''
        },
           function (result) {
               if (result.IsSuccess) {
                   service.showSuccessAlert('操作已成功', '');
                   $('#supplierModal').modal('hide');
                   $scope.tableParams.reload();
               }
               else {
                   service.processFailedCode(result.MsgCode, result.Message, '');
               }
           });
    };

    $scope.onDel = function (s) {
        if (confirm('是否确认删除此供应商？')) {
            service.invokePost('Supplier/DeleteSupplierBySupplierID', { SupplierID: s.SupplierID }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { service.processFailedCode(result.MsgCode, result.Message, '') }
            })
        }
    };
    $scope.init();
});

app.controller('consumerCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.dlgRegion = {};
        service.getProvinceCityList(function (a) { a && null != a.ResObject && ($scope.dlgRegion = a.ResObject) });
        $scope.preCnt = 0;
        $scope.queryConsumerData = { ConsumerName: '' };
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('Customer/GetCustomerInfoList', {
                    CustomerName: $scope.queryConsumerData.ConsumerName || '',
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

    $scope.onQuery = function () { $scope.tableParams.page(1), $scope.tableParams.reload() };
    $scope.onAdd = function () {
        $scope.consumerDetails = {};
        $('#consumerModal').modal('show');
    };
    $scope.onEdit = function (a) { a && ($scope.consumerDetails = a, $scope.onChgqProvince(), $scope.onChgqCity(), $scope.consumerDetails.IsLock = $scope.consumerDetails.IsLock.toString(), $("#consumerModal").modal("show")) };
    $scope.onAddorEdit = function () {
        service.invokePost('Customer/AddOrUpdateCustomer', {
            CustomerID: $scope.consumerDetails.CustomerID || null,
            CustomerName: $scope.consumerDetails.CustomerName || '',
            Phone: $scope.consumerDetails.Phone || '',
            Remark: $scope.consumerDetails.Remark || '',
            OperatorGuid: $scope.adminGuid,
            IsLock: $scope.consumerDetails.IsLock,
            ProvinceId: $scope.consumerDetails.ProvinceId,
            CityId: $scope.consumerDetails.CityId,
            DistinctId: $scope.consumerDetails.DistinctId,
            Address: $scope.consumerDetails.Address
        },
           function (result) {
               if (result.IsSuccess) {
                   service.showSuccessAlert('操作已成功', '');
                   $('#consumerModal').modal('hide');
                   $scope.tableParams.reload();
               }
               else {
                   service.processFailedCode(result.MsgCode, result.Message, '');
               }
           });
    };

    $scope.onDel = function (s) {
        if (s.CustomerName == "上海摩亭总部")
        {
            return service.showWarningAlert("您是总部不能删除", ""), !1;
        }
        if (confirm('是否确认删除此客户？')) {
            service.invokePost('Customer/DeleteCustomerByCustomerID', { CustomerID: s.CustomerID }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { service.processFailedCode(result.MsgCode, result.Message, '') }
            })
        }
    };

 
    $scope.onChgqProvince = function () {
        if ($scope.consumerDetails.ProvinceId) {
            var cityData = $scope.dlgRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.consumerDetails.ProvinceId) {
                        arrCity.push(s);
                    }
                });
                $scope.qcityData = arrCity;
            }
        }
        else {
            $scope.qcityData = [];
        }
    };
    $scope.onChgqCity = function () {
        if ($scope.consumerDetails.CityId) {
            var districtData = $scope.dlgRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.consumerDetails.CityId) {
                        district.push(s);
                    }
                });
                $scope.qdistrictData = district;
            }
        } else {
            $scope.qdistrictData = [];
        }
    };
    $scope.init();
});

app.controller('servicemanCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.selRegion = $scope.dlgRegion = {};
        service.getProvinceCityList(function (a) { a && null != a.ResObject && ($scope.selRegion = $scope.dlgRegion = a.ResObject) });
        $scope.preCnt = 0;
        $scope.queryServicePersonData = { Phone: '', ServicemanName: '' };
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('ServicePerson/GetServicePersonInfList', {
                    Phone: $scope.queryServicePersonData.Phone || '',
                    UserName: $scope.queryServicePersonData.ServicemanName || '',
                    ProvinceId: $scope.queryServicePersonData.Province || 0,
                    CityId: $scope.queryServicePersonData.City || 0,
                    DistinctId: $scope.queryServicePersonData.District || 0,
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
        $scope.servicemanDetails = { uStatus: '', uSex: '' };
        $('#servicemanModal').modal('show');
    };
    $scope.onEdit = function (s) {
        if (s) {
            $scope.servicemanDetails = s;
            s.Password = service.curPwd;
            $scope.servicemanDetails.Status = s.Status.toString();
            $scope.servicemanDetails.Sex = s.Sex != null || s.Sex != undefined ? jQuery.trim(s.Sex.toString()) : '';
            $scope.onChgqProvince();
            $scope.onChgqCity();
            $('#servicemanModal').modal('show');
        }
    };
    $scope.onAddorEdit = function () {
        service.invokePost('ServicePerson/AddServicePersonInfo', {
            ServicePersonID: $scope.servicemanDetails.ServicePersonID || null, UserName: $scope.servicemanDetails.UserName || '',
            RealName: $scope.servicemanDetails.RealName || '', Phone: $scope.servicemanDetails.Phone || '',
            Status: $scope.servicemanDetails.Status || '', Password: $scope.servicemanDetails.Password || '',
            Sex: $scope.servicemanDetails.Sex || '',
            ProvinceID: $scope.servicemanDetails.ProvinceID || '', CityID: $scope.servicemanDetails.CityID || '', DistinctId: $scope.servicemanDetails.DistrictID || '',
            Address: $scope.servicemanDetails.Address || '', Remark: $scope.servicemanDetails.Remark || ''
        },
           function (result) {
               if (result.IsSuccess) {
                   service.showSuccessAlert('操作已成功', '');
                   $('#servicemanModal').modal('hide');
                   $scope.tableParams.reload();
               }
               else {
                   service.processFailedCode(result.MsgCode, result.Message, '');
               }
           });
    };

    $scope.onDel = function (s) {
        if (confirm('是否确认删除此维修人员？')) {
            service.invokePost('ServicePerson/DeleteServicePersonByServiceGuid', { ServicePersonID: s.ServicePersonID }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { service.processFailedCode(result.MsgCode, result.Message, '') }
            })
        }
    };

    $scope.onChgProvince = function () {
        if ($scope.queryServicePersonData.Province) {
            var cityData = $scope.selRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.queryServicePersonData.Province) {
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
        if ($scope.queryServicePersonData.City) {
            var districtData = $scope.selRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.queryServicePersonData.City) {
                        district.push(s);
                    }
                });
                $scope.districtData = district;
            }
        } else {
            $scope.districtData = [];
        }
    };

    $scope.onChgqProvince = function () {
        if ($scope.servicemanDetails.ProvinceID) {
            var cityData = $scope.dlgRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.servicemanDetails.ProvinceID) {
                        arrCity.push(s);
                    }
                });
                $scope.qcityData = arrCity;
            }
        }
        else {
            $scope.qcityData = [];
        }
    };

    $scope.onChgqCity = function () {
        if ($scope.servicemanDetails.CityID) {
            var districtData = $scope.dlgRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.servicemanDetails.CityID) {
                        district.push(s);
                    }
                });
                $scope.qdistrictData = district;
            }
        } else {
            $scope.qdistrictData = [];
        }
    };

    $scope.init();
});

app.controller('cusbicyclerigCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.selRegion = $scope.dlgRegion = {};
        service.getProvinceCityList(function (a) { a && null != a.ResObject && ($scope.selRegion = $scope.dlgRegion = a.ResObject) });
        $scope.preCnt = 0;
        $scope.queryPinlessData = { ConsumerName: '', LockNo: '', BicycleNo: '' };
        $scope.defaultSetting();
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('Biking/CustomerBicycleLockList', {
                    LockNumber: $scope.queryPinlessData.LockNo || '',
                    BicycleNumber: $scope.queryPinlessData.BicycleNo || '',
                    CustomerName: $scope.queryPinlessData.ConsumerName || '',
                    StartTime: jQuery('.pinlessSTime input').val() || null,
                    EndTime: jQuery('.pinlessETime input').val() || null,
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        if (result.Message) { service.showWarningAlert(result.Message, ""); }
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };
    $scope.defaultSetting = function (a) { var b = $filter("parseDtToChar")(!1); jQuery(".pinlessSTime,.pinlessETime").datetimepicker(b.option), $scope.queryPinlessData.pinlessSTime = b.start, $scope.queryPinlessData.pinlessETime = b.curDt, a && (jQuery(".pinlessSTime input").val(b.start), jQuery(".pinlessETime input").val(b.curDt)) };
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.onDel = function (s) {
        if (confirm('是否确认解绑车辆？')) {
            var q = { DistributionGuid: s.DistributionGuid, LockGuid: s.LockGuid, BicycleGuid: s.BicycleGuid, CustomerID: s.CustomerID, Status: 0, Remark: '', OperatorGuid: $scope.adminGuid }, sk = JSON.stringify([q]);
            service.invokePost('Biking/AddOrUpdateCustomerBicycleLock', { StrJson: sk }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { if (result.Message) { service.showWarningAlert(result.Message, ""); } }
            })
        }
    };

    //远程升级锁程序包
    $scope.RemoteBicyleLockUpgrade = function (s) {
        //alert( s.LockGuid+","+s.BicycleGuid+","+s.CustomerID);
        if (confirm('是否确认远程升级锁程序包？')) {
            service.invokePost('Biking/SendUpgradeBicLockProgram', { LockGuid: s.LockGuid, BicycleGuid: s.BicycleGuid, CustomerID: s.CustomerID }, function (result) {
                if (result.IsSuccess) {
                    $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                }
                else { service.processFailedCode(result.MsgCode, result.Message, '') }
            })
        }
    };


    $scope.onChgProvince = function () {
        if ($scope.queryPinlessData.Province) {
            var cityData = $scope.selRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.queryPinlessData.Province) {
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
        if ($scope.queryPinlessData.City) {
            var districtData = $scope.selRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.queryPinlessData.City) {
                        district.push(s);
                    }
                });
                $scope.districtData = district;
            }
        } else {
            $scope.districtData = [];
        }
    };

    $scope.onChgqProvince = function () {
        if ($scope.queryPinlessDetails.ProvinceID) {
            var cityData = $scope.dlgRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.queryPinlessDetails.ProvinceID) {
                        arrCity.push(s);
                    }
                });
                $scope.qcityData = arrCity;
            }
        }
        else {
            $scope.qcityData = [];
        }
    };

    $scope.onChgqCity = function () {
        if ($scope.queryPinlessDetails.CityID) {
            var districtData = $scope.dlgRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.queryPinlessDetails.CityID) {
                        district.push(s);
                    }
                });
                $scope.qdistrictData = district;
            }
        } else {
            $scope.qdistrictData = [];
        }
    };

    $scope.init();
});