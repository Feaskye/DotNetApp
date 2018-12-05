app.controller('BreakdownCtr', function ($scope, service, $filter, ngTableParams, Excel, $timeout) {
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.init = function () {
        $scope.selRegion = {};
        //获取省市区数据
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
                service.invokeGet('AdminBreakdown/GetBreakdownListByCondition', {
                    BicyCleNumber: $scope.BicycleNumber || '',
                    ProvinceID: $scope.province || 0,
                    CityID: $scope.city || 0,
                    DistrictID: $scope.District || 0,
                    PageIndex: params.page() - 1,
                    PageSize: params.count(),
                    T:"Search"
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

    //选择省加载市事件
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

    //选择市加载区事件
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

    $scope.onUpdate = function () {
        service.invokePost('AdminBreakdown/AddOrUpdateBreakDown', {
            BreakdownGuid: $scope.BicycleItem.BreakdownGuid,
            BicyCleNumber: $scope.BicycleItem.BicyCleNumber,
            Longitude: $scope.BicycleItem.Longitude,
            Latitude: $scope.BicycleItem.Latitude,
            BreakTypeNameID : $scope.BicycleItem.BreakTypeNameID,
            GradeNameID : $scope.BicycleItem.GradeNameID,
            Status: $scope.BicycleItem.Status,
            Address: $scope.BicycleItem.Address,
            Remark: $scope.BicycleItem.Remark,
            OperatorGuid: "00000000-0000-0000-0000-000000000001",
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

    //导出Excel
    $scope.exportData = function () {
        //Excel打开后中文乱码添加如下字符串解决
        var exportContent = "\uFEFF";
        //alert(document.getElementById('exportable').innerHTML);
        var blob = new Blob([exportContent + document.getElementById('exportable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "故障维修信息表.xls");
    };



    //暂时废弃
    $scope.exportExcel = function (tableId) {

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 0
        }, {
            total: 0,
            getData: function ($defer, params) {
                service.invokeGet('AdminBreakdown/GetBreakdownListByCondition', {
                    BicyCleNumber: $scope.BicycleNumber || '',
                    ProvinceID: $scope.province || 0,
                    CityID: $scope.city || 0,
                    DistrictID: $scope.District || 0,
                    T: "excels",
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {

                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);

                        $scope.exportHref = Excel.tableToExcel(tableId, '故障维修信息表');

                        $timeout(function () {
                            location.href = $scope.exportHref;
                        }, 100); // trigger download

                    } else {
                        service.processFailedCode(result.MsgCode, result.Message, '');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    }

    //暂时废弃
    $scope.exportExcel222= function () {
    //function exportExcel() {
        return $http({
            url: '/AdminBreakdown/GetBreakdownListByCondition',
            method: "GET",
            headers: {
                'Content-type': 'application/json'
            },
            params: {
                BicyCleNumber: $scope.BicycleNumber || '',
                ProvinceID: $scope.province || 0,
                CityID: $scope.city || 0,
                DistrictID: $scope.District || 0,
                T: "excels",
                PageIndex: params.page() - 1,
                PageSize: params.count()
            },
            responseType: 'arraybuffer'
        }).success(function (data) {
            var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            var objectUrl = URL.createObjectURL(blob);
            var a = document.createElement('a');
            document.body.appendChild(a);
            a.setAttribute('style', 'display:none');
            a.setAttribute('href', objectUrl);
            a.setAttribute('download', "3333");
            a.click();
            URL.revokeObjectURL(objectUrl);
        });
    }



    //编辑
    $scope.onClickEdit = function (s) {
        if (s) {
            $scope.isEdit = true;

            service.invokeGet('AdminBreakdown/GetBreakDownByDetail', { BreakdownGuid: s.BreakdownGuid },
              function (result) {
                     $scope.BicycleItem = result.ResObject;
                      //初始化状态下拉框值
                     $scope.StatusNames = [{ id: "0", name: '故障' }, { id: "1", name: '已维修' }, { id: "2", name: '回仓库' }, { id: "3", name: '报废' }];
                      //初始化等级
                     $scope.GradeNameID = [{ id: "1", name: '第一等级' }, { id: "2", name: '第二等级' }, { id: "3", name: '第三等级' }];
                      //初始化故障类型
                      $scope.BreakTypeNameID = [{ id: "0", name: '无法开关锁' }, { id: "1", name: '无法骑行' }, { id: "2", name: '无法结算' }, { id: "3", name: '二维码受损' }, { id: "4", name: '其它问题' }];

                      jQuery('#myModal').modal('show');
              });
        }
    }

    $scope.onzoomPic = function (q,t) {
        if (q) {
            jQuery(q.target).zoomify();
            if (t) {
                jQuery("div.zoomed").removeClass("zoomify-shadow");
            }
        }
    };

    $scope.init();


});

app.controller('replogCtr', function ($scope, service, $filter, ngTableParams, $location, $routeParams) {
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
                service.invokeGet('AdminBreakdown/RepairRecord', {
                    BikeNumber: $scope.queryRepairData.bicyCleNumber || '',
                    UserName: $scope.queryRepairData.userNickName || '',
                    RepirtStartTime: jQuery('.repairSTime input').val() || null,
                    RepirtEndTime: jQuery('.repairETime input').val() || null,
                    PageIndex: params.page() - 1,
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

    $scope.defaultSetting = function () {
        var dtSet = $filter('parseDtToChar')(false);
        jQuery('.repairSTime,.repairETime').datetimepicker(dtSet.option);
        $scope.queryRepairData = { repairSTime: dtSet.start, repairETime: dtSet.curDt };
    };

    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.onzoomPic = function (a, b) { a && (jQuery(a.target).zoomify(), b && jQuery("div.zoomed").removeClass("zoomify-shadow")) };
    $scope.init();
});