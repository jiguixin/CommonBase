function showToolTip(evt) {
    map.infoWindow.setTitle("坐标");
    map.infoWindow.setContent("你点击的位置是：</br>x:" + evt.mapPoint.x+"</br>y:"+evt.mapPoint.y);
    map.infoWindow.show(evt.screenPoint, map.getInfoWindowAnchor(evt.screenPoint));
}