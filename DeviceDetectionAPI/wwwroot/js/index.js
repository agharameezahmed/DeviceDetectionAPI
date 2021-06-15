const uri = 'api/devicedetection';

function getDeviceInfo() {
    fetch(uri)
        .then(response => response.json())
        .then(function (data) {
            _displayItems(data);
        })
        .catch(function (error) {
            console.error('Unable to get data.', error);
        });
}

function _displayItems(data) {
    _clearItems();
    document.getElementById('deviceType').innerHTML = data.deviceType;
    document.getElementById('operatingSystem').innerHTML = data.operatingSystem;
}

function _clearItems() {
    document.getElementById('deviceType').innerHTML = "";
    document.getElementById('operatingSystem').innerHTML = "";
}