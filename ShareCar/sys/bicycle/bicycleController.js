app.controller('BicycleCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    $scope.init = function () {

        $scope.Consumer = {};
        $scope.isSelected = false;
        $scope.selRegion = {};
        //获取省市区数据
        service.getProvinceCityList(function (st) {
            if (st && st.ResObject!= null) {
                $scope.selRegion = st.ResObject;
            }
        });

        ////获取客户名称
        //service.getCustomerInfoList(function (result) {
        //    $scope.customerList = result.ResObject;
        //    console.log(JSON.stringify(result.ResObject));

        //});

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {

                service.invokeGet('Biking/GetBicycleBaseListByCondition', {
                    BicyCleNumber: $scope.BicycleNumber || '',
                    ProvinceID: $scope.province || 0,
                    CityID: $scope.city || 0,
                    DistrictID: $scope.District || 0,
                    pageIndex: params.page() - 1,
                    pageSize: params.count()

                },
                function (result) {

                    if (result.IsSuccess && result.ResObject!=null) {
                        data = result.ResObject;
                        $scope.qData = data.List;
                        params.total(result.ResObject.Total);
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

    $scope.onChkCol = function () {

        $scope.isSelected = !$scope.isSelected;
        angular.forEach($scope.qData, function (sk, t) {
            sk.Checked = $scope.isSelected;
        });
    };

    $scope.tableParams1 = new ngTableParams({
        page: 1,
        count: 10
    }, {
        total: 0,
        getData: function ($defer, params) {

            service.invokeGet('Customer/GetCustomerInfoList', {
                CustomerName: $scope.Consumer.NickName || '',
                pageIndex: params.page() - 1,
                pageSize: params.count()

            },
            function (result) {

                if (result.IsSuccess && result.ResObject != null) {
                    data = result.ResObject;
                    params.total(result.ResObject.Total);
                    $defer.resolve(data.List);
                } else {

                    service.processFailedCode(result.MsgCode, result.Message, '');
                    params.total(0);
                    $defer.resolve([]);
                }
            });
        }
    });

    $scope.onConsumerQuery = function () {

        $scope.tableParams1.page(1);
        $scope.tableParams1.reload();


    };

    $scope.onSelect = function () {

        $('#myModalselect').modal('show');

    };


    $scope.Confirm = function (item) {

        $scope.CustomerID = item.CustomerID;
        $scope.Checkeds = [];
          angular.forEach($scope.qData, function (a) {

            if (a.Checked)
            {
                var Sig = {};
                Sig.LockGuid = a.LockGuid;
                Sig.BicycleGuid = a.BicycleGuid;
                Sig.CustomerID = $scope.CustomerID;
                Sig.OperatorGuid = $scope.adminGuid;
                $scope.Checkeds.push(Sig);

            }

        });

          if ($scope.Checkeds.length<=0)
          {


               service.showMessage("请选择要分配的车辆", '1');

          }

          service.invokePost('Biking/AddOrUpdateCustomerBicycleLock',{StrJson:JSON.stringify($scope.Checkeds) },


               function (result) {
                   $scope.tableParams.reload();
                   if (result.IsSuccess && result.ResObject== true) {

                       $('#myModalselect').modal('hide');
                       service.showSuccessAlert('保存成功!', '');
                   } else {
                       $('#myModalselect').modal('hide');
                       service.showWarningAlert(result.Message, '1');

                   }
               });


    }


    $scope.onSelCol = function (s) {
        if (s) {
            $scope.safeApply(function () {
                angular.forEach($scope.qData, function (a) { s.BicycleGuid == a.BicycleGuid && (a.Checked = s.Checked) });
            });
        }
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

    //删除事件
    $scope.onDel = function (s) {
        if (s) {
            if (confirm('是否确认删除车辆信息？')) {
                service.invokePost('Biking/DeleteBicycleBaseByBicycleGuid', { BicycleBaseGuid: s.BicycleBaseGuid }, function (result) {
                    if (result.IsSuccess && result.ResObject == true) {
                        service.showMessage("删除车辆信息成功!");
                        $('#myModal').modal('hide');
                        $scope.tableParams.reload();
                    }
                });
            }
        }
    };

    //还车异常处理
    $scope.onReturnCar = function () {
        var phone = $("#txtPhone").val();
        if (!phone) {
            return service.showWarningAlert("手机号码不能为空，请输入", ""), !1;

        } else {
            if (confirm('是否确认要处理还车异常信息吗？')) {
                service.invokePost('UserInfo/ReturnCarByPhone', { Phone: phone,OperatorGuid: $scope.adminGuid  }, function (result) {
                    if (result.IsSuccess && result.Message == "用户还车处理成功！") {
                        service.showMessage("用户还车处理成功！");
                    }
                });
            }
        }
    };

     $(function () {
            $("#range").change(function () {
                var v = $("#range").val();
                $.ajax({
                    type: "post",
                    url: "/Statistics/GetDateByRange",
                    data: { range:v},
                    async: false,
                    success: function (data) {
                        $("#beginDate").val(data.beginDate);
                        $("#endDate").val(data.endDate);
                    }
                });
            })
        });



     $scope.onUpdate = function () {
         //var adminGuid = $("#AdminGuid").val();
         //alert(adminGuid);
        service.invokePost('Biking/AddOrUpdateBicycleBase', {
            LockGuid: $scope.BicycleItem.LockGuid,
            //BicyCleNumber: $scope.BicycleItem.BicyCleNumber,
            DeviceNo: $scope.BicycleItem.LockNumber,
            //SecretKey: $scope.BicycleItem.SecretKey,
            //LockNumber: $scope.BicycleItem.LockNumber,
            //LockMac: "444",
            //Longitude: 0,
            //Latitude: 0,
            //ElectricQuantity: $scope.BicycleItem.ElectricQuantity,
            LockStatus: $scope.BicycleItem.LockStatus == "已关锁" ? 1 : 0,
            ElectricQuantityStatus: 1,     //电量充足
            //ElectricQuantity:100,
            //IsInElectronicFenCing:true, //默认在电子围栏内
            Address: $scope.BicycleItem.Address,
            Remark: $scope.BicycleItem.Remark
            //OperatorGuid:  $scope.BicycleItem.AdminGuid

        },
               function (result) {
                   $scope.tableParams.reload();
                   if (result.IsSuccess && result.ResObject== true) {
                       $('#myModal').modal('hide');
                       service.showSuccessAlert('保存成功!', '');
                   } else {
                       service.showWarningAlert(result.Message, '1');
                   }
               });
    }

     $scope.onAdd = function () {
         //var adminguid = $scope.adminGuid;

        $scope.BicycleItem = {};
        $scope.isEdit = false;
        //设置默认值
        //$scope.BicycleItem.BicycleTypeName = '通用型';
        $scope.BicycleItem.LockStatus = '已关锁';
        //$scope.BicycleItem.AdminGuid = adminguid;
        $('#myModal').modal('show');
    }

    // 编辑按钮点击事件
    //$scope.onClickEdit = function (item) {
    //    service.saveDataInLocaleSession("bicycle", JSON.stringify(item));
    //    location.href = 'index.html#/bicycle/Update/' + item.BicycleBaseGuid;
    //};

    //编辑
    $scope.onClickEdit = function (s) {
        if (s) {
            $scope.isEdit = true;
            $scope.BicycleItem = JSON.parse(JSON.stringify(s));
            //$scope.adminItem.Password = $scope.cusPwd;
            //$scope.adminItem.AdminSt = s.Status == 0;
            $('#myModal').modal('show');
        }
    }

    //查看
    $scope.onDetail = function (s) {
        if (s)
        {
            $scope.BicycleItem = JSON.parse(JSON.stringify(s));
            $('#myModalDetail').modal('show');
        }
    }

    $scope.init();

});



//生成车辆编号控制器
app.controller('BicycleNumberCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.init = function () {
        $scope.defaultSetting();

        $scope.preCnt = 0;

        //供应商
        $scope.querySupplierData = { SupplierNo: '', SupplierName: '' };

        //获取客户名称
        service.getCustomerInfoList(function (result) {
            $scope.customerList = result.ResObject;
        });


        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokeGet('Biking/GetBicycleNumberListByCondition', {
                    StartTime: jQuery('.userSTime input').val() || null,
                    EndTime: jQuery('.userETime input').val() || null,
                    CustomerName: $scope.qcusName,
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

    $scope.defaultSetting = function () {
        $scope.BicycleItem = {};

        var dtSet = $filter('parseDtToChar')(false);
        jQuery('.userSTime,.userETime').datetimepicker(dtSet.option);
        $scope.queryUserData = { userSTime: dtSet.start, userETime: dtSet.curDt };

        $scope.BicycleItem.txtMinValue = 111111;
        $scope.BicycleItem.txtMaxValue = 1111111111;
    };

    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    //批量生成车辆编号列表
    $scope.ProduceBicycleNumber = function () {
        var txtMinValue = $("#txtMinValue").val();
        var txtMaxValue = $("#txtMaxValue").val();

        var txtCount = $("#txtCount").val();
        if (!txtCount) {
            return service.showWarningAlert("请输入生成的个数", ""), !1;
        }

        if (!txtMinValue || !txtMaxValue) {
            return service.showWarningAlert("请输入区间最小数和区间最大数!", ""), !1;
        } else {
            if (confirm('是否确认要批量生成车辆编号信息吗？')) {
                service.invokePost('Biking/GetBicycleProduceBicNumber', { minValue: txtMinValue, maxValue: txtMaxValue, num: txtCount, CustomerID: $scope.CustomerName||0 }, function (result) {
                    if (result.IsSuccess && result.Message == "生成自行车编号成功！") {
                        $scope.tableParams.reload();
                        return service.showSuccessAlert("批量生成车辆编号成功!", ""), !1;
                    } else {
                        return service.showWarningAlert("批量生成车辆编号出现异常!", ""), !1;
                    }
                });
            }
        }
    };


    //导出Excel
    $scope.exportData = function () {
        //Excel打开后中文乱码添加如下字符串解决
        var exportContent = "\uFEFF";
        //alert(document.getElementById('exportable').innerHTML);
        var blob = new Blob([exportContent + document.getElementById('exportable').innerHTML], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, "批量生成车辆编号信息表.xls");
    };


    $scope.init();
});