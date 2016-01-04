var geocoder;
var var_map;
var var_marker;
function init_map() {
    var var_location = new google.maps.LatLng(45.430817, 12.331516);

    var var_mapoptions = {
        center: var_location,
        zoom: 14

    };
    //GEOCODER
    geocoder = new google.maps.Geocoder();
    var_marker = new google.maps.Marker({
        position: var_location,
        map: var_map,
        draggable: true,
        title: "Place Where Violation Occured"
    });

    var_map = new google.maps.Map(document.getElementById("map-container"),
    var_mapoptions);
    var_marker.setMap(var_map);

    google.maps.event.addListener(var_marker, 'drag', function () {
        geocoder.geocode({ 'latLng': var_marker.getPosition() }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    $('#Address').val(results[0].formatted_address);
                    $('#Latitude').val(var_marker.getPosition().lat());
                    $('#Longitude').val(var_marker.getPosition().lng());
                }
            }
        });
    });

}
$("#Address").focusout(function () {
    var geocoder = new google.maps.Geocoder();
    var strSearch = document.getElementById("Address").value;
    geocoder.geocode({ 'address': strSearch }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var x = results[0].geometry.location.lat();
            var y = results[0].geometry.location.lng();
            document.getElementById('Latitude').value = x;
            document.getElementById('Longitude').value = y;
            var latlng = new google.maps.LatLng(x, y);
            var myOptions = {
                zoom: 8,
                center: latlng
            };
            map = new google.maps.Map(document.getElementById("map-container"), myOptions);
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(x, y),
                map: map,
                title: strSearch
            });
            
            var infowindow = new google.maps.InfoWindow({
                content: strSearch
            });
            infowindow.open(map, marker);
            google.maps.event.addDomListener(window, 'load');
        } else {
            alert("Location Not Found...");
            document.getElementById('Address').value = "";
        }
    });
});

$(function () {
    $("#Address").autocomplete({
        //This bit uses the geocoder to fetch address values
        source: function (request, response) {
            geocoder.geocode({ 'address': request.term }, function (results, status) {
                response($.map(results, function (item) {
                    return {
                        label: item.formatted_address,
                        value: item.formatted_address,
                        latitude: item.geometry.location.lat(),
                        longitude: item.geometry.location.lng()
                    }
                }));
            })
        },
        //This bit is executed upon selection of an address
        select: function (event, ui) {
            $("#Latitude").val(ui.item.latitude);
            $("#Longitude").val(ui.item.longitude);
            var pos = ui.item.position;
            var lct = ui.item.locType;
            var bounds = ui.item.bounds;
            if (bounds) {
                var_map.fitBounds(bounds);
            }
            var location = new google.maps.LatLng(ui.item.latitude, ui.item.longitude);
            var_marker.setPosition(location);
            var_map.setCenter(location);
        }
    });
});

google.maps.event.addDomListener(window, 'load', init_map);
