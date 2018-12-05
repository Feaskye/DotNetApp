app.controller('BaiduMapCtr', function ($scope, service, $filter, ngTableParams) {
    $scope.onQuery = function () {
        $scope.tableParams.page(1);
        $scope.tableParams.reload();
    };

    //统计车辆数据
    $scope.init = function () {
        $scope.TotalData = {};
        service.getTotalData(function (st) {
            if (st && st.ResObject != null) {
                $scope.TotalData = st.ResObject
            }


        });

        $scope.selRegion = {};
        //获取省市区数据
        service.getProvinceCityList(function (st) {
            if (st && st.ResObject != null) {
                $scope.selRegion = st.ResObject;
            }
        });
      
        get_data && tmplng && tmplat && get_data({ lng: tmplng, lat: tmplat });

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10
        }, {
            total: 0,
            getData: function ($defer, params) {
                if (!$scope.province || !$scope.city || !$scope.District)
                    return;
                service.invokePost('Electronicfence/GetElectronicTjListByCondition', {
                    ProvinceID: $scope.province || 0,
                    CityID: $scope.city || 0,
                    DistrictID: $scope.District || 0
                },
                function (result) {
                    if (result.IsSuccess && result.ResObject != null) {
                        data = result.ResObject;
                        params.total(result.ResObject.Total);
                        $defer.resolve(data.List);
                    } else {
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






    $scope.init();
});