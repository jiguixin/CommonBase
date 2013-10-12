var loading={
    showLoading:function () {
    var loadingImg = dojo.byId("loadingImg");
    esri.show(loadingImg);
    map.disableMapNavigation();
    map.hideZoomSlider();
},
    hideLoading:function () {
    var loadingImg = dojo.byId("loadingImg");
    esri.hide(loadingImg);
    map.enableMapNavigation();
    map.showZoomSlider();
}}