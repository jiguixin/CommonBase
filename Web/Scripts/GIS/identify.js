
var identify= {
    //查询feature所有属性
    doIdentify: function(e) {
        $("#identifyFrm").css("visibility", "hidden");
        var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer";
        var identifyTask;
        identifyTask = new esri.tasks.IdentifyTask(url);

        var identifyParams = new esri.tasks.IdentifyParameters();
        identifyParams.tolerance = 3;
        identifyParams.returnGeometry = true;
        identifyParams.layerOption = esri.tasks.IdentifyParameters.LAYER_OPTION_ALL;
        identifyParams.width = map.width;
        identifyParams.height = map.height;

        map.graphics.clear();

        //构建数组存储选择的feature
        var featureResult = { features: [] };

        identifyParams.geometry = e.mapPoint;
        identifyParams.mapExtent = map.extent;
        identifyTask.execute(identifyParams, function(idResults) { addToMap(idResults, e); });

        function addToMap(idResults, e) {
            var content = "";
            for (var i = 0; i < idResults.length; i++) {
                var feature = idResults[i].feature;

                //向数据添加feature数据
                featureResult.features.push(feature);

                for (var attribute in feature.attributes) {
                    content += attribute + ":" + feature.attributes[attribute] + "</br>";
                }
                //添加定位连接
                //content += "<a href='#' onClick='identify.showFeature(featureResult.features[" + i + "]); return false;'>定位</a></br>";
                identify.showFeature(featureResult.features[i]);
            }
            //如果没有选择有feature，查询结果div隐藏
            if (content != "") {
                $("#identifyFrm").css("top", e.screenY - 50).css("left", e.screenX).css("visibility", "visible").html(content);
            } else {
                $("#identifyFrm").css("visibility", "hidden").html();
            }

        }

    },

    //查询设定字段，构建饼状图
    identifyPieChart: function(e) {
        var fields = ["规划人口数", "现有人口数"];
        var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer/1";

        var queryTask = new esri.tasks.QueryTask(url);
        var query = new esri.tasks.Query();
        query.spatialRelationship = esri.tasks.Query.SPATIAL_REL_INTERSECTS;
        query.outFields = fields;
        query.returnGeometry = true;

        query.geometry = e.mapPoint;
        queryTask.execute(query);
        dojo.connect(queryTask, "onComplete", getChart);

        function getChart(featureSet) {
            map.graphics.clear();

            var features = featureSet.features;
            var data = new Array();
            ;
            for (var i = 0; i < features.length; i++) {
                var feature = features[i];
                var attributes = feature.attributes;

                //向data数组添加值，数据值必须转换为double，js为float
                for (var j = 0; j < fields.length; j++) {
                    var data1 = [fields[j], parseFloat(attributes[fields[j]])];
                    data.push(data1);
                }

                //地图高亮显示选中feature
                identify.showFeature(feature);
            }
            if (features.length > 0) {

                $("#identifyFrm").css("top", e.screenY - 50).css("left", e.screenX).css("visibility", "visible").css("width", "300px").css("height", "350px").css("overflow", "hidden");
                identify.showPieChart(data);
            } else {
                $("#identifyFrm").css("visibility", "hidden").html();
            }
        }
    },
    
    //显示查询feature位置
    showFeature: function(feature) {
        map.graphics.clear();
        var symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([10, 250, 250]), 2), new dojo.Color([50, 200, 255, 0.5]));
        feature.setSymbol(symbol);
        map.graphics.add(feature);
    },
    //显示饼状图
    showPieChart: function(data) {

        var piechart = new Highcharts.Chart({
            chart: {
                renderTo: 'identifyFrm',
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: true
            },
            title: {
                text: ''
            },
            tooltip: {
                formatter: function() {
                    return '<b>' + this.point.name + '</b>: ' + this.percentage.toPrecision(2) + ' %';
                }
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    shadow: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                type: 'pie',
                name: '',
                data: data
            }]
        });
    }
};
