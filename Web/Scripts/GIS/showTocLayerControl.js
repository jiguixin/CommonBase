
    var maplayer;
    function showLayerList(layer) {
        maplayer = layer;
        var infos = layer.layerInfos, info;
        var items = [];
        var visible = [];
        var il = infos.length;

        for (var i = 0; i < il; i++) {
            info = infos[i];
            if (info.defaultVisiblity) {
                visible.push(info.id);
            }
            items[i] = "<input type='checkbox' class='list_item' checked='" + (info.defaultVisiblity ? "checked" : "") + "'id='" + info.id + "' onclick='updateLayerVisiblility()'/><label for='" + info.id + "'>" + info.name + "</label></br>";
        }

        dojo.byId("TocLayerControl").innerHTML = items.join();
        layer.setVisibleLayers(visible);
    }

    function updateLayerVisiblility() {
        var inputs = dojo.query(".list_item"), input;
        var visible = [];
        var il = inputs.length;
        for (var i = 0; i < il; i++) {
            input = inputs[i];
            if (input.checked) {
                visible.push(input.id);
            }
        }
        maplayer.setVisibleLayers(visible);
    }
    