function showExtent(extent) {
    var s = "";
    s = "最小X: " + extent.xmin + "&nbsp;</br>"
       + "最小Y: " + extent.ymin + "&nbsp;</br>"
       + "最大X: " + extent.xmax + "&nbsp;</br>"
       + "最大Y: " + extent.ymax;
    dojo.byId("extent").innerHTML = s;
}
