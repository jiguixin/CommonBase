function showXYLocation(evt) {
    var localPoint = evt.mapPoint;
    dojo.byId("xylocation").innerHTML = localPoint.x + ", " + localPoint.y;
}