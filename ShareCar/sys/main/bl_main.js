app.controller('rootCtr', function ($scope, service) {
    $scope.init = function () {
        if (service.GetDataInLocaleSession(AccessCodeKeyName) == null) {
            location.href = 'login/login.html';
        }
        $scope.islogoPage = true;

        $scope.userName = service.GetDataInLocaleSession(UserNameKeyName);      //用户名
        $scope.accessCode = service.GetDataInLocaleSession(AccessCodeKeyName);  //AccessCode
        $scope.roleName = service.GetDataInLocaleSession(RoleNameKeyName);   //角色名称
        $scope.adminGuid = service.GetDataInLocaleSession(AdminGuidKeyName); //当前系统用户Guid
        $scope.menus = service.GetDataInLocaleSession(service.MenuKeyName);

        $scope.loadPreMenu();
    }

    $scope.loadPreMenu = function () {
        var s0 = service.GetDataInLocaleSession(service.chkMenuNode), s1 = service.GetDataInLocaleSession(service.navigationTxt);
        if (s0 && s1) {
            var eles = jQuery("#dashboard-menu >li"), txt0 = decodeURIComponent(s0), txt1 = decodeURIComponent(s1), q = '';
            if (txt0 && txt1) {
                jQuery("ul.breadcrumb").html(txt1);
                jQuery(eles).each(function (s, t) {
                    q = jQuery(t).find(".dashboard-menu-link >span").text();
                    if (q == txt0) {
                        eles.removeClass("active").find(".pointer").addClass("display-close");
                        eles.find(".dashboard-menu-link").removeClass("side-cols-active");
                        jQuery(t).addClass("active").find(".dashboard-menu-link").addClass("side-cols-active");
                        jQuery(t).find(".pointer").removeClass("display-close");
                        var sTag = jQuery(t).find(".submenu"), tq = txt1.match(/\<\/li\>/ig), chevronBtn = jQuery(t).find(".side-trans-btn");
                        if (sTag.length > 0) {
                            if (tq) {
                                if (tq.length == 3) {
                                    sTag.show(); chevronBtn.removeClass("icon-chevron-left");
                                    if (!chevronBtn.hasClass("icon-chevron-down")) {
                                        chevronBtn.addClass("icon-chevron-down");
                                    }
                                }
                            }
                        }
                        return false;
                    }
                });
            }
        }
    };

    // 管理员登出
    $scope.logout = function () {
        service.logout();
    }

    // 权限检验
    $scope.CheckPermission = function (permissionString) {
        return $scope.permissions.indexOf(permissionString) != -1;
    };

    $scope.init();
});

app.controller('welcomeCtr', function ($scope, service) {
    $scope.init = function () {
        var sk = jQuery("ul.breadcrumb li"), st0 = sk.eq(1), st1 = sk.eq(2), tag = "node-display-none"; if (!st0.hasClass(tag)) { st0.addClass(tag) } if (!st1.hasClass(tag)) { st1.addClass(tag) };
        $scope.isLoad = $scope.isLoad03 = false;
        $scope.depositDetails = [];
        service.invokeGet("Statistics/GetStatisticsData", {}, function (a) {
            var b = a.ResObject; b && b.dataList &&
                b.dataList.length && ($scope.portletDetails = b.dataList[0], $scope.isLoad = !0)
        });
        $scope.onQdeposit();
        $scope.breakdownTip = setInterval(getbreakdownLog, 50 * 1000);
    }

    function getbreakdownLog() {
        service.invokeGet("AdminBreakdown/Getnotification", {}, function (a) {
            if (a && a.ResObject) {
                var sk = a.ResObject, cnt = sk.Total;
                if (cnt) {
                    var t0 = '提醒', txt = '您有' + cnt + '条新的故障信息，请及时处理！';
                    jQuery.gritter.add({
                        title: t0,
                        text: '<a href="#/breakdown" title="查看详情" target="_blank">' + txt + '</a>',
                        image: 'images/envelope.png',
                        sticky: false,
                        time: 5000,
                        speed: 500,
                        position: 'bottom-right',
                        class_name: 'gritter-success'
                    });
                }
            }
        });
    }

    $scope.onQdeposit = function (s) {
        service.invokeGet("Statistics/GetStatisticsDataByCondition", { TypeID: s || 1 }, function (a) {
            $scope.safeApply(function () {
                var b = a.ResObject; b && b.ConditionList &&
                b.ConditionList.length && ($scope.depositDetails = b.ConditionList, $scope.isLoad03 = !0); $scope.isLoad03 = true;
            });
        });
    };

    $scope.$watch('depositDetails', function (s, t) {
        if (s && s.length && s != t) {
            $scope.qchart(s);
        }
    });

    var eles = jQuery("div.dashboard_03-tag > ul > li");
    eles.on({
        click: function () {
            var index = eles.index(jQuery(this));
            var clsName = "dashboard_03-tb-activate", clsName0 = "dashboard-menu-link";
            eles.find("a").removeClass(clsName).addClass(clsName0).find("span").removeClass(clsName);
            jQuery(this).find("a").addClass(clsName).find("span").addClass(clsName);
            $scope.onQdeposit(index + 1);
        }
    });

    $scope.qchart = function (s) {
        var qdata = s[0];
        var chart = document.getElementById('dashboard03_deposit');
        var chartData = echarts.init(chart);
        option = {
            color: ['#c23531'],
            title: {
                text: '',
                subtext: '',
                sublink: ''
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                },
                formatter: function (params) {
                    var tar = params[1];
                    return tar.name + '<br/>' + tar.seriesName + tar.value + " 元";
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: {
                type: 'category',
                splitLine: { show: false },
                data: ['总收入', '押金总额', '总余额', '押金充值总额', '余额充值总额', '退押金总额']
            },
            yAxis: {
                type: 'value'
            },
            series: [
                 {
                     name: '辅助',
                     type: 'bar',
                     stack: '总量',
                     data: [0, 0, 0, 0, 0, 0]
                 },
                 {
                     name:'',
                     type:'bar',
                     stack: '总量',
                     label: {
                         normal: {
                             show: true,
                             position: 'inside'
                         }
                     },
                     barWidth: '60%',
                     data: [qdata.TotalAAmount, qdata.TotalAccountAmount, qdata.TotalAmounts, qdata.TotalDAmount, qdata.TotalDepositAmount, qdata.TotalReturnAmount]
                 }
            ]
        };
        chartData.setOption(option);
        function eConsole(param) { }
        chartData.on("click", eConsole);
    };
    $scope.init();
});

app.controller('gpageCtr', function ($scope, service, $filter, ngTableParams) {
    var ele = jQuery('#gpagePanel'), cls = "display-close", sk;
    $scope.init = function () {
        $scope.defaultSetting();
        $scope.curSelPics = null;
        $scope.isAdd = false;
        $scope.maxNum = 5;
        $scope.picCnt = 0;
        $scope.loadPic();
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokePost('Upload/QueryGuidePagePhoto', {
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                        $scope.maxNum = data.QNum;
                        $scope.picCnt = data.Total;
                    } else {
                        service.showWarningAlert(result.Message ? result.Message : '暂无引导页', '1');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };
    $scope.loadPic = function () {
        sk = $("#pic").zyUpload({
            width: "650px",
            height: "100%",
            itemWidth: "120px",
            itemHeight: "100px",
            url: "/upload/uploadAction",
            multiple: false,
            dragDrop: true,
            del: true,
            finishDel: true,
            onSelect: function (files, allFiles) {
                if ($scope.curSelPics = allFiles, $scope.curSelPics && $scope.curSelPics.length > 1) {
                    return service.showWarningAlert("每次保存只能上传一张引导页图片", ""), !1;
                }
            },
            onDelete: function (file, files) { $scope.curSelPics = files; }
        });
    };
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };
    $scope.onAdd = function () { return $scope.picCnt >= $scope.maxNum ? (service.showWarningAlert("最多只能上传" + $scope.maxNum + "张引导页图片", "1"), void 0) : (ele.removeClass(cls), void 0) };
    $scope.defaultSetting = function (a) { var b = $filter("parseDtToChar")(!1); jQuery(".picSTime,.picETime").datetimepicker(b.option), $scope.qPicData = { picSTime: b.start, picETime: b.curDt }, a && (jQuery(".picSTime input").val(b.start), jQuery(".picETime input").val(b.curDt)) };
    $scope.onSave = function () {
        if (!$scope.curSelPics || 0 == $scope.curSelPics.length) return service.showWarningAlert("请先选择一张引导页图片", ""), void 0;
        if ($scope.curSelPics.length > 1) return service.showWarningAlert("每次只能上传一张引导页图片", ""), void 0;
        if ($scope.isNullOrEmpty($scope.qPicData.picTag)) return service.showWarningAlert("引导页图片的标题不能为空", ""), void 0;
        var formData = new FormData; formData.append("picLoc", $scope.curSelPics[0]), formData.append("picTag", $scope.qPicData.picTag || ""), formData.append("sDt", jQuery(".picSTime input").val() || null), formData.append("eDt", jQuery(".picETime input").val() || null), formData.append("seq", $scope.qPicData.Seq || 0);
        $('#loading').show();
        jQuery.ajax({
            url: ULR + "Upload/NewGuidePagePhoto", type: "POST", cache: !1, data: formData, processData: !1, contentType: !1, headers: { AccessCode: service.GetDataInLocaleSession(AccessCodeKeyName) }, complete: function (a) {
                try { var c = $.parseJSON(a.responseText); c.IsSuccess ? (service.showSuccessAlert("引导页图片已上传", "1"), $scope.tableParams.reload(), $scope.onCancel()) : c.Message && service.showWarningAlert(c.Message, "") }
                catch (d) { service.showWarningAlert("上传图片错误", "") } finally { $("#loading").hide() }
            }
        });
    };
    $scope.onCancel = function () {
        $scope.defaultSetting(!0);
        if ($scope.curSelPics && $scope.curSelPics.length > 0) {
            var cnt = $scope.curSelPics.length;
            for (var i = 0; i < cnt; i++) {
                ZYFILE.funDeleteFile(i, true);
                ZYFILE.perUploadFile.splice(i, 1);
            }
            $scope.curSelPics = null;
        }
        ele.addClass(cls);
    };
    $scope.onDel = function (s) {
        if (s) {
            if (confirm('是否确认删除此引导页？')) {
                service.invokePost('Upload/RemoveGuidePagePhoto', { PicGuid: s.PicGuid }, function (result) {
                    if (result.IsSuccess && result.ResObject == true) {
                        service.showSuccessAlert("删除引导页图片成功!", "1");
                        $scope.tableParams.reload();
                    }
                });
            }
        }
    };
    $scope.onModify = function (a) { a && (a.$edit = !0, a.BeginDate = new Date(a.BeginDate), a.EndDate = new Date(a.EndDate)) };
    $scope.onEdit = function (s) {
        if (s && s.PicGuid) {
            service.invokePost('Upload/UpdateGuidePage', {
                PicGuid: s.PicGuid, Seq: s.Seq || 0, BeginDate: new Date(s.BeginDate).Format("yyyy/MM/dd"), EndDate: new Date(s.EndDate).Format("yyyy/MM/dd")
            }, function (result) {
                result.IsSuccess ? $scope.tableParams.reload() : result.ErrorMessage && service.showWarningAlert(result.ErrorMessage, "1");
            });
        }
    };
    $scope.onCancelEdit = function (a) { a && (a.$edit = !1, $scope.tableParams.reload()) };
    $scope.init();
});

app.controller('commercialCtr', function ($scope, service, $filter, ngTableParams) {
    var ele = jQuery('#commercialPanel'), cls = "display-close", sk;
    $scope.init = function () {
        $scope.defaultSetting();
        $scope.curSelPics = null;
        $scope.isAdd = false;
        $scope.maxNum = 5;
        $scope.picCnt = 0;
        $scope.loadPic();
        $scope.preCnt = 0;
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                $scope.preCnt = (params.page() - 1) * params.count();
                service.invokePost('Upload/QueryCommercialPhoto', {
                    PageIndex: params.page() - 1,
                    PageSize: params.count()
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(data.Total);
                        $defer.resolve(data.List);
                        $scope.maxNum = data.QNum;
                        $scope.picCnt = data.Total;
                    } else {
                        service.showWarningAlert(result.Message ? result.Message : '暂无广告弹窗', '1');
                        params.total(0);
                        $defer.resolve([]);
                    }
                });
            }
        });
    };
    $scope.loadPic = function () {
        sk = $("#pic").zyUpload({
            width: "650px",
            height: "100%",
            itemWidth: "120px",
            itemHeight: "100px",
            url: "/upload/uploadAction",
            multiple: false,
            dragDrop: true,
            del: true,
            finishDel: true,
            onSelect: function (files, allFiles) {
                if ($scope.curSelPics = allFiles, $scope.curSelPics && $scope.curSelPics.length > 1) {
                    return service.showWarningAlert("每次保存只能上传一张广告弹窗图片", ""), !1;
                }
            },
            onDelete: function (file, files) { $scope.curSelPics = files; }
        });
    };
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };
    $scope.onAdd = function () { return $scope.picCnt >= $scope.maxNum ? (service.showWarningAlert("最多只能上传" + $scope.maxNum + "张广告弹窗图片", "1"), void 0) : (ele.removeClass(cls), void 0) };
    $scope.defaultSetting = function (a) { var b = $filter("parseDtToChar")(!1); jQuery(".picSTime,.picETime").datetimepicker(b.option), $scope.qPicData = { picSTime: b.start, picETime: b.curDt }, a && (jQuery(".picSTime input").val(b.start), jQuery(".picETime input").val(b.curDt)) };
    $scope.onSave = function () {
        if (!$scope.curSelPics || 0 == $scope.curSelPics.length) return service.showWarningAlert("请先选择一张广告弹窗图片", ""), void 0;
        if ($scope.curSelPics.length > 1) return service.showWarningAlert("每次只能上传一张广告弹窗图片", ""), void 0;
        if ($scope.isNullOrEmpty($scope.qPicData.picTag)) return service.showWarningAlert("广告弹窗图片的标题不能为空", ""), void 0;
        var formData = new FormData; formData.append("picLoc", $scope.curSelPics[0]), formData.append("picTag", $scope.qPicData.picTag || ""), formData.append("sDt", jQuery(".picSTime input").val() || null), formData.append("eDt", jQuery(".picETime input").val() || null), formData.append("seq", $scope.qPicData.Seq || 0);
        $('#loading').show();
        jQuery.ajax({
            url: ULR + "Upload/NewCommercialPhoto", type: "POST", cache: !1, data: formData, processData: !1, contentType: !1, headers: { AccessCode: service.GetDataInLocaleSession(AccessCodeKeyName) }, complete: function (a) {
                try { var c = $.parseJSON(a.responseText); c.IsSuccess ? (service.showSuccessAlert("广告弹窗图片已上传", "1"), $scope.tableParams.reload(), $scope.onCancel()) : c.Message && service.showWarningAlert(c.Message, "") }
                catch (d) { service.showWarningAlert("上传图片错误", "") } finally { $("#loading").hide() }
            }
        });
    };
    $scope.onCancel = function () {
        $scope.defaultSetting(!0);
        if ($scope.curSelPics && $scope.curSelPics.length > 0) {
            var cnt = $scope.curSelPics.length;
            for (var i = 0; i < cnt; i++) {
                ZYFILE.funDeleteFile(i, true);
                ZYFILE.perUploadFile.splice(i, 1);
            }
            $scope.curSelPics = null;
        }
        ele.addClass(cls);
    };
    $scope.onDel = function (s) {
        if (s) {
            if (confirm('是否确认删除此广告弹窗？')) {
                service.invokePost('Upload/RemoveCommercialPhoto', { PicGuid: s.PicGuid }, function (result) {
                    if (result.IsSuccess && result.ResObject == true) {
                        service.showSuccessAlert("删除广告弹窗成功!", "1");
                        $scope.tableParams.reload();
                    }
                });
            }
        }
    };
    $scope.onModify = function (a) { a && (a.$edit = !0, a.BeginDate = new Date(a.BeginDate), a.EndDate = new Date(a.EndDate)) };
    $scope.onEdit = function (s) {
        if (s && s.PicGuid) {
            service.invokePost('Upload/UpdateCommercial', {
                PicGuid: s.PicGuid, Seq: s.Seq || 0, BeginDate: new Date(s.BeginDate).Format("yyyy/MM/dd"), EndDate: new Date(s.EndDate).Format("yyyy/MM/dd")
            }, function (result) {
                result.IsSuccess ? $scope.tableParams.reload() : result.ErrorMessage && service.showWarningAlert(result.ErrorMessage, "1");
            });
        }
    };
    $scope.onCancelEdit = function (a) { a && (a.$edit = !1, $scope.tableParams.reload()) };
    $scope.init();
});