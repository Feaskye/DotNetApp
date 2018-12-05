app.controller('railCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.selRegion = {};
        service.getProvinceCityList(function (st) {
            if (st && st.ResObject != null) {
                $scope.selRegion = st.ResObject;
            }
        });
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                service.invokePost('Electronicfence/QueryElectronicfenceListByCondition', {
                    EnclosureSeq: $scope.seq || '',
                    EnclosureProvince: $scope.province || 0,
                    EnclosureCity: $scope.city || 0,
                    EnclosureDistrict: $scope.area || 0,
                    PageIndex: params.page()-1,
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

    //电子围栏列表查询省市区联动筛选条件
    $scope.onChgProvince = function () {
        if ($scope.province) {
            var cityData = $scope.selRegion.CityData;
            if (cityData) {
                var arrCity = [];
                angular.forEach(cityData, function (s, t) {
                    if (s && s.ProvinceId == $scope.province) {
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


    //选择市加载区
    $scope.onChgCity = function () {
        if ($scope.city) {
            var districtData = $scope.selRegion.District;
            if (districtData) {
                var district = [];
                angular.forEach(districtData, function (s, t) {
                    if (s && s.CityId == $scope.city) {
                        district.push(s);
                    }
                });
                $scope.districtData = district;
            }
        } else {
            $scope.districtData = [];
        }
    };

    //编辑电子围栏页面上省市区联动
    //$scope.onChgProvinceEdit = function () {
    //    if ($scope.electronicfenceData.EnclosureProvinceId) {
    //        var cityData = $scope.selRegion.CityData;
    //        if (cityData) {
    //            var arrCity = [];
    //            angular.forEach(cityData, function (s, t) {
    //                if (s && s.ProvinceId == $scope.electronicfenceData.EnclosureProvinceId) {
    //                    arrCity.push(s);
    //                }
    //            });
    //            $scope.cityData = arrCity;
    //        }
    //    }
    //    else {
    //        $scope.cityData = [];
    //    }
    //};

    ////选择市加载区
    //$scope.onChgCityEdit = function () {
    //    if ($scope.electronicfenceData.EnclosureCityId) {
    //        var districtData = $scope.selRegion.District;
    //        if (districtData) {
    //            var district = [];
    //            angular.forEach(districtData, function (s, t) {
    //                if (s && s.CityId == $scope.electronicfenceData.EnclosureCityId) {
    //                    district.push(s);
    //                }
    //            });
    //            $scope.districtData = district;
    //        }
    //    } else {
    //        $scope.districtData = [];
    //    }
    //};


    //查询电子围栏下的车辆详细信息
    $scope.onDetail = function (s) {
        if (s) {

            service.invokeGet('Electronicfence/GetElectronicfenceBicycle', { electronicfenceGuid: s.EnclosureGuid },
              function (result) {
                  if (result.ResObject != null){
                      $scope.electronicfenceBicycleData = result.ResObject.BicyCleList;
                      $scope.curElectronicfenceNo = s.EnclosureNumber || '';
                      $scope.cnt = result.ResObject.BicyCleCount || 0;
                      jQuery('#electronicfenceInfoModal').modal('show');
                  }
                  else {
                      $scope.curElectronicfenceNo = s.EnclosureNumber || '';
                      $scope.cnt = 0;
                      $scope.electronicfenceBicycleData = null;
                      jQuery('#electronicfenceInfoModal').modal('show');
                  }

              });
        }
    };

    //编辑页面跳转事件
    $scope.onEdit = function (s) {
        if (s) {
            $scope.isEdit = true;

            service.invokeGet('Electronicfence/QueryElectronicfenceDetail', { electronicfenceGuid: s.EnclosureGuid },
              function (result) {
                  $scope.electronicfenceData = result.ResObject;
                  //初始化电子围栏的状态下拉框值
                  $scope.StatusNames = [{ id: 1, name: '正常' }, { id: 0, name: '报警' }];

                  jQuery('#electronicfenceModal').modal('show');
              });
        }
    };


    //添加页面跳转事件
    $scope.onAdd = function () {
        $scope.electronicfenceData = {};
        //初始化电子围栏的状态下拉框值
        $scope.StatusNames = [{ id: 1, name: '正常' }, { id: 0, name: '报警' }];
        //$scope.selected = 1;

        //生成电子围栏编号
        //$scope.electronicfenceData.EnclosureSeq = "2wwwwwwwwwwww";
        service.invokeGet('Electronicfence/CreateElectronicFenCingNumber', {},
            function (result) {
                $scope.electronicfenceData.EnclosureSeq = result.ResObject;
                $scope.electronicfenceData.EnclosureState = $scope.StatusNames[0].id;
            });
        $scope.isEdit = false;
        jQuery('#electronicfenceModal').modal('show');
    };

    //编辑提交事件
    $scope.onUpdate = function () {
        service.invokePost('Electronicfence/AddOrEditElectronicfence', {
            EnclosureGuid: $scope.electronicfenceData.EnclosureGuid,       //电子围栏Guid
            EnclosureSeq: $scope.electronicfenceData.EnclosureSeq,        //电子围栏编号
            EnclosureLongitude: $scope.electronicfenceData.EnclosureLongitude,
            EnclosureLatitude: $scope.electronicfenceData.EnclosureLatitude,
            EnclosureState: $scope.electronicfenceData.EnclosureState,
            //省市区三级值
            //EnclosureProvinceId: $scope.electronicfenceData.EnclosureProvinceId,
            //EnclosureCityId: $scope.electronicfenceData.EnclosureCityId,
            //EnclosureDistrictId: $scope.electronicfenceData.EnclosureDistrictId,

            EnclosureRemark: $scope.electronicfenceData.EnclosureRemark,
            //DetailAddress: $scope.electronicfenceData.DetailAddress,
            CreateBy: $scope.electronicfenceData.CreateBy,

        },
               function (result) {
                   $scope.tableParams.reload();
                   if (result.IsSuccess && result.ResObject == true) {
                       $('#electronicfenceModal').modal('hide');
                       service.showSuccessAlert('保存成功!', '');
                   } else {
                       service.showWarningAlert(result.Message, '1');
                   }
               });
    }


    //删除电子围栏事件
    $scope.onDel = function (s) {
        if (s) {
            if (confirm('是否确认删除此电子围栏？')) {
                service.invokePost('Electronicfence/CancelElectronicfence', { EnclosureGuid: s.EnclosureGuid }, function (result) {
                    if (result.IsSuccess && result.ResObject) {
                        $scope.tableParams.reload();
                    }
                    else {
                        if (result.Message) {
                            service.showWarningAlert(result.Message, '1');
                            return false;
                        }
                    }
                });
                $event.stopPropagation();
                return false;
            }
        }
    };


    $scope.init();
});

app.controller('productionlockCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.preCnt = 0;
        $scope.queryLockData = { LockNumber: '', SupplierName: '', CustormName:'' };
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('Supplier/GetLockSupplierList', {
                    CustormName:$scope.queryLockData.CustormName, SupplierName: $scope.queryLockData.SupplierName,
                    LockNumber: $scope.queryLockData.LockNo, PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                    } else {
                        service.showWarningAlert(result.Message, "");
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

    $scope.init();
});