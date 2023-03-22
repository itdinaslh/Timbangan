"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/scale")
    .build();

connection.start();

var pos = "1";
var current = "Timbangan1";

connection.on("Timbangan1", function (value) {    
    const div = document.getElementById('berat');    

    if (pos == "1") {
        div.textContent = value;
    }
});

connection.on("Timbangan2", function (value) {    
    const div = document.getElementById('berat');    

    if (pos == "2") {
        div.textContent = value;
    }
});

connection.on("Timbangan3", function (value) {    
    const div = document.getElementById('berat');       

    if (pos == "3") {
        div.textContent = value;
    }
});

$connection.hub.disconnected(function () {
    setTimeout(function () {
        $.connection.hub.start();
    }, 5000); // Restart connection after 5 seconds.
});

function ChangePos() {
    var x = document.getElementById("pos").value;
    pos = x;
}