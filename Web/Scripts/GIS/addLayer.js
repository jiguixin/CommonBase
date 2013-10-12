function addLayer(map) {
    //Query all counties in Kansas
    var countyQueryTask = new esri.tasks.QueryTask("http://localhost/ArcGIS/rest/services/MapTest/MapServer/1");
    var countyQuery = new esri.tasks.Query();
    countyQuery.outFields = ["*"];
    countyQuery.returnGeometry = true;
    countyQuery.outSpatialReference = map.spatialReference;
    countyQuery.where = "OBJECTID>10";
    countyQueryTask.execute(countyQuery, addCountyFeatureSetToMap);

}

function addCountyFeatureSetToMap(featureSet) {
    var symbol = new esri.symbol.SimpleFillSymbol();
    symbol.setColor(new dojo.Color([150, 150, 150, 0.5]));

    //Create graphics layer for counties
    var countyLayer = new esri.layers.GraphicsLayer();
    map.addLayer(countyLayer);

    var infoTemplate = new esri.InfoTemplate("${NAME}", "${*}");

    //Add counties to the graphics layer
    dojo.forEach(featureSet.features, function (feature) {
        countyLayer.add(feature.setSymbol(symbol).setInfoTemplate(infoTemplate));
    });
}