app.service('service', function ($http, config, $q) {
    this.url = config.serviceUrl;
    var deferred = $q.defer();

    this.invokeGet = function (action, data, then) {
        var deferred = $q.defer();
        $.get(this.url + action, data)
        .success(function (data) {
            deferred.resolve(data);
            deferred.promise.then(then(data));
        });
    }

    this.invokePost = function (action, data, then) {
        var deferred = $q.defer();
        $.post(this.url + action, data)
        .success(function (data) {
            deferred.resolve(data);
            deferred.promise.then(then(data));
        });
    }

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

    this.showalertMsg = function (s) {
        if (s != undefined && (s.tipMsg == undefined || s.tipMsg == null)) {
            s.tipMsg = '操作已成功';
        }
    };

    this.loaddropdown = function (then, tk) {
        $.post(this.url + tk)
            .success(function (data) {
                deferred.resolve(data);
                deferred.promise.then(then(data));
            });
    };

    this.agentType = function () { var u = navigator.userAgent; var sc = { agents: function () { return { isAndroid: u.indexOf('Android') > -1 || u.indexOf('Adr') > -1, isiOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/) }; } }; return sc; };

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

    this.IsInt = function (num) {
        var r = /^\d+$/;
        return r.test(num);
    }

    this.Guidformat = function (s) {
        if (s) {
            return /^\w{8}-(\w{4}-){3}\w{12}$/.test(s);
        }
        return false;
    };

    this.ToSpecPage = function (s, k) {
        var t = this.agentType; var q = t().agents(); if (q.isAndroid && typeof s == 'function') { s(); }
        else if (q.isiOS && typeof k == 'function') { k(); }
    };
});