app.service('service', function ($http, config, $q) {
    this.url = config.serviceUrl;
    this.PermKeyName = "PermKeyName";
    this.MenuKeyName = "MenuKeyName";
    this.curPwd = "00000000-0000-0000-0000-00000000000x";
    this.chkMenuNode = "ChkMenuNode";
    this.navigationTxt = "NavigationTxt";
    var deferred = $q.defer();

    //退出登录
    this.logout = function () {
        window.sessionStorage.clear();
        location.href = 'login/login.html';
    };

    // 根据结果代码处理操作失败
    this.processFailedCode = function (code, msg) {
        switch (code) {
            case "118003":
                window.sessionStorage.clear();
                location.href = 'login/login.html';
                break;

            case "118004":
                window.sessionStorage.clear();
                location.href = 'login/login.html';
                break;

            default:
                if (msg)
                { alert(msg); }
                break;
        }
    };

    this.invokeGet = function (action, data, then) {
        var deferred = $q.defer();

        $http({
            method: "get",
            url: this.url + action,
            params: data
        })
        .success(function (response) {
            deferred.resolve(response);
            deferred.promise.then(function (response) {
                if (response.IsSuccess) {
                    then(response);
                } else {
                    switch (response.MsgCode) {
                        case "118002":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        case "118003":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        case "118004":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        default:
                            if (!response.IsSuccess && response.Message)
                            { alert(response.Message); }
                            break;
                    }
                }
            });
        });

        //$.get(this.url + action, data)
        //.success(function (data) {
        //    deferred.resolve(data);
        //    deferred.promise.then(then(data));
        //});
    }

    this.invokePost = function (action, data, then) {
        var deferred = $q.defer();

        $http({
            method: "post",
            url: this.url + action,
            data: data
        })
        .success(function (response) {
            deferred.resolve(response);
            deferred.promise.then(function (response) {
                if (response.IsSuccess) {
                    then(response);
                }
                else {
                    switch (response.MsgCode) {
                        case "118002":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        case "118003":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        case "118004":
                            window.sessionStorage.clear();
                            location.href = 'login/login.html';
                            break;

                        default:
                            $("#sysMessage").text(response.Message);
                            $('#messageModal').modal('toggle');

                            break;
                    }
                }
            });
        });



        //$.post(this.url + action, data)
        //.success(function (data) {
        //    deferred.resolve(data);
        //    deferred.promise.then(then(data));
        //});
    }

    //this.saveUserInLocale = function (data) {
    //    window.sessionStorage.setItem('accessCode', data);
    //}

    this.saveDataInLocaleSession = function (key, data) {
        window.sessionStorage.setItem(key, data);
    }

    this.GetDataInLocaleSession = function (key) {
        return window.sessionStorage.getItem(key);
    }

    this.locreload = function () { location.reload(); };

    this.getUserFromLocale = function () {
        var user = window.localStorage.getItem('utag');
        return $.parseJSON(user);
    }

    this.removeUserFromLocale = function () {
        window.localStorage.setItem('utag', '');
    }

    this.toLogin = function () { location.href = '../login/login.html'; };

    this.showalertMsg = function (s) {
        if (s != undefined && (s.tipMsg == undefined || s.tipMsg == null)) {
            s.tipMsg = '操作已成功';
        }
    };

    this.downloadExcel = function (s) {
        window.location.href = s;
    };

    this.showWarningAlert = function (data, index) {
        $("#warningAlert" + index).text(data);
        $("#warningAlert" + index).show(500).delay(5000).fadeOut(500);
    };

    this.loaddropdown = function (then,tk) {
        $.post(this.url + tk)
            .success(function (data) {
                deferred.resolve(data);
                deferred.promise.then(then(data));
            });
    };

    this.distip = function (k) {
        setTimeout(function () {
            if (k != undefined && (k.tipMsg == undefined || k.tipMsg == null)) {
                k.tipMsg = '';
            }
        }, 3000);
    };

    //客户下拉数据
    this.getCustomerInfoList = function (then) {
        this.invokeGet('Customer/GetCustomerInfo', {}, function (response) {
            then(response);
        });
    };

    //获取角色数据
    this.getAdminRoleList = function (then) {
        this.invokeGet('AdminAdmin/GetAdminRole', {}, function (response) {
            then(response);
        });
    };
    //获取省市县数据
    this.getProvinceCityList = function (then) {
        this.invokePost('Electronicfence/QueryProvinceCityData', {}, function (response) {
            then(response);
        });
    };

    //统计数据
    this.getTotalData = function (then) {
        this.invokeGet('Electronicfence/GetStatisticalListData', {}, function (response) {
            then(response);
        });
    };


        this.showMessage = function (data) {
            $("#sysMessage").text(data);
            $('#messageModal').modal('toggle');
        };

        this.showSuccessAlert = function (data, index) {
            $("#successAlert" + index).text(data);
            $("#successAlert" + index).show(500).delay(5000).fadeOut(500);
        };

        this.showWarningAlert = function (data, index) {
            $("#warningAlert" + index).text(data);
            $("#warningAlert" + index).show(500).delay(5000).fadeOut(500);
        };

        // 格式化JS日期格式成.net日期格式
        this.formatDateToNetDateTime = function (date) {


            if (date == null) return null;

            var y = date.getFullYear();

            var m = date.getMonth() + 1;
            m = m < 10 ? '0' + m : m;

            var d = date.getDate();
            d = d < 10 ? ('0' + d) : d;

            var h = date.getHours();
            h = h < 10 ? ('0' + h) : h;

            var min = date.getMinutes();
            min = min < 10 ? ('0' + min) : min;

            var s = date.getSeconds();
            s = s < 10 ? ('0' + s) : s;

            return y + '-' + m + '-' + d + ' ' + h + ':' + min + ':' + s;
        }

        // 是否正整数
        this.IsInt = function (num) {
            var r = /^\d+$/; //正整数
            return r.test(num);
        }

        // 是否经度
        this.IsLongitude = function (num) {
            var r = /^[\-\+]?(0?\d{1,2}\.\d{1,6}|1[0-7]?\d{1}\.\d{1,6}|180\.0{1,6})$/;
            return r.test(num);
        }

        // 是否纬度
        this.IsLatitude = function (num) {
            var r = /^[\-\+]?([0-8]?\d{1}\.\d{1,6}|90\.0{1,6})$/;
            return r.test(num);
        }

        // 是否正整数或浮点数
        this.IsIntOrFloat = function (num) {

            var r = /^[0-9]+[.]?[0-9]{0,2}$/;   //匹配整数和小数;
            return r.test(num);
        }

        // 检验字符串长度
        this.CheckLength = function (text, min, max) {
            text = text.toString();

            if (text.length < min || text.length > max)
                return false;

            return true;
        }

    });