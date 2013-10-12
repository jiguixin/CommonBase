var renderMap = {
    //唯一值渲染
    uniqueRender: function (attributeField) {
        map.graphics.clear();
        var queryTask = new esri.tasks.QueryTask("http://localhost/ArcGIS/rest/services/MapTest/MapServer/1");
        var query = new esri.tasks.Query();
        query.where = "1=1";
        query.outFields = [attributeField];
        query.returnGeometry = true;
        queryTask.execute(query, addUniqueRenderToMap);

        function addUniqueRenderToMap(featureSet) {
            var defaultSymbol = new esri.symbol.SimpleFillSymbol().setStyle(esri.symbol.SimpleFillSymbol.STYLE_NULL);
            defaultSymbol.outline.setStyle(esri.symbol.SimpleFillSymbol.STYLE_NULL);

            var renderer = new esri.renderer.UniqueValueRenderer(defaultSymbol, attributeField);
            renderer.addValue(30000, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([255, 0, 0, 0.5])));
            renderer.addValue(35000, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([0, 255, 0, 0.5])));
            renderer.addValue(0, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([0, 0, 255, 0.5])));

            map.graphics.setRenderer(renderer);
            dojo.forEach(featureSet.features, function (feature) {
                map.graphics.add(feature);
            });
        }
    },
    //分级渲染：自定义需要渲染的等级和等级范围
    classBreakRender: function (attributeField) {
        map.graphics.clear();
        var queryTask = new esri.tasks.QueryTask("http://localhost/ArcGIS/rest/services/MapTest/MapServer/1");
        var query = new esri.tasks.Query();
        query.where = "1=1";
        query.outFields = [attributeField];
        query.returnGeometry = true;
        queryTask.execute(query, addClassBreakRenderToMap);

        function addClassBreakRenderToMap(featureSet) {
            var symbol = new esri.symbol.SimpleFillSymbol();
            symbol.setColor(new dojo.Color([150, 150, 150, 0.3]));

            var renderer = new esri.renderer.ClassBreaksRenderer(symbol, attributeField);
            renderer.addBreak(0, 15000, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([56, 168, 0, 0.5])));
            renderer.addBreak(19400, 30000, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([139, 209, 0, 0.5])));
            renderer.addBreak(33100, 67000, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([255, 255, 0, 0.5])));
            renderer.addBreak(68000, 112200, new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color([255, 128, 0, 0.5])));

            map.graphics.setRenderer(renderer);
            dojo.forEach(featureSet.features, function (feature) {
                map.graphics.add(feature);
            });
        }
    },
    //分级渲染：可用于点图层，根据设定的等级范围进行渲染
    classBreakRender1: function (attributeField, numRanges) {
        map.graphics.clear();
        var queryTask = new esri.tasks.QueryTask("http://localhost/ArcGIS/rest/services/MapTest/MapServer/0");
        var query = new esri.tasks.Query();
        query.where = "1=1";
        query.outFields = [attributeField];
        query.returnGeometry = true;
        queryTask.execute(query, addClassBreakRenderToMap1);

        function addClassBreakRenderToMap1(featureSet) {
            var features = featureSet.features;

            var min = max = parseFloat(features[0].attributes[attributeField]);

            //找到字段最大和最小值
            dojo.forEach(features, function(feature) {
                min = Math.min(min, feature.attributes[attributeField]);
                max = Math.max(max, feature.attributes[attributeField]);
            });

            //根据分级数计算分级间距
            var breaks = (max - min) / numRanges;

            var outline = new esri.symbol.SimpleLineSymbol().setWidth(1).setColor(new dojo.Color([255,0,0,0.5]));
            var fillColor = new dojo.Color([240,150,240,0.5]);
            var defaultSymbol = new esri.symbol.SimpleMarkerSymbol().setSize(1).setOutline(outline);

            var renderer = new esri.renderer.ClassBreaksRenderer(defaultSymbol, attributeField);
            
            for (var i=0; i<numRanges; i++) {
                renderer.addBreak(parseFloat(min + (i*breaks)),
                    parseFloat(min + ((i+1)*breaks)),
                    new esri.symbol.SimpleMarkerSymbol().setSize((i+1)*6).setColor(fillColor).setOutline(outline));
            }

            map.graphics.setRenderer(renderer);

            dojo.forEach(featureSet.features, function(feature) {
                map.graphics.add(feature);
            });
        }


    }
}
