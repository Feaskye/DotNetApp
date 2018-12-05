app.filter('colStatusFilter', function () {
    return function (key) {
        var kt = new Boolean(key);
        if (kt == true)
            return '是';
        return '否';
    }
});

app.filter('dateFormatFilter', function () {
    return function (key) {
        //key="2015-07-31T17:37:39.607"
        var value = '';
        if (key == null || key == '') {
            value = '';
        } else if (key != null && key.indexOf('.') > 0) {
            //2015-08-11 14:34:17.667=>2015-08-11 14:34:17
            value = key.substring(0, key.indexOf('.'));
        } else if (key.length == 14) {
            value = key.substring(0, 4) + '-' + key.substring(4, 6) + '-' + key.substring(6, 8)
                + ' ' + key.substring(8, 10) + ':' + key.substring(10, 12) + ':' + key.substring(12, 14);
        } else {
            value = new Date(key).toLocaleDateString();
        }

        if (value != null && value.indexOf('T') > 0) {
            value = value.replace('T', ' ');
        }
        return value;
    }
});

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