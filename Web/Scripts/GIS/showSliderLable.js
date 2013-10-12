var scaleLables;
function showSliderLabel(layer) {
    var lables = [];
    var lods = layer.tileInfo.lods;
    for (var i = 0; i < lods.length; i++) {
        lables[i] = lods[i].scale;
    }
    scaleLables = lables;
    
    esriConfig.defaults.map.sliderLabel = {
        tick: 0,
        lables: lables,
        style: "width:2em; font-family:Verdana; font-size:65%; color:#fff; padding-left:2px;"
    };
}
function sliderLabelShowOrHide() {
    var inputs = dojo.query("#sliderLableCheckBox"), input;
    input = inputs[0];
    if (input.checked) {
        esriConfig.defaults.map.sliderLabel = {
            tick: 0,
            lables: scaleLables,
            style: "width:2em; font-family:Verdana; font-size:65%; color:#fff; padding-left:2px;"
        };
    } else {
        esriConfig.defaults.map.sliderLabel = {
            tick: 0,
            lables: ""
        };
    }
}