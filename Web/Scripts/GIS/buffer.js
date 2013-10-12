var buffer = {
    //点缓冲
    pointBuffer: function (e) {

        esriConfig.defaults.io.proxyUrl = "../proxy.ashx";
        esriConfig.defaults.io.alwaysUseProxy = false;

        var gsvc = new esri.tasks.GeometryService("http://localhost/arcgis/rest/services/Geometry/GeometryServer");


        var symbol = new esri.symbol.SimpleMarkerSymbol();
        var graphic = new esri.Graphic(e.mapPoint, symbol);

        var params = new esri.tasks.BufferParameters();
        params.features = [graphic];

        params.distances = [1000];
        params.unit = esri.tasks.BufferParameters.UNIT_METER;
        gsvc.buffer(params);

        dojo.connect(gsvc, "onBufferComplete", showBufferSymbol);

        function showBufferSymbol(graphics) {
            map.graphics.clear();

            var symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 2), new dojo.Color([255, 0, 5, 0.5]));
            var graphic1 = new esri.Graphic(graphics[0].geometry, symbol);
            map.graphics.add(graphic1);

            var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer/0";
            var queryTask = new esri.tasks.QueryTask(url);

            var query = new esri.tasks.Query();
            query.returnGeometry = true;
            query.spatialRelationship = esri.tasks.Query.SPATIAL_REL_INTERSECTS;
            query.geometry = graphic1.geometry;
            queryTask.execute(query);

            dojo.connect(queryTask, "onComplete", showBufferResult);
        }

        function showBufferResult(featureSet) {
            //var symbol = new esri.symbol.SimpleMarkerSymbol();
            //symbol.style = esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE;
            //symbol.size(10);
            //symbol.Color(new dojo.Color([255, 255, 0, 0.5]));

            //var infoTemplate = new esri.infoTemplate("PS_YDMJ:${PS_YDMJ}", "${*}");

            var features = featureSet.features;
            for (var i = 0; i < features.length; i++) {
                var feature = features[i];
                identify.showFeature(feature);
            }
        }
    },
    //面缓冲
    polygonBuffer: function (e) {
        esriConfig.defaults.io.proxyUrl = "../proxy.ashx";
        esriConfig.defaults.io.alwaysUseProxy = false;

        var geometryService = new esri.tasks.GeometryService("http://localhost/arcgis/rest/services/Geometry/GeometryServer");
        var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer/1";
        var queryTask = new esri.tasks.QueryTask(url);
        var symbol = new esri.symbol.SimpleMarkerSymbol();
        var graphic = new esri.Graphic(e.mapPoint, "");

        var query = new esri.tasks.Query();
        query.returnGeometry = true;
        query.spatialRelationship = esri.tasks.Query.SPATIAL_REL_INTERSECTS;
        query.geometry = graphic.geometry;
        queryTask.execute(query);

        dojo.connect(queryTask, "onComplete", getSelectGeometry);

        function getSelectGeometry(featureSet) {
            var features = featureSet.features;
            symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([0, 0, 0]), 1), new dojo.Color([255, 0, 0, 0.25]));
            //把绘制的图形添加到map.graphics进行显示
            graphic = new esri.Graphic(features[0].geometry, symbol);
            map.graphics.add(graphic);
            geometryService.simplify([graphic], doSimplify);
        }
        function doSimplify(graphics) {
            doBuffer(graphics);
        }

        //缓冲
        function doBuffer(graphics) {
            //buffer参数
            var params = new esri.tasks.BufferParameters();
            //buffer的范围值
            params.distances = [1000];
            //空间参考
            //params.bufferSpatialReference = map.spatialReference;
            //输出结果的空间参考(与地图一致)
            params.outSpatialReference = map.spatialReference;

            params.features = graphics;
            //buffer的单位，从列表框获取
            params.unit = esri.tasks.BufferParameters.UNIT_METER;
            //buffer操作
            geometryService.buffer(params, showBuffer);
        }

        //显示buffer的结果
        function showBuffer(features) {
            map.graphics.clear();

            symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0, 0.65]), 2), new dojo.Color([255, 0, 0, 0.35]));

            for (var j = 0; j < features.length; j++) {
                graphic = new esri.Graphic(features[j].geometry, symbol);
                map.graphics.add(graphic);
            }


            var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer/0";
            var queryTask = new esri.tasks.QueryTask(url);

            esriConfig.defaults.io.proxyUrl = "../proxy.ashx";
            esriConfig.defaults.io.alwaysUseProxy = false;

            var query = new esri.tasks.Query();
            query.returnGeometry = true;
            query.spatialRelationship = esri.tasks.Query.SPATIAL_REL_WITHIN;
            query.geometry = graphic.geometry.getExtent();
            query.outSpatialReference = map.spatialReference;
            query.outFields = ["*"];
            queryTask.execute(query, getQueryFeatures);
            //dojo.connect(queryTask, "onComplete", getQueryFeatures);
            
            
        }
        //显示缓冲区范围内的地物
        function getQueryFeatures(featureSet) {
            var features = featureSet.features;
            for (var i = 0; i < features.length; i++) {
                showSelectFeature(features[i]);
            }
        }
    },
    //自定义绘制图形缓冲
    drawBuffer: function (geometry) {
        //根据图形的类型定义显示样式
        switch (geometry.type) {
            case "point":
                var symbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE, 10, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1), new dojo.Color([0, 255, 0, 0.25]));
                break;
            case "polyline":
                var symbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([255, 0, 0]), 1);
                break;
            case "polygon":
                var symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([0, 0, 0]), 1), new dojo.Color([255, 0, 0, 0.25]));
                break;
        }
        //把绘制的图形添加到map.graphics进行显示
        var graphic = new esri.Graphic(geometry, symbol);
        map.graphics.add(graphic);

        var geometryService = new esri.tasks.GeometryService("http://localhost/arcgis/rest/services/Geometry/GeometryServer");


        //如果是面需要先进行simplify操作，否则直接进行buffer
        if (geometry.type === "polygon") {
            geometryService.simplify([graphic], doSimplify);
        } else {
            doBuffer([graphic]);
        }

        //simplify结束调用buffer

        function doSimplify(graphics) {
            doBuffer(graphics);
        }

        function doBuffer(graphics) {
            //buffer参数
            var params = new esri.tasks.BufferParameters();
            //buffer的范围值，从输入框中获取
            params.distances = [500];
            //空间参考
            //params.bufferSpatialReference = new esri.SpatialReference({ wkid: dojo.byId("wkid").value });
            //输出结果的空间参考
            params.outSpatialReference = map.spatialReference;
            params.features = graphics;
            //buffer的单位，从列表框获取
            params.unit = esri.tasks.BufferParameters.UNIT_METER;
            //buffer操作
            geometryService.buffer(params, showBuffer);
        }

        //显示buffer的结果
        function showBuffer(features) {
            map.graphics.clear();

            symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0, 0.65]), 2), new dojo.Color([255, 0, 0, 0.35]));

            //for (var j = 0; j < features.length; j++) {
            //    graphic = new esri.Graphic(features[j].geometry, symbol);
            //    map.graphics.add(graphic);
            //}
            var url = "http://localhost/ArcGIS/rest/services/MapTest/MapServer/0";
            var queryTask = new esri.tasks.QueryTask(url);

            esriConfig.defaults.io.proxyUrl = "../proxy.ashx";
            esriConfig.defaults.io.alwaysUseProxy = false;

            var query = new esri.tasks.Query();
            query.returnGeometry = true;
            query.outFields = ["*"];
            var graphic = new esri.Graphic(features[0].geometry, symbol);
            map.graphics.add(graphic);

            query.geometry = graphic.geometry.getExtent();
            queryTask.execute(query, getQueryFeatures);
            //dojo.connect(queryTask, "onComplete", getQueryFeatures);
        }
        //显示缓冲区范围内的地物
        function getQueryFeatures(featureSet) {
            var features = featureSet.features;
            for (var i = 0; i < features.length; i++) {
                showSelectFeature(features[i]);
            }
        }
    }
}