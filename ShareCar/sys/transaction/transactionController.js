app.controller('curtransCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.queryTransactionData = {};
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokePost('UserInfo/GetTransactionList', {
                    Phone: $scope.queryTransactionData.phone || '',
                    DeviceNo: $scope.queryTransactionData.bikeNo || '',
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

    $scope.onreturnBike = function (s) {
        if (s) {
            $scope.transData = {};
            service.invokePost('UserInfo/GetUserByUserGuid', { UserInfoGuid: s.UserinfoGuid }, function (q) {
                if (q.IsSuccess) {
                    $scope.transData = q.ResObject;
                    $scope.transData.StartTime = $filter('dateFormatFilter')($scope.transData.StartTime);
                    $scope.transData.EndTime = $filter('dateFormatFilter')($scope.transData.EndTime);
                    $('#transactionModal').modal('show');
                }
            });
        }
    };
    $scope.onConfirm = function () {
        $scope.isclkConfirm = true;
        service.invokePost('UserInfo/ReturnCarByPhone', { Phone: $scope.transData.Phone }, function (q) {
            $scope.isclkConfirm = false;
            if (q.IsSuccess) {
                $('#transactionModal').modal('hide');
                $scope.tableParams.reload();
                if (q.Message) {
                    service.showSuccessAlert(q.Message, '');
                }
            }
        });
    };

    $scope.init();
});

app.controller('oldtransCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.qoldTransactionData = {};
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokePost('UserInfo/GetHistoryUserTransactionList', {
                    Phone: $scope.qoldTransactionData.phone || '',
                    DeviceNo: $scope.qoldTransactionData.bikeNo || '',
                    ReturnStatus: $scope.qoldTransactionData.transStatus || null,
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

    $scope.init();
});

app.controller('utransCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.chkSeq = 0;
        $scope.defaultSetting();
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                var qTag = $scope.chkSeq == 2 ? 'GetUserRefundDepositRecordList' : $scope.chkSeq == 1 ? 'GetUserAccountRechargeRecordList' : 'GetUserDepositRechargeRecordList';
                service.invokeGet('UserInfo/' + qTag, {
                    Phone: $scope.queryUserTrans.phone || '',
                    StartTime: jQuery('.transSTime input').val() || null,
                    EndTime: jQuery('.transETime input').val() || null,
                    RechargeType: $scope.queryUserTrans.transType || -1,
                    IsRecharge: $scope.queryUserTrans.transResult || -1,
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

    $scope.defaultSetting = function () {
        var dtSet = $filter('parseDtToChar')(false);
        jQuery('.transSTime,.transETime').datetimepicker(dtSet.option);
        $scope.queryUserTrans = { transSTime: dtSet.start, transETime: dtSet.curDt };
    };

    var btns = jQuery('.btns-div >input.col-btn');
    btns.on({
        click: function () {
            var tag = 'btn-info', ele = jQuery(this);
            if (!ele.hasClass(tag)) { ele.addClass(tag); }
            ele.siblings().removeClass(tag);
            $scope.chkSeq = btns.index(ele);
            $scope.defaultSetting();
            $scope.onQuery();
        }
    });

    $scope.init();
});

app.controller('rentchargeCtr', function ($scope, service, $filter, ngTableParams, $location) {
    $scope.init = function () {
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            count: -1,
        }, {
            counts: [],
            getData: function ($defer, params) {
                params.data.length = 0;
                service.invokeGet('ChargingRule/GetChargingRuleList', {},
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject || [];
                        $defer.resolve(data.List);
                    } else {
                        service.processFailedCode(result.MsgCode, result.Message, '');
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

    $scope.oneditDetail = function (s) {
        if (s) {
            $scope.rentalDetails = s;
            $('#rentalModal').modal('show');
        }
    };

    $scope.onremoveDetail = function (s) {
        if (s) {
            if (confirm('是否确认删除此车辆租借费用？')) {
                service.invokePost('ChargingRule/DeleteChargingRuleByServiceGuid', { RuleID: s.RuleID }, function (result) {
                    if (result.IsSuccess) {
                        $scope.tableParams.reload(); service.showSuccessAlert("操作已成功", "");
                    }
                    else { service.processFailedCode(result.MsgCode, result.Message, '') }
                })
            }
        }
    };

    $scope.onAdd = function () {
        $scope.rentalDetails = {};
        $('#rentalModal').modal('show');
    };

    $scope.onAddorEdit = function () {
        if (jQuery('#rentalBtn').attr('disabled') == 'disabled') return;
        service.invokePost('ChargingRule/AddChargingRule', $scope.rentalDetails,
              function (result) {
                  if (result.ResObject) {
                      $('#rentalModal').modal('hide');
                      $scope.tableParams.reload();
                      service.showSuccessAlert("操作已成功", "")
                  } else {
                      service.processFailedCode(result.MsgCode, result.Message, '');
                  }
              });
    };

    $scope.init();
});