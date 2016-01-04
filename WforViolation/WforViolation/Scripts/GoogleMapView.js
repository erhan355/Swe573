var geocoder;
var var_map;
var var_marker;
var violationLatitude;
var violationLongitude;
function init_map() {
    //violationLatitude = $("#Latitude").val();
    //violationLongitude = $("#Longitude").val();

    //var floatLatitude = parseFloat(violationLatitude);
    //var floatLongitude = parseFloat(violationLongitude);

 
    var var_location = new google.maps.LatLng(document.getElementById("Latitude").value, document.getElementById("Longtitude").value);
    //var var_location = new google.maps.LatLng(45.430817, 12.331516);
    var var_mapoptions = {
        center: var_location,
        zoom: 14

    };
    //GEOCODER
    geocoder = new google.maps.Geocoder();
    var_marker = new google.maps.Marker({
        position: var_location,
        map: var_map,
        draggable: false,
        title: "Place Where Violation Occured"
    });

    var_map = new google.maps.Map(document.getElementById("map-container"),
    var_mapoptions);
    var_marker.setMap(var_map);
}

google.maps.event.addDomListener(window, 'load', init_map);
