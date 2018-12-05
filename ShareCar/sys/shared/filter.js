
// Course状态过滤器
app.filter('CourseStatusFilter', function () {
    return function (key) {

        switch (key) {
            case 0:
                return "未启用";
                break;

            case 1:
                return "启用";
                break;
        }

        return "未知";
    }
});


app.filter("cusStatusFilter", function () { return function (a) { var b, c = "未知"; try { b = Number(a) } catch (d) { return c } switch (b) { case 0: return "禁用"; case 1: return "启用" } return c } });

app.filter('colStatusFilter', function () {
    return function (key) {
        var kt = new Boolean(key);
        if (kt == true)
            return '是';
        return '否';
    }
});

app.filter('genderFilter', function () {
    return function (key) {
        return key != null && key != undefined ? typeof key == 'boolean' ? !key ? '女' : '男' : key : '';
    }
});

app.filter('adminStatusFilter', function () {
    return function (key) {
        var st = '未知';
        switch (key) {
            case 0:
                st = '否';
                break;
            case 1:
                st = '是';
                break;
        }
        return st;
    };
});

app.filter('memberStatusFilter', function () {
    return function (key) {
        if (/^\d+$/.test(key)) { return key == 0 ? '正常' : '锁定'; }
        return key;
    }
});

app.filter('dateFormatFilter', function () {
    return function (key, s) {
        //key="2015-07-31T17:37:39.607"
        var value = '';
        if (key == null || key == '') {
            value = '';
        } else if (key != null && key.indexOf && key.indexOf('.') > 0) {
            //2015-08-11 14:34:17.667=>2015-08-11 14:34:17
            value = key.substring(0, key.indexOf('.'));
        } else if (key.length && key.length == 14) {
            value = key.substring(0, 4) + '-' + key.substring(4, 6) + '-' + key.substring(6, 8)
                + ' ' + key.substring(8, 10) + ':' + key.substring(10, 12) + ':' + key.substring(12, 14);
        } else {
            value = new Date(key).toLocaleString();
        }

        if (value != null && value.indexOf('T') > 0) {
            value = value.replace('T', ' ');
        }
        if (s) {
            value = value.replace(' 00:00:00', '');
        }
        return value;
    }
});

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

app.filter('parseDtToChar', function () {
    return function (key) {
        var today = new Date();
        var month = (today.getMonth() + 1) < 10 ? ('0' + (today.getMonth() + 1)) : (today.getMonth() + 1);
        var curDate = today.getDate() < 10 ? '0' + today.getDate() : today.getDate();
        var hour = today.getHours() < 10 ? '0' + today.getHours() : today.getHours();
        var min = today.getMinutes() < 10 ? '0' + today.getMinutes() : today.getMinutes();
        var sec = today.getSeconds() < 10 ? '0' + today.getSeconds() : today.getSeconds();
        var start = today.getFullYear() + '/' + month + '/01 ';
        var curDt = today.getFullYear() + '/' + month + '/' + curDate + ' ';
        var option = { pickDate: true, format: "yyyy/MM/dd", pickTime: false };
        if (key) {
            start = start + hour + ':' + min + ':' + sec;
            curDt = curDt + hour + ':' + min + ':' + sec;
            option = { pickDate: true, pickTime: true, hourStep: 1, minuteStep: 15, secondStep: 30, use24hours: true };
        }
        var dt = { start: start, curDt: curDt, option: option };
        return dt;
    }
});

// 手机号隐藏过滤器
app.filter('PhoneMaskFilter', function () {
    return function (key) {
        if (key != null) {
            if (key.length == 11) {
                return key.substring(0, 3) + "****" + key.substring(7, 11);
            }
        }

        return key;
    }
});

app.filter('permStatusFilter', function () {
    return function (key) {
        return key ? "是" : "否";
    }
});

app.filter('picLocFilter', function () {
    return function (key) {
        return ULR.replace("/api/", "") + key;
    }
});

app.filter('breakdownTypeFilter', function () {
    return function (key) {
        try {
            var s = Number(key), str = '';
            switch (s) {
                case 0:
                    str = '无法开关锁';
                    break;
                case 1:
                    str = '无法骑行';
                    break;
                case 2:
                    str = '无法结算';
                    break;
                case 3:
                    str = '二维码受损';
                    break;
                case 4:
                    str = '其它问题';
                    break;
            }
        } catch (e) {
        }
        return str;
    }
});

app.filter('breakdownGradeNameFilter', function () {
    return function (key) {
        try {
            var s = Number(key), str = '';
            switch (s) {
                case 1:
                    str = '等级一';
                    break;
                case 2:
                    str = '等级二';
                    break;
                case 3:
                    str = '等级三';
                    break;
            }
        } catch (e) {
        }
        return str;
    }
});

app.filter('servicemanStatusFilter', function () {
    return function (key) {
        return key == 0 ? "禁用" : "正常";
    }
});